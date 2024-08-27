using quanLyDuAnNoiBo.Accounts;
using quanLyDuAnNoiBo.BodQuanLyDuAn;
using quanLyDuAnNoiBo.ChamCong.Dtos;
using quanLyDuAnNoiBo.ChamCongRepo;
using quanLyDuAnNoiBo.CongViec;
using quanLyDuAnNoiBo.Global;
using quanLyDuAnNoiBo.KieuViec;
using quanLyDuAnNoiBo.PmQuanLyDuAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.ChamCong
{
    public class PMDuyetChamCongAppService : quanLyDuAnNoiBoAppService
    {
        private readonly IPmQuanLyDuAnRepository _pmQuanLyDuAnRepository;
        private readonly IChamCongRepository _chamCongRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICongViecRepository _congViecRepository;
        private readonly ISprintDuAnRepository _sprintDuAnRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly IKieuViecRepository _kieuViecRepository;
        private readonly ITaiLieuDinhKemCongViecRepository _taiLieuDinhKemCongViecRepository;
        public PMDuyetChamCongAppService(
            IPmQuanLyDuAnRepository pmQuanLyDuAnRepository,
            IChamCongRepository chamCongRepository,
            IAccountRepository accountRepository,
            ICongViecRepository congViecRepository,
            ISprintDuAnRepository sprintDuAnRepository,
            ISubTaskRepository subTaskRepository,
            IKieuViecRepository kieuViecRepository,
            ITaiLieuDinhKemCongViecRepository taiLieuDinhKemCongViecRepository
            )
        {
            _pmQuanLyDuAnRepository = pmQuanLyDuAnRepository;
            _chamCongRepository = chamCongRepository;
            _accountRepository = accountRepository;
            _congViecRepository = congViecRepository;
            _sprintDuAnRepository = sprintDuAnRepository;
            _subTaskRepository = subTaskRepository;
            _kieuViecRepository = kieuViecRepository;
            _taiLieuDinhKemCongViecRepository = taiLieuDinhKemCongViecRepository;
        }

        public async Task<PagedResultDto<GetAllDuAnPMDuyetChamCongOutputDto>> GetAllDuAnPMDuyetChamCong(GetAllPMDuyetChamCongInputDto input)
        {
            if (input == null)
            {
                return null;
            }

            var currentUserId = CurrentUser.Id;

            if (currentUserId != null)
            {
                string keyword = RegexFormat(input.Keyword);
                var duAns = await _pmQuanLyDuAnRepository.GetListDuAnByInput((Guid)currentUserId, input.Keyword, input.StartTime, input.EndTime, input.Sorting, input.SkipCount, input.MaxResultCount);
                var chamCongs = await _chamCongRepository.GetListAsync();
                var query = duAns.Where(e => chamCongs.Any(w => w.DuAnId == e.Id)).ToList();

                var items = new List<GetAllDuAnPMDuyetChamCongOutputDto>();

                foreach (var item in query)
                {
                    var output = new GetAllDuAnPMDuyetChamCongOutputDto
                    {
                        DuAnId = item.Id,
                        TenDuAn = item.TenDuAn,
                        NhanSuChoDuyetChamCong = chamCongs.Where(e => e.TrangThaiChamCong == TrangThaiChamCongConsts.ChoDuyet && e.DuAnId == item.Id).Count(),
                        TongThoiGianChoDuyet = chamCongs.Where(e => e.TrangThaiChamCong == TrangThaiChamCongConsts.ChoDuyet && e.DuAnId == item.Id).Sum(e => e.ThoiGianChamCong),
                        TongThoiGianDaThucHien = chamCongs.Where(e => e.TrangThaiChamCong == TrangThaiChamCongConsts.DaDuyet && e.DuAnId == item.Id).Sum(e => e.ThoiGianChamCong),
                        TrangThaiPheDuyet = chamCongs.Any(e => e.TrangThaiChamCong == TrangThaiChamCongConsts.ChoDuyet && e.DuAnId == item.Id) ? TrangThaiPheDuyetChamCongConsts.ChoDuyet : TrangThaiPheDuyetChamCongConsts.DaDuyet
                    };

                    items.Add(output);
                }

                return new PagedResultDto<GetAllDuAnPMDuyetChamCongOutputDto>
                {
                    TotalCount = items.Count,
                    Items = items
                };
            }

            return null;
        }

        public async Task<PagedResultDto<GetListChamCongByDuAnIdOutputDto>> GetListChamCongByDuAnId(GetListChamCongByDuAnIdInputDto input)
        {
            if (input == null)
            {
                return null;
            }

            var chamCongs = await _chamCongRepository.GetListChamCongDuAn(input.DuAnId, input.StartTime, input.EndTime, input.SkipCount, input.MaxResultCount);
            var items = new List<GetListChamCongByDuAnIdOutputDto>();

            // Nhóm chấm công theo NhanVienId
            var chamCongGroupedByNhanVien = chamCongs.GroupBy(cc => cc.NhanVienId);

            foreach (var chamCongGroup in chamCongGroupedByNhanVien)
            {
                var output = new GetListChamCongByDuAnIdOutputDto
                {
                    // ChamCongId = chamCongGroup.First().Id,  // Lấy ChamCongId đầu tiên của nhóm này
                    NhanVienId = chamCongGroup.Key,
                    TenNhanVien = await _accountRepository.GetUsernameById(chamCongGroup.Key),
                    ListChamCong = new List<DataChamCong>()
                };

                // Tạo dictionary để tra cứu nhanh chấm công theo ngày
                var chamCongDict = chamCongGroup.ToDictionary(
                    cc => cc.NgayChamCong.Date,
                    cc => new DataChamCong
                    {
                        ChamCongId = cc.Id,
                        NgayChamCong = cc.NgayChamCong,
                        ThoiGianChamCong = cc.ThoiGianChamCong,
                        TrangThaiChamCong = cc.TrangThaiChamCong,
                        NghiNuaNgay = cc.ThoiGianChamCong == 4,
                        IsDayOff = false
                    });

                // Duyệt qua tất cả các ngày từ StartTime đến EndTime
                for (var ngay = input.StartTime.Date; ngay <= input.EndTime.Date; ngay = ngay.AddDays(1))
                {
                    bool isDayOff = ngay.DayOfWeek == DayOfWeek.Saturday || ngay.DayOfWeek == DayOfWeek.Sunday;

                    if (chamCongDict.ContainsKey(ngay))
                    {
                        var dailyChamCong = chamCongDict[ngay];
                        dailyChamCong.IsDayOff = isDayOff;
                        output.ListChamCong.Add(dailyChamCong);
                    }
                    else
                    {
                        output.ListChamCong.Add(new DataChamCong
                        {
                            ChamCongId = Guid.Empty,
                            NgayChamCong = ngay,
                            ThoiGianChamCong = 0,
                            TrangThaiChamCong = null,
                            NghiNuaNgay = false,
                            IsDayOff = isDayOff
                        });
                    }
                }

                items.Add(output);
            }

            return new PagedResultDto<GetListChamCongByDuAnIdOutputDto>
            {
                TotalCount = items.Count,
                Items = items.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), e => e.TenNhanVien.Contains(input.Keyword)).ToList()
            };
        }

        public async Task<List<GetThongTinChamCongChiTietOutputDto>> GetThongTinChamCongChiTiet(Guid nhanVienId, DateTime ngayChamCong)
        {
            try
            {
                var chamCongs = await _chamCongRepository.GetListChamCongByNhanVienId(nhanVienId, ngayChamCong);
                var congViecs = await _congViecRepository.GetListAsync();
                var account = await _accountRepository.FindByIdAsync(nhanVienId);
                var sprints = await _sprintDuAnRepository.GetListAsync();
                var subTasks = await _subTaskRepository.GetListAsync();
                var kieuViecs = await _kieuViecRepository.GetListAsync();
                var taiLieuDinhKemCongViecs = await _taiLieuDinhKemCongViecRepository.GetListAsync();

                var query = from chamCong in chamCongs
                            join congViec in congViecs on chamCong.Id equals congViec.ChamCongId into chamCongCongViecs
                            from congViec in chamCongCongViecs.DefaultIfEmpty()
                            join sprint in sprints on chamCong.SprintId equals sprint.Id into chamCongSprints
                            from sprint in chamCongSprints.DefaultIfEmpty()
                            join subTask in subTasks on congViec?.SubtaskId equals subTask.Id into congViecSubTasks
                            from subTask in congViecSubTasks.DefaultIfEmpty()
                            join kieuViec in kieuViecs on congViec?.KieuViecId equals kieuViec.Id into congViecKieuViecs
                            from kieuViec in congViecKieuViecs.DefaultIfEmpty()
                            join taiLieu in taiLieuDinhKemCongViecs on congViec?.Id equals taiLieu.CongViecId into congViecTaiLieus
                            from taiLieu in congViecTaiLieus.DefaultIfEmpty()
                            select new GetThongTinChamCongChiTietOutputDto
                            {
                                ChamCongId = chamCong.Id,
                                NhanVienId = chamCong.NhanVienId,
                                TenNhanVien = account.UserName,
                                NgayChamCong = chamCong.NgayChamCong,
                                TenSprint = sprint?.TenSprint,
                                TenTask = chamCong.TenTask,
                                TenSubtask = subTask?.TenSubTask,
                                KieuViec = kieuViec?.TenKieuViec,
                                LoaiHinh = congViec != null ? GlobalModel.SortedLoaiHinhDuAn[(int)congViec.LoaiHinh] : null,
                                ThoiGian = chamCong.ThoiGianChamCong,
                                TrangThaiSubtask = subTask?.TrangThaiSubtask,
                                TrangThaiChamCong = chamCong.TrangThaiChamCong,
                                NgayDuyetChamCong = chamCong.NgayDuyetChamCong,
                                GhiChu = congViec?.GhiChu,
                                ChamCongDinhKemFiles = congViec != null && taiLieu != null ? new List<ChamCongDinhKemFiles>
                        {
                            new ChamCongDinhKemFiles
                            {
                                TenDinhKem = taiLieu.TenDinhKem,
                                FileDinhKem = taiLieu.FileDinhKem
                            }
                        } : new List<ChamCongDinhKemFiles>()
                            };

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> DuyetChamCongChiTiet(DuyetChamCongListInputDto input)
        {
            if (input == null)
            {
                return false;
            }

            foreach(var item in input.ChamCongIds)
            {
                var chamCong = await _chamCongRepository.GetChamCongById(item);
                if (chamCong != null)
                {
                    if (chamCong.TrangThaiChamCong == TrangThaiChamCongConsts.ChoDuyet)
                    {
                        chamCong.TrangThaiChamCong = TrangThaiChamCongConsts.DaDuyet;
                        chamCong.NgayDuyetChamCong = DateTime.Now;
                        await _chamCongRepository.UpdateAsync(chamCong);
                    }

                    // Kiểm tra xem subtask ứng với chamCong này đã hoàn thành chưa
                    var congViecs = await _congViecRepository.GetCongViecsByChamCongId(chamCong.Id);
                    if (congViecs != null)
                    {
                        foreach (var congViec in congViecs)
                        {
                            var subTask = await _subTaskRepository.GetSubTaskById(congViec.SubtaskId);
                            if (subTask != null)
                            {
                                if (subTask.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh)
                                {
                                    subTask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.DaHoanThanh;
                                    await _subTaskRepository.UpdateAsync(subTask);
                                }
                            }
                        }
                    }

                }
            }

            return true;
        }

        public async Task<bool> DuyetChamCongTheoDanhSachNV(DuyetChamCongDanhSachNhanVien input)
        {
            if (input == null)
            {
                return false;
            }

            // Duyệt chấm công theo khoảng startDate - endDate
            var chamCongs = await _chamCongRepository.GetListChamCongByTheoKhoang(input.StartDate, input.EndDate);
            // lấy ra các bản ghi chấm công để cập nhật dựa trên nhanVienIds
            foreach(var item in input.ChamCongs)
            {
                var chamCong = await _chamCongRepository.GetChamCongById(item.ChamCongId);
                if (chamCong != null)
                {
                    if (chamCong.TrangThaiChamCong == TrangThaiChamCongConsts.ChoDuyet)
                    {
                        chamCong.TrangThaiChamCong = TrangThaiChamCongConsts.DaDuyet;
                        chamCong.NgayDuyetChamCong = DateTime.Now;
                        await _chamCongRepository.UpdateAsync(chamCong);
                    }

                    // Kiểm tra xem subtask ứng với chamCong này đã hoàn thành chưa
                    var congViecs = await _congViecRepository.GetCongViecsByChamCongId(chamCong.Id);
                    if (congViecs != null)
                    {
                        foreach (var congViec in congViecs)
                        {
                            var subTask = await _subTaskRepository.GetSubTaskById(congViec.SubtaskId);
                            if (subTask != null)
                            {
                                if (subTask.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh)
                                {
                                    subTask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.DaHoanThanh;
                                    await _subTaskRepository.UpdateAsync(subTask);
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        public async Task<bool> TuChoiChamCongChiTiet(DuyetChamCongListInputDto input)
        {
            if (input == null)
            {
                return false;
            }

            foreach (var item in input.ChamCongIds)
            {
                var chamCong = await _chamCongRepository.GetChamCongById(item);
                if (chamCong != null)
                {
                    if (chamCong.TrangThaiChamCong == TrangThaiChamCongConsts.ChoDuyet)
                    {
                        chamCong.TrangThaiChamCong = TrangThaiChamCongConsts.TuChoi;
                        chamCong.NgayDuyetChamCong = DateTime.Now;
                        await _chamCongRepository.UpdateAsync(chamCong);
                    }

                    // Kiểm tra xem subtask ứng với chamCong này đã hoàn thành chưa
                    var congViecs = await _congViecRepository.GetCongViecsByChamCongId(chamCong.Id);
                    if (congViecs != null)
                    {
                        foreach (var congViec in congViecs)
                        {
                            var subTask = await _subTaskRepository.GetSubTaskById(congViec.SubtaskId);
                            if (subTask != null)
                            {
                                if (subTask.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh)
                                {
                                    subTask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.ChuaHoanThanh;
                                    await _subTaskRepository.UpdateAsync(subTask);
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        public async Task<bool> TuChoiChamCongTheoDanhSachNV(DuyetChamCongDanhSachNhanVien input)
        {
            if (input == null)
            {
                return false;
            }

            // Duyệt chấm công theo khoảng startDate - endDate
            var chamCongs = await _chamCongRepository.GetListChamCongByTheoKhoang(input.StartDate, input.EndDate);
            // lấy ra các bản ghi chấm công để cập nhật dựa trên nhanVienIds
            foreach (var item in input.ChamCongs)
            {
                var chamCong = await _chamCongRepository.GetChamCongById(item.ChamCongId);
                if (chamCong != null)
                {
                    if (chamCong.TrangThaiChamCong == TrangThaiChamCongConsts.ChoDuyet)
                    {
                        chamCong.TrangThaiChamCong = TrangThaiChamCongConsts.TuChoi;
                        chamCong.NgayDuyetChamCong = DateTime.Now;
                        await _chamCongRepository.UpdateAsync(chamCong);
                    }

                    // Kiểm tra xem subtask ứng với chamCong này đã hoàn thành chưa
                    var congViecs = await _congViecRepository.GetCongViecsByChamCongId(chamCong.Id);
                    if (congViecs != null)
                    {
                        foreach (var congViec in congViecs)
                        {
                            var subTask = await _subTaskRepository.GetSubTaskById(congViec.SubtaskId);
                            if (subTask != null)
                            {
                                if (subTask.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh)
                                {
                                    subTask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.ChuaHoanThanh;
                                    await _subTaskRepository.UpdateAsync(subTask);
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        private static string RegexFormat(string input)
        {
            if (input != null)
            {
                return Regex.Replace(input, @"\s+", " ").Trim();
            }
            else
                return input;
        }
    }
}
