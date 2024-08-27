using quanLyDuAnNoiBo.Accounts;
using quanLyDuAnNoiBo.BodQuanLyDuAn;
using quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos;
using quanLyDuAnNoiBo.DanhMuc;
using quanLyDuAnNoiBo.DuAn;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.Global.Dtos;
using quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.BoDQuanLyDuAn
{
    public class BoDQuanLyDuAnAppService : quanLyDuAnNoiBoAppService, IBoDQuanLyDuAnAppService
    {
        private readonly IQuanLyDuAnRepository _quanLyDuAnRepository;
        private readonly IChiTietDuAnRepository _chiTietDuAnRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly ISprintDuAnRepository _sprintDuAnRepository;
        private readonly IModuleDuAnRepository _moduleDuAnRepository;
        private readonly ISubTask_NhanVienRepository _subTask_NhanVienRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IChucDanhRepository _chucDanhRepository;
        public BoDQuanLyDuAnAppService(
            IQuanLyDuAnRepository quanLyDuAnRepository,
            IChiTietDuAnRepository chiTietDuAnRepository,
            ISubTaskRepository subTaskRepository,
            ISprintDuAnRepository sprintDuAnRepository,
            IModuleDuAnRepository moduleDuAnRepository,
            ISubTask_NhanVienRepository subTask_NhanVienRepository,
            IAccountRepository accountRepository,
            IChucDanhRepository chucDanhRepository
            )
        {
            this._quanLyDuAnRepository = quanLyDuAnRepository;
            this._chiTietDuAnRepository = chiTietDuAnRepository;
            this._subTaskRepository = subTaskRepository;
            this._sprintDuAnRepository = sprintDuAnRepository;
            this._moduleDuAnRepository = moduleDuAnRepository;
            this._subTask_NhanVienRepository = subTask_NhanVienRepository;
            this._accountRepository = accountRepository;
            this._chucDanhRepository = chucDanhRepository;
        }

        public async Task<bool> CreateDuAnAsync(CreateDuAnDto input)
        {
            if (input == null)
            {
                return false;
            }

            var checkExist = await _quanLyDuAnRepository.CheckExistDuAn(input.MaDuAn, input.Id);
            if (checkExist)
            {
                throw new DuAnAlrealdyExistsException();
            }

            var quanLyDuAn = ObjectMapper.Map<CreateDuAnDto, QuanLyDuAn>(input);
            quanLyDuAn.TrangThai = DuAn.TrangThaiDuAnConsts.KhoiTao;
            await _quanLyDuAnRepository.InsertAsync(quanLyDuAn);
            var chiTietDuAn = new ChiTietDuAn
            {
                DuAnId = quanLyDuAn.Id,
                LuongCoSo = input.LuongCoSo,
                BanGiaoDungHan = input.BanGiaoDungHan,
                HieuQuaSuDungNhanSu = input.HieuQuaSuDungNhanSu,
                NoLucKhacPhucLoi = input.NoLucKhacPhucLoi,
                MucDoHaiLongCuaKhachHang = input.MucDoHaiLongCuaKhachHang,
                MucDoloiBiPhatHien = input.MucDoloiBiPhatHien,
                MucDoLoiUAT = input.MucDoLoiUAT,
                TyLeThucHienDungQuyTrinh = input.TyLeThucHienDungQuyTrinh,
                NangSuatTaoTestcase = input.NangSuatTaoTestcase,
                NangSuatThuThiTestcase = input.NangSuatThuThiTestcase,
                NangSuatDev = input.NangSuatDev,
                NangSuatVietUT = input.NangSuatVietUT,
                NangSuatThucThiUT = input.NangSuatThucThiUT,
                NangSuatBA = input.NangSuatBA,
                GiaTriHopDong = input.GiaTriHopDong,
                ChiPhiABH = input.ChiPhiABH,
                ChiPhiOpexPhanBo = input.ChiPhiOpexPhanBo,
                ChiPhiLuongDuKien = input.ChiPhiLuongDuKien,
                ChiPhiLuongThucTe = input.ChiPhiLuongThucTe,
                LaiDuKien = input.LaiDuKien,
                LaiThucTe = input.LaiThucTe
            };

            await _chiTietDuAnRepository.InsertAsync(chiTietDuAn);

            if (input.Module != null)
            {
                foreach (var item in input.Module)
                {
                    var moduleDuAn = new ModuleDuAn
                    {
                        DuAnId = quanLyDuAn.Id,
                        TenModule = item.TenModule,
                        PM = item.PM,
                        Dev = item.Dev,
                        Test = item.Test,
                        BA = item.BA,
                        TongThoiGian = item.TongThoiGian
                    };
                    await _moduleDuAnRepository.InsertAsync(moduleDuAn);
                }
            }

            return true;
        }

        public async Task<PagedResultDto<GetAllDuAnDto>> GetAllAsync(GetAllDuAnInputDto input)
        {
            // Fetch the filtered list of projects (DuAn)
            var quanLyDuAn = await _quanLyDuAnRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword, input.KhachHang, input.TrangThai, input.QuanLyDuAnId);
            var ids = quanLyDuAn.Select(s => s.Id).ToList();
            var totalCount = quanLyDuAn.Count();

            // Fetch all projects, sprints, and subtasks
            var duAns = await _quanLyDuAnRepository.GetListAsync();
            var sprints = await _sprintDuAnRepository.GetListAsync();
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

            // Map the projects to the DTO
            var items = ObjectMapper.Map<List<QuanLyDuAn>, List<GetAllDuAnDto>>(quanLyDuAn);
            foreach (var item in items)
            {
                // Ensure item.Id is not null before using it as a key
                if (item.Id.HasValue)
                {
                    item.NhanSu = nhanSuCountDict.ContainsKey(item.Id.Value) ? nhanSuCountDict[item.Id.Value] : 0;
                }
                else
                {
                    item.NhanSu = 0;
                }
            }

            return new PagedResultDto<GetAllDuAnDto>()
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<GetDuAnDto> GetDuAnByIdAsync(Guid id)
        {
            var quanLyDuAn = await _quanLyDuAnRepository.GetDuAnAsync(id);
            var chiTietDuAn = await _chiTietDuAnRepository.GetChiTietDuAnAsync(id);
            //var subTask = await _subTaskRepository.GetByDuAnId(id);
            //var sprintDuAn = await _sprintDuAnRepository.GetByDuAnId(id);
            var moduleDuAn = await _moduleDuAnRepository.GetModuleDuAnAsync(id);
            //var subTask_NhanVien = await _subTask_NhanVienRepository.GetByDuAnId(id);

            var result = ObjectMapper.Map<QuanLyDuAn, GetDuAnDto>(quanLyDuAn);
            result.GiaTriHD = quanLyDuAn.GiaTriHopDong;
            result.DuAnId = quanLyDuAn.Id;
            result.LuongCoSo = chiTietDuAn.LuongCoSo;
            result.BanGiaoDungHan = chiTietDuAn.BanGiaoDungHan;
            result.HieuQuaSuDungNhanSu = chiTietDuAn.HieuQuaSuDungNhanSu;
            result.NoLucKhacPhucLoi = chiTietDuAn.NoLucKhacPhucLoi;
            result.MucDoHaiLongCuaKhachHang = chiTietDuAn.MucDoHaiLongCuaKhachHang;
            result.MucDoloiBiPhatHien = chiTietDuAn.MucDoloiBiPhatHien;
            result.MucDoLoiUAT = chiTietDuAn.MucDoLoiUAT;
            result.TyLeThucHienDungQuyTrinh = chiTietDuAn.TyLeThucHienDungQuyTrinh;
            result.NangSuatTaoTestcase = chiTietDuAn.NangSuatTaoTestcase;
            result.NangSuatThuThiTestcase = chiTietDuAn.NangSuatThuThiTestcase;
            result.NangSuatDev = chiTietDuAn.NangSuatDev;
            result.NangSuatVietUT = chiTietDuAn.NangSuatVietUT;
            result.NangSuatThucThiUT = chiTietDuAn.NangSuatThucThiUT;
            result.NangSuatBA = chiTietDuAn.NangSuatBA;
            result.GiaTriHopDong = chiTietDuAn.GiaTriHopDong;
            result.ChiPhiABH = chiTietDuAn.ChiPhiABH;
            result.ChiPhiOpexPhanBo = chiTietDuAn.ChiPhiOpexPhanBo;
            result.ChiPhiLuongDuKien = chiTietDuAn.ChiPhiLuongDuKien;
            result.ChiPhiLuongThucTe = chiTietDuAn.ChiPhiLuongThucTe;
            result.LaiDuKien = chiTietDuAn.LaiDuKien;
            result.LaiThucTe = chiTietDuAn.LaiThucTe;
            if (moduleDuAn != null)
            {
                result.Module = ObjectMapper.Map<List<ModuleDuAn>, List<ModuleDto>>(moduleDuAn);
            }

            return result;
        }

        public async Task<bool> UpdateDuAnAsync(UpdateDuAnDto input)
        {
            if (input.Id != null)
            {
                var quanLyDuAn = await _quanLyDuAnRepository.GetDuAnAsync(input.Id);
                var chiTietDuAn = await _chiTietDuAnRepository.GetChiTietDuAnAsync(input.Id);
                var moduleDuAn = await _moduleDuAnRepository.GetModuleDuAnAsync(input.Id);
                if (quanLyDuAn == null)
                {
                    return false;
                }

                var checkExist = await _quanLyDuAnRepository.CheckExistDuAn(input.MaDuAn, input.Id);
                if (checkExist)
                {
                    throw new DuAnAlrealdyExistsException();
                }

                quanLyDuAn.MaDuAn = input.MaDuAn;
                quanLyDuAn.TenDuAn = input.TenDuAn;
                quanLyDuAn.GiaTriHopDong = input.GiaTriHD;
                quanLyDuAn.SoHopDong = input.SoHopDong;
                quanLyDuAn.NoiDungPhatTrien = input.NoiDungPhatTrien;
                quanLyDuAn.KhachHang = input.KhachHang;
                quanLyDuAn.NgayBatDau = input.NgayBatDau.Value.ToLocalTime();
                quanLyDuAn.NgayKetThuc = input.NgayKetThuc.ToLocalTime();
                quanLyDuAn.QuyTrinhPhatTrien = input.QuyTrinhPhatTrien;
                quanLyDuAn.CongNgheSuDung = input.CongNgheSuDung;
                quanLyDuAn.UngDungDauCuoi = input.UngDungDauCuoi;
                quanLyDuAn.QuanLyDuAnId = input.QuanLyDuAnId;
                await _quanLyDuAnRepository.UpdateAsync(quanLyDuAn);

                chiTietDuAn.LuongCoSo = input.LuongCoSo;
                chiTietDuAn.BanGiaoDungHan = input.BanGiaoDungHan;
                chiTietDuAn.HieuQuaSuDungNhanSu = input.HieuQuaSuDungNhanSu;
                chiTietDuAn.NoLucKhacPhucLoi = input.NoLucKhacPhucLoi;
                chiTietDuAn.MucDoHaiLongCuaKhachHang = input.MucDoHaiLongCuaKhachHang;
                chiTietDuAn.MucDoloiBiPhatHien = input.MucDoloiBiPhatHien;
                chiTietDuAn.MucDoLoiUAT = input.MucDoLoiUAT;
                chiTietDuAn.TyLeThucHienDungQuyTrinh = input.TyLeThucHienDungQuyTrinh;
                chiTietDuAn.NangSuatTaoTestcase = input.NangSuatTaoTestcase;
                chiTietDuAn.NangSuatThuThiTestcase = input.NangSuatThuThiTestcase;
                chiTietDuAn.NangSuatDev = input.NangSuatDev;
                chiTietDuAn.NangSuatVietUT = input.NangSuatVietUT;
                chiTietDuAn.NangSuatThucThiUT = input.NangSuatThucThiUT;
                chiTietDuAn.NangSuatBA = input.NangSuatBA;
                chiTietDuAn.GiaTriHopDong = input.GiaTriHopDong;
                chiTietDuAn.ChiPhiABH = input.ChiPhiABH;
                chiTietDuAn.ChiPhiOpexPhanBo = input.ChiPhiOpexPhanBo;
                chiTietDuAn.ChiPhiLuongDuKien = input.ChiPhiLuongDuKien;
                chiTietDuAn.ChiPhiLuongThucTe = input.ChiPhiLuongThucTe;
                chiTietDuAn.LaiDuKien = input.LaiDuKien;
                chiTietDuAn.LaiThucTe = input.LaiThucTe;
                await _chiTietDuAnRepository.UpdateAsync(chiTietDuAn);

                //if (moduleDuAn != null)
                //{
                //    foreach (var item in moduleDuAn)
                //    {
                //        var module = input.Module.FirstOrDefault(f => f.Id == item.Id);
                //        if (module != null)
                //        {
                //            item.TenModule = module.TenModule;
                //            item.PM = module.PM;
                //            item.Dev = module.Dev;
                //            item.Test = module.Test;
                //            item.BA = module.BA;
                //            item.TongThoiGian = module.TongThoiGian;
                //            await _moduleDuAnRepository.UpdateAsync(item);
                //        }
                //        else
                //        {
                //            await _moduleDuAnRepository.InsertAsync(item);
                //        }
                //    }
                //}
                if (input.Module != null)
                {
                    foreach (var item in input.Module)
                    {
                        if (item.Id != null)
                        {
                            var module = moduleDuAn.FirstOrDefault(f => f.Id == item.Id);
                            if (module != null)
                            {
                                module.TenModule = item.TenModule;
                                module.PM = item.PM;
                                module.Dev = item.Dev;
                                module.Test = item.Test;
                                module.BA = item.BA;
                                module.TongThoiGian = item.TongThoiGian;
                                await _moduleDuAnRepository.UpdateAsync(module);
                            }
                        }
                        else
                        {
                            var insertModule = new ModuleDuAn
                            {
                                DuAnId = quanLyDuAn.Id,
                                TenModule = item.TenModule,
                                PM = item.PM,
                                Dev = item.Dev,
                                Test = item.Test,
                                BA = item.BA,
                                TongThoiGian = item.TongThoiGian
                            };
                            await _moduleDuAnRepository.InsertAsync(insertModule);
                        }
                    }
                }

                // Nếu tồn tại module trong db nhưng không tồn tại trong input thì lọc ra những bản ghi khác với input rồi xóa đi
                if (moduleDuAn != null)
                {
                    foreach (var item in moduleDuAn)
                    {
                        if (input.Module.FirstOrDefault(f => f.Id == item.Id) == null)
                        {
                            await _moduleDuAnRepository.DeleteAsync(item);
                        }
                    }
                }


                return true;
            }

            return false;

        }

        public async Task<bool> DeleteDuAnAsync(Guid id)
        {
            var quanLyDuAn = await _quanLyDuAnRepository.GetDuAnAsync(id);
            var chiTietDuAn = await _chiTietDuAnRepository.GetChiTietDuAnAsync(id);
            var moduleDuAn = await _moduleDuAnRepository.GetModuleDuAnAsync(id);
            if (quanLyDuAn == null)
            {
                return false;
            }

            await _quanLyDuAnRepository.DeleteAsync(quanLyDuAn);
            await _chiTietDuAnRepository.DeleteAsync(chiTietDuAn);
            if (moduleDuAn != null)
            {
                foreach (var item in moduleDuAn)
                {
                    await _moduleDuAnRepository.DeleteAsync(item);
                }
            }
            return true;
        }

        public async Task ThayDoiTrangThaiDuAn(Guid id, TrangThaiDuAnConsts trangThai)
        {
            var duAn = await _quanLyDuAnRepository.GetDuAnAsync(id);
            if (duAn != null)
            {
                duAn.TrangThai = trangThai;
                await _quanLyDuAnRepository.UpdateAsync(duAn);
                switch (trangThai)
                {
                    case TrangThaiDuAnConsts.DangThucHien:
                        duAn.NgayBatDau = DateTime.Now;
                        break;

                    case TrangThaiDuAnConsts.KetThuc:
                        duAn.NgayKetThuc = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }
        }

        public async Task<List<GetAllSprintListOutputDto>> GetAllSprintsPheDuyetVaChuaPheDuyetAsync(Guid duAnId)
        {
            try
            {
                var result = new List<GetAllSprintListOutputDto>();
                var sprints = await _sprintDuAnRepository.GetAllSprintListPheDuyetVaChuaPheDuyetByDuAnId(duAnId);
                var modules = await _moduleDuAnRepository.GetModuleDuAnAsync(duAnId);
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

        public async Task<bool> AcceptSprint(Guid sprintId)
        {
            var sprint = await _sprintDuAnRepository.GetSprintById(sprintId);
            if (sprint == null)
            {
                return false;
            }

            sprint.TrangThaiSprint = TrangThaiSprintConsts.DaPheDuyet;
            await _sprintDuAnRepository.UpdateAsync(sprint);

            var subtasks = await _subTaskRepository.GetSubtaksBySprintId(sprintId);
            if (subtasks.Count > 0)
            {
                foreach(var subtask in subtasks)
                {
                    subtask.TrangThaiSubtask = TrangThaiSubtaskConsts.DaDuocPhanCong;
                    await _subTaskRepository.UpdateAsync(subtask);
                }
            }
            return true;
        }
    }
}
