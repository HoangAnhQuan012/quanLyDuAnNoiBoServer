using quanLyDuAnNoiBo.Accounts;
using quanLyDuAnNoiBo.BodQuanLyDuAn;
using quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos;
using quanLyDuAnNoiBo.ChamCongRepo;
using quanLyDuAnNoiBo.CongViec;
using quanLyDuAnNoiBo.Global.Dtos;
using quanLyDuAnNoiBo.NhanVienQuanLyDuAn.Dtos;
using quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.NhanVienQuanLyDuAn
{
    public class NhanVienQuanLyDuAn : quanLyDuAnNoiBoAppService
    {
        private readonly IQuanLyDuAnRepository _quanLyDuAnRepository;
        private readonly ISprintDuAnRepository _sprintRepository;
        private readonly IModuleDuAnRepository _moduleRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IChamCongRepository _chamCongRepository;
        private readonly ICongViecRepository _congViecRepository;
        public NhanVienQuanLyDuAn(
            ISprintDuAnRepository sprintRepository,
            IModuleDuAnRepository moduleRepository,
            ISubTaskRepository subTaskRepository,
            IAccountRepository accountRepository,
            IQuanLyDuAnRepository quanLyDuAnRepository,
            IChamCongRepository chamCongRepository,
            ICongViecRepository congViecRepository
            )
        {
            _sprintRepository = sprintRepository;
            _moduleRepository = moduleRepository;
            _subTaskRepository = subTaskRepository;
            _accountRepository = accountRepository;
            _quanLyDuAnRepository = quanLyDuAnRepository;
            _chamCongRepository = chamCongRepository;
            _congViecRepository = congViecRepository;
        }

        public async Task<List<GetAllSprintListOutputDto>> GetAllSprintsKeHoachAsync(Guid duAnId)
        {
            try
            {
                var currentUserId = CurrentUser.Id;
                var result = new List<GetAllSprintListOutputDto>();
                var sprints = await _sprintRepository.GetAllSprintListByDuAnId(duAnId);
                var modules = await _moduleRepository.GetModuleDuAnAsync(duAnId);
                var accounts = await _accountRepository.GetAllAccountsForFilter();
                var subtasks = await _subTaskRepository.GetSubtaskByUserId((Guid)currentUserId);
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

        public async Task<PagedResultDto<GetAllDuAnDto>> GetAllDuAn(BoDQuanLyDuAn.Dtos.GetAllDuAnInputDto input)
        {
            var currentUserId = CurrentUser.Id;
            var duAns = await _quanLyDuAnRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword, input.KhachHang, input.TrangThai, input.QuanLyDuAnId);
            var sprints = await _sprintRepository.GetListAsync();
            var subtasks = await _subTaskRepository.GetListAsync();

            // Join 3 tables and group by duAn to get distinct NhanSu count for each duAn
            var nhanSuCounts = duAns.GroupJoin(
                sprints,
                duAn => duAn.Id,
                sprint => sprint.DuAnId,
                (duAn, duAnSprints) => new { duAn, duAnSprints }
            ).SelectMany(
                x => x.duAnSprints.DefaultIfEmpty(),
                (x, sprint) => new { x.duAn, sprint }
            ).GroupJoin(
                subtasks,
                x => x.sprint != null ? x.sprint.Id : Guid.Empty, // Ensure sprint is not null
                subtask => subtask.SprintId,
                (x, sprintSubtasks) => new { x.duAn, sprintSubtasks }
            ).GroupBy(x => x.duAn.Id)
            .Select(g => new
            {
                DuAnId = g.Key,
                NhanSuCount = g.SelectMany(x => x.sprintSubtasks)
                    .SelectMany(subtask => subtask.NhanSu != null ? subtask.NhanSu.Split(',') : new string[0])
                    .Distinct()
                    .Count()
            }).ToList();

            // Convert nhanSuCounts to a dictionary for easy lookup
            var nhanSuCountDict = nhanSuCounts.ToDictionary(x => x.DuAnId, x => x.NhanSuCount);

            var sprintSubs = sprints.Where(e =>
                                subtasks.Any(w => w.SprintId == e.Id &&
                                w.NhanSu != null ? w.NhanSu.Split(',').Contains(currentUserId.ToString()) : false
                                )).ToList();

            var result = duAns.Where(e =>
                            sprintSubs.Any(w => w.DuAnId == e.Id)
                                ).Select(s => new GetAllDuAnDto
                                {
                                    Id = s.Id,
                                    TenDuAn = s.TenDuAn,
                                    KhachHang = s.KhachHang,
                                    TrangThai = s.TrangThai,
                                    MaDuAn = s.MaDuAn,
                                    NgayBatDau = s.NgayBatDau,
                                    NgayKetThuc = s.NgayKetThuc,
                                    NhanSu = nhanSuCountDict.ContainsKey(s.Id) ? nhanSuCountDict[s.Id] : 0
                                }).ToList();

            return new PagedResultDto<GetAllDuAnDto>()
            {
                TotalCount = result.Count,
                Items = result
            };
        }

        public async Task<List<GetAllSprintListOutputDto>> GetAllSprintsThucTien(Guid duAnId)
        {
            var congViecs = await _congViecRepository.GetListAsync();
            var currentUserId = CurrentUser.Id;
            var result = new List<GetAllSprintListOutputDto>();
            var sprints = await _sprintRepository.GetAllSprintListByDuAnId(duAnId);
            var modules = await _moduleRepository.GetModuleDuAnAsync(duAnId);
            var accounts = await _accountRepository.GetAllAccountsForFilter();
            var subtasks = await _subTaskRepository.GetSubtaskByUserId((Guid)currentUserId);

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
