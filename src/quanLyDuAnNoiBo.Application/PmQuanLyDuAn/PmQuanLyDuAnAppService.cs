using quanLyDuAnNoiBo.Accounts;
using quanLyDuAnNoiBo.BodQuanLyDuAn;
using quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos;
using quanLyDuAnNoiBo.CanhBao;
using quanLyDuAnNoiBo.CongViec;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.Global;
using quanLyDuAnNoiBo.Global.Dtos;
using quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.PmQuanLyDuAn
{
    public class PmQuanLyDuAnAppService : quanLyDuAnNoiBoAppService, IPmQuanLyDuAnAppService
    {
        private readonly IPmQuanLyDuAnRepository _pmQuanLyDuAnRepository;
        private readonly ISprintDuAnRepository _sprintRepository;
        private readonly IModuleDuAnRepository _moduleRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICanhBaoRepository _canhBaoRepository;
        private readonly ICongViecRepository _congViecRepository;
        public PmQuanLyDuAnAppService(
            IPmQuanLyDuAnRepository pmQuanLyDuAnRepository,
            ISprintDuAnRepository sprintRepository,
            IModuleDuAnRepository moduleRepository,
            ISubTaskRepository subTaskRepository,
            IAccountRepository accountRepository,
            ICanhBaoRepository canhBaoRepository,
            ICongViecRepository congViecRepository
        )
        {
            _pmQuanLyDuAnRepository = pmQuanLyDuAnRepository;
            _sprintRepository = sprintRepository;
            _moduleRepository = moduleRepository;
            _subTaskRepository = subTaskRepository;
            _accountRepository = accountRepository;
            _canhBaoRepository = canhBaoRepository;
            _congViecRepository = congViecRepository;
        }

        public async Task<PagedResultDto<GetAllDuAnByPmDto>> GetAllDuAnByPm(GetAllDuAnByPmInputDto input)
        {
            var currentUserId = CurrentUser.Id;

            // Fetch the filtered list of projects (DuAn) by PM
            var query = await _pmQuanLyDuAnRepository.GetAllDuAnByPm(currentUserId, input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword, input.KhachHang, input.TrangThai);
            var totalCount = query.Count();

            // Fetch all projects, sprints, and subtasks
            var duAns = await _pmQuanLyDuAnRepository.GetListAsync();
            var sprints = await _sprintRepository.GetListAsync();
            var subtasks = await _subTaskRepository.GetListAsync();

            // Join 3 tables and group by duAn to get distinct NhanSu count for each duAn
            var nhanSuCounts = duAns
            .GroupJoin(
                sprints,
                duAn => duAn.Id,
                sprint => sprint.DuAnId,
                (duAn, duAnSprints) => new { duAn, duAnSprints }
            )
            .SelectMany(
                x => x.duAnSprints.DefaultIfEmpty(), // Ensure duAnSprints is not null
                (x, sprint) => new { duAn = x.duAn, sprint } // Explicitly define the type of duAn
            )
            .GroupJoin(
                subtasks,
                x => x.sprint != null ? x.sprint.Id : Guid.Empty, // Ensure sprint is not null
                subtask => subtask.SprintId,
                (x, sprintSubtasks) => new { x.duAn, sprintSubtasks }
            )
            .GroupBy(x => x.duAn.Id)
            .Select(g => new
            {
                DuAnId = g.Key,
                NhanSuCount = g.SelectMany(x => x.sprintSubtasks)
                    .SelectMany(subtask => subtask.NhanSu != null ? subtask.NhanSu.Split(',') : new string[0])
                    .Distinct()
                    .Count()
            })
            .ToList();

            // Convert nhanSuCounts to a dictionary for easy lookup
            var nhanSuCountDict = nhanSuCounts.ToDictionary(x => x.DuAnId, x => x.NhanSuCount);

            // Map the projects to the DTO
            var items = ObjectMapper.Map<List<QuanLyDuAn>, List<GetAllDuAnByPmDto>>(query);
            foreach (var item in items)
            {
                // Set the NhanSu property for each DTO item based on the dictionary
                if (item.Id.HasValue && nhanSuCountDict.ContainsKey(item.Id.Value))
                {
                    item.NhanSu = nhanSuCountDict[item.Id.Value];
                }
                else
                {
                    item.NhanSu = 0; // Default to 0 if no NhanSu count found or item.Id is null
                }
            }

            return new PagedResultDto<GetAllDuAnByPmDto>()
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<GetDuAnByPmIdDto> GetDuAnByPmId(Guid duAnId)
        {
            var currentUserId = CurrentUser.Id;
            var query = await _pmQuanLyDuAnRepository.GetDuAnByPmId(currentUserId, duAnId);
            var result = ObjectMapper.Map<QuanLyDuAn, GetDuAnByPmIdDto>(query);
            return result;
        }

        public async Task<bool> CreateSprintAsync(CreateOrUpdateSprintDto input)
        {
            if (input == null)
            {
                return false;
            }

            var checkSprintExist = await _sprintRepository.CheckExistSprint(input.SprintId, input.TenSprint, input.DuAnId);
            if (checkSprintExist)
            {
                throw new SprintAlreadyExistException();
            }

            var sprint = ObjectMapper.Map<CreateOrUpdateSprintDto, SprintDuAn>(input);
            sprint.DuAnId = input.DuAnId;
            sprint.TrangThaiSprint = DuAn.TrangThaiSprintConsts.KhoiTao;
            await _sprintRepository.InsertAsync(sprint);
            return true;
        }

        public async Task<bool> UpdateSprintAsync(CreateOrUpdateSprintDto input)
        {
            if (input != null)
            {
                var sprint = await _sprintRepository.GetSprintById(input.SprintId);
                if (sprint == null)
                {
                    return false;
                }

                sprint.TenSprint = input.TenSprint;
                sprint.NgayBatDau = input.NgayBatDau;
                sprint.NgayKetThuc = input.NgayKetThuc;
                await _sprintRepository.UpdateAsync(sprint);

                if (input.Subtasks.Count > 0 && input.Subtasks != null)
                {
                    foreach (var subtask in input.Subtasks)
                    {
                        string nhanSu = string.Join(",", subtask.NhanSu.Select(e => e.Id));
                        var subTaskInfo = await _subTaskRepository.GetSubTaskById(subtask.SubtaskId);
                        if (subTaskInfo != null)
                        {
                            subTaskInfo.ThoiGianBatDau = subtask.ThoiGianBatDau.ToLocalTime();
                            subTaskInfo.ThoiGianKetThuc = subtask.ThoiGianKetThuc.ToLocalTime();
                            subTaskInfo.NhanSu = nhanSu;
                            subTaskInfo.SprintId = input.SprintId;
                            await _subTaskRepository.UpdateAsync(subTaskInfo);
                        } else
                        {
                            throw new UserFriendlyException("Subtask không tồn tại!");
                        }
                    }
                }

                return true;
            }

            return false;
        }

        public async Task SendAcceptSprintToBoD(Guid sprintId)
        {
            var sprint = await _sprintRepository.GetSprintById(sprintId);
            if (sprint == null)
            {
                //throw new SprintNotFoundException();
                return;
            }

            sprint.TrangThaiSprint = DuAn.TrangThaiSprintConsts.DaGuiPheDuyet;
            await _sprintRepository.UpdateAsync(sprint);
            var subtasks = await _subTaskRepository.GetSubtaksBySprintId(sprintId);
            if (subtasks.Count > 0)
            {
                foreach (var subtask in subtasks)
                {
                    subtask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.ChoPheDuyetSprint;
                    await _subTaskRepository.UpdateAsync(subtask);
                }
            }
        }

        public async Task CreateSubTaskByModuleId(CreateSubTaskDto input)
        {
            try
            {
                var module = await _moduleRepository.GetModuleById(input.ModuleId);
                if (module == null)
                {
                    //throw new ModuleNotFoundException();
                    return;
                }

                var subTask = ObjectMapper.Map<CreateSubTaskDto, SubTask>(input);
                subTask.TongThoiGian = input.PM + input.Dev + input.Test + input.BA;
                subTask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.KhoiTao;
                await _subTaskRepository.InsertAsync(subTask);
                if (subTask != null)
                {
                    var canhBao = new Entities.CanhBao
                    {
                        SubtaskId = subTask.Id,
                        TenTask = module.TenModule,
                        TenSubtask = subTask.TenSubTask,
                        TrangThaiSubtask = subTask.TrangThaiSubtask,
                    };
                    await _canhBaoRepository.InsertAsync(canhBao);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ModuleSubTaskDto>> GetAllListModuleByDuAnId(Guid duAnId)
        {
            var query = await _moduleRepository.GetModuleDuAnAsync(duAnId);
            var result = ObjectMapper.Map<List<ModuleDuAn>, List<ModuleSubTaskDto>>(query);
            foreach (var item in result)
            {
                var subTask = await _subTaskRepository.GetAllListSubTaskByModuleId(item.Id);
                item.SubTasks = subTask.Select(x => new SubtaskInfo
                {
                    SubtaskId = x.Id,
                    TenSubtask = x.TenSubTask,
                    PM = x.PM,
                    DEV = x.Dev,
                    BA = x.BA,
                    TESTER = x.Test,
                    TongSoGio = x.TongThoiGian,
                    TrangThaiSubtask = x.TrangThaiSubtask,
                }).ToList();
            }
            return result;
        }

        public async Task<ModuleSubTaskDto> GetAllMandayLeft(Guid moduleId, Guid? subTaskId)
        {
            var module = await _moduleRepository.GetModuleById(moduleId);
            var result = ObjectMapper.Map<ModuleDuAn, ModuleSubTaskDto>(module);
            var subTask = await _subTaskRepository.GetAllListSubTaskByModuleId(moduleId);

            if (subTask.Count > 0)
            {
                result.PM -= subTask.Sum(x => x.PM);
                result.Dev -= subTask.Sum(x => x.Dev);
                result.Test -= subTask.Sum(x => x.Test);
                result.BA -= subTask.Sum(x => x.BA);
                result.TongThoiGian -= subTask.Sum(x => x.PM + x.Dev + x.Test + x.BA);

                result.PM = GlobalFunction.LamTronTien(result.PM);
                result.Dev = GlobalFunction.LamTronTien(result.Dev);
                result.Test = GlobalFunction.LamTronTien(result.Test);
                result.BA = GlobalFunction.LamTronTien(result.BA);
                result.TongThoiGian = GlobalFunction.LamTronTien(result.TongThoiGian);
            }

            return result;
        }

        public async Task UpdateSubtask(UpdateSubTaskDto input)
        {
            var subTask = await _subTaskRepository.GetSubTaskById(input.Id);
            if (subTask == null)
            {
                //throw new SubTaskNotFoundException();
                return;
            }

            subTask.TenSubTask = input.TenSubTask;
            subTask.PM = input.PM;
            subTask.Dev = input.Dev;
            subTask.Test = input.Test;
            subTask.BA = input.BA;
            await _subTaskRepository.UpdateAsync(subTask);
        }

        public async Task DeleteSubTask(Guid subTaskId)
        {
            var subTask = await _subTaskRepository.GetSubTaskById(subTaskId);
            if (subTask == null)
            {
                //throw new SubTaskNotFoundException();
                return;
            }

            await _subTaskRepository.DeleteAsync(subTask);
        }

        public async Task<SubTaskDto> GetSubTaskForEdit(Guid subTaskId)
        {
            var subTask = await _subTaskRepository.GetSubTaskById(subTaskId);
            if (subTask == null)
            {
                //throw new SubTaskNotFoundException();
                return null;
            }

            var result = ObjectMapper.Map<SubTask, SubTaskDto>(subTask);
            return result;
        }

        public async Task<List<GetAllSprintListOutputDto>> GetAllSprintsAsync(Guid duAnId)
        {
            try
            {
                var result = new List<GetAllSprintListOutputDto>();
                var sprints = await _sprintRepository.GetAllSprintListByDuAnId(duAnId);
                var modules = await _moduleRepository.GetModuleDuAnAsync(duAnId);
                var accounts = await _accountRepository.GetAllAccountsForFilter();
                var subtasks = await _subTaskRepository.GetAllSubTasks();
                var query = from sprint in sprints
                            join sub in subtasks on sprint.Id equals sub.SprintId into subGroup
                            from sub in subGroup.DefaultIfEmpty()
                            join module in modules on sub?.ModuleId equals module.Id into moduleGroup
                            from module in moduleGroup.DefaultIfEmpty()
                            group new { sub, module } by sprint into sprintGroup
                            select new GetAllSprintListOutputDto
                            {
                                Id = sprintGroup.Key.Id,
                                TenSprint = sprintGroup.Key.TenSprint,
                                NgayBatDau = sprintGroup.Key.NgayBatDau,
                                NgayKetThuc = sprintGroup.Key.NgayKetThuc,
                                DuAnId = sprintGroup.Key.DuAnId,
                                TongSoGio = sprintGroup.Sum(g => g.sub?.PM + g.sub?.Dev + g.sub?.Test + g.sub?.BA),
                                TrangThaiSprint = sprintGroup.Key.TrangThaiSprint,
                                Tasks = sprintGroup
                                        .GroupBy(g => g.module)
                                        .Where(moduleGroup => moduleGroup.Key != null) // Lọc các moduleGroup có giá trị hợp lệ
                                        .Select(moduleGroup => new TaskInfo
                                        {
                                            TaskId = moduleGroup.Key?.Id,
                                            TenTask = moduleGroup.Key?.TenModule,
                                            TongSoGio = moduleGroup.Sum(g => g.sub?.PM + g.sub?.Dev + g.sub?.Test + g.sub?.BA),
                                            Subtasks = moduleGroup
                                                        .Where(g => g.sub != null) // Lọc các subtask có giá trị hợp lệ
                                                        .Select(g => new SubtaskInfo
                                                        {
                                                            SubtaskId = g.sub?.Id,
                                                            TenSubtask = g.sub?.TenSubTask,
                                                            PM = g.sub?.PM,
                                                            DEV = g.sub?.Dev,
                                                            BA = g.sub?.BA,
                                                            TESTER = g.sub?.Test,
                                                            TongSoGio = g.sub?.PM + g.sub?.Dev + g.sub?.Test + g.sub?.BA,
                                                            ThoiGianBatDau = g.sub?.ThoiGianBatDau,
                                                            ThoiGianKetThuc = g.sub?.ThoiGianKetThuc,
                                                            TrangThaiSubtask = g.sub?.TrangThaiSubtask,
                                                            NhanSu = g.sub?.NhanSu?.Split(',')
                                                                        .Select(x => new LookupTableDto
                                                                        {
                                                                            Id = Guid.Parse(x),
                                                                            DisplayName = accounts.FirstOrDefault(a => a.Id == Guid.Parse(x))?.UserName
                                                                        }).ToList() ?? new List<LookupTableDto>()
                                                        }).ToList()
                                        }).ToList()
                            };
                result = query.ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<GetAllSprintListOutputDto>> GetAllSprintsThucTienCuaPM(Guid duAnId)
        {
            var congViecs = await _congViecRepository.GetListAsync();
            var result = new List<GetAllSprintListOutputDto>();
            var sprints = await _sprintRepository.GetAllSprintListByDuAnId(duAnId);
            var modules = await _moduleRepository.GetModuleDuAnAsync(duAnId);
            var accounts = await _accountRepository.GetAllAccountsForFilter();
            var subtasks = await _subTaskRepository.GetListAsync();

            var query = from sprint in sprints
                        join sub in subtasks on sprint.Id equals sub.SprintId into subGroup
                        from sub in subGroup.DefaultIfEmpty()
                        join module in modules on sub?.ModuleId equals module.Id into moduleGroup
                        from module in moduleGroup.DefaultIfEmpty()
                        group new { sub, module } by sprint into sprintGroup
                        select new GetAllSprintListOutputDto
                        {
                            Id = sprintGroup.Key.Id,
                            TenSprint = sprintGroup.Key.TenSprint,
                            NgayBatDau = sprintGroup.Key.NgayBatDau,
                            NgayKetThuc = sprintGroup.Key.NgayKetThuc,
                            DuAnId = sprintGroup.Key.DuAnId,
                            TongSoGio = sprintGroup.Sum(g => (g.sub?.PM ?? 0) + (g.sub?.Dev ?? 0) + (g.sub?.Test ?? 0) + (g.sub?.BA ?? 0)),
                            TrangThaiSprint = sprintGroup.Key.TrangThaiSprint,
                            Tasks = sprintGroup
                                    .GroupBy(g => g.module)
                                    .Where(moduleGroup => moduleGroup.Key != null) // Lọc các moduleGroup có giá trị hợp lệ
                                    .Select(moduleGroup => new TaskInfo
                                    {
                                        TaskId = moduleGroup.Key?.Id,
                                        TenTask = moduleGroup.Key?.TenModule,
                                        TongSoGio = moduleGroup.Sum(g => (g.sub?.PM ?? 0) + (g.sub?.Dev ?? 0) + (g.sub?.Test ?? 0) + (g.sub?.BA ?? 0)),
                                        Subtasks = moduleGroup
                                                    .Where(g => g.sub != null) // Lọc các subtask có giá trị hợp lệ
                                                    .Select(g => new SubtaskInfo
                                                    {
                                                        SubtaskId = g.sub?.Id,
                                                        TenSubtask = g.sub?.TenSubTask,
                                                        PM = g.sub?.PM,
                                                        DEV = g.sub?.Dev,
                                                        BA = g.sub?.BA,
                                                        TESTER = g.sub?.Test,
                                                        TongSoGio = congViecs
                                                                    .Where(cv => cv.SubtaskId == g.sub?.Id)
                                                                    .Sum(cv => cv.SoGio),
                                                        ThoiGianBatDau = g.sub?.ThoiGianBatDau,
                                                        ThoiGianKetThuc = g.sub?.ThoiGianKetThuc,
                                                        TrangThaiSubtask = g.sub?.TrangThaiSubtask,
                                                        NhanSu = g.sub?.NhanSu?.Split(',')
                                                                    .Select(x => new LookupTableDto
                                                                    {
                                                                        Id = Guid.Parse(x),
                                                                        DisplayName = accounts.FirstOrDefault(a => a.Id == Guid.Parse(x))?.UserName
                                                                    }).ToList() ?? new List<LookupTableDto>()
                                                    }).ToList()
                                    }).ToList()
                        };

            result = query.ToList();

            return result;
        }
    }
}
