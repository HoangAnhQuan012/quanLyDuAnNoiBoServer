using quanLyDuAnNoiBo.Accounts;
using quanLyDuAnNoiBo.BodQuanLyDuAn;
using quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos;
using quanLyDuAnNoiBo.ChamCong.Dtos;
using quanLyDuAnNoiBo.ChamCongRepo;
using quanLyDuAnNoiBo.CongViec;
using quanLyDuAnNoiBo.DanhMuc;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.Global;
using quanLyDuAnNoiBo.KieuViec;
using quanLyDuAnNoiBo.PmQuanLyDuAn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.ChamCong
{
    public class NhanVienChamCongAppService : quanLyDuAnNoiBoAppService
    {
        private readonly IQuanLyDuAnRepository _duAnRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly ISprintDuAnRepository _sprintDuAnRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IChucDanhAppService chucDanhAppService;
        private readonly IPmQuanLyDuAnRepository _pmQuanLyDuAnRepository;
        private readonly IChamCongRepository _chamCongRepository;
        private readonly ICongViecRepository _congViecRepository;
        private readonly ITaiLieuDinhKemCongViecRepository _taiLieuDinhKemCongViecRepository;
        private readonly IKieuViecRepository _kieuViecRepository;
        public NhanVienChamCongAppService(
            IQuanLyDuAnRepository duAnRepository,
            ISubTaskRepository subTaskRepository,
            ISprintDuAnRepository sprintDuAnRepository,
            IAccountRepository accountRepository,
            IChucDanhAppService chucDanhAppService,
            IPmQuanLyDuAnRepository pmQuanLyDuAnRepository,
            IChamCongRepository chamCongRepository,
            ICongViecRepository congViecRepository,
            ITaiLieuDinhKemCongViecRepository taiLieuDinhKemCongViecRepository,
            IKieuViecRepository kieuViecRepository
            )
        {
            _duAnRepository = duAnRepository;
            _subTaskRepository = subTaskRepository;
            _sprintDuAnRepository = sprintDuAnRepository;
            _accountRepository = accountRepository;
            this.chucDanhAppService = chucDanhAppService;
            _pmQuanLyDuAnRepository = pmQuanLyDuAnRepository;
            _chamCongRepository = chamCongRepository;
            _congViecRepository = congViecRepository;
            _taiLieuDinhKemCongViecRepository = taiLieuDinhKemCongViecRepository;
            _kieuViecRepository = kieuViecRepository;
        }

        public async Task<PagedResultDto<GetAllDuAnChamCongDto>> GetAllDuAnChamCong(GetAllDuAnChamCongInput input)
        {
            var currentUserId = CurrentUser.Id;
            var account = await _accountRepository.GetAccountAsync(currentUserId);
            if (account != null)
            {
                var chucDanh = await chucDanhAppService.GetChucDanhById(account.ChucDanhId);
                if (chucDanh != null && chucDanh.MaChucDanh == "PM")
                {
                    var result = await _pmQuanLyDuAnRepository.GetAllDuAnByPm(currentUserId, input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword, input.KhachHang, input.TrangThai);
                    var totalCount = result.Count();

                    var items = ObjectMapper.Map<List<QuanLyDuAn>, List<GetAllDuAnChamCongDto>>(result);
                    foreach (var item in items)
                    {
                        var pm = await _accountRepository.GetAccountAsync(item.QuanLyDuAnId);
                        item.TenQuanLyDuAn = pm.UserName;
                    }

                    return new PagedResultDto<GetAllDuAnChamCongDto>()
                    {
                        TotalCount = totalCount,
                        Items = items
                    };
                }
                else if (chucDanh != null && (chucDanh.MaChucDanh != "PM" || chucDanh.MaChucDanh != "BoD"))
                {
                    var duAns = await _duAnRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword, input.KhachHang, input.TrangThai, null);
                    var subtasks = await _subTaskRepository.GetListAsync();
                    var sprints = await _sprintDuAnRepository.GetListAsync();
                    // query để check xem nhanSu trong subtask có contain currentUserId không nếu tồn tại 1 subtask có nhanSuId = currentUserId thì lấy ra cả dự án đó
                    // Để lấy ra dự án thì cần lấy ra sprintId từ subtask và lấy ra dự án từ sprintId
                    var result = duAns.Where(duAn =>
                                    sprints.Any(sprint =>
                                        sprint.DuAnId == duAn.Id &&
                                        subtasks.Any(subtask =>
                                            subtask.SprintId == sprint.Id &&
                                            subtask.NhanSu != null ? subtask.NhanSu.Split(',').Contains(currentUserId.ToString()) : false
                                        )
                                    )
                                ).ToList();
                    var totalCount = result.Count();

                    var items = ObjectMapper.Map<List<QuanLyDuAn>, List<GetAllDuAnChamCongDto>>(result);
                    // query để lấy tên quản lý dự án
                    foreach (var item in items)
                    {
                        var pm = await _accountRepository.GetAccountAsync(item.QuanLyDuAnId);
                        item.TenQuanLyDuAn = pm.UserName;
                    }

                    return new PagedResultDto<GetAllDuAnChamCongDto>()
                    {
                        TotalCount = totalCount,
                        Items = items
                    };
                }
            }

            return new PagedResultDto<GetAllDuAnChamCongDto>();
        }

        public async Task<PagedResultDto<GetAllChamCongListOutput>> GetAllChamCongByDuAnId(GetAllChamCongInputDto input)
        {
            var currentUserId = CurrentUser.Id;
            var chamCongs = await _chamCongRepository.GetAllChamCongListByDuAnId(input.DuAnId, (Guid)currentUserId, input.SkipCount, input.MaxResultCount, input.Sorting);
            var sprints = await _sprintDuAnRepository.GetListAsync();
            var subtasks = await _subTaskRepository.GetListAsync();
            // query để check xem nhanSu trong subtask có contain currentUserId không nếu tồn tại 1 subtask có nhanSuId = currentUserId thì lấy ra list chamCong của nhan viên đó
            // Để lấy ra cham cong nhan vien thì cần lấy ra sprintId từ subtask và lấy ra cham cong từ sprintId
            var result = chamCongs.Where(chamCong =>
                            sprints.Any(sprint =>
                                sprint.DuAnId == input.DuAnId &&
                                    subtasks.Any(subtask =>
                                        subtask.SprintId == sprint.Id &&
                                        subtask.NhanSu != null ? subtask.NhanSu.Split(',').Contains(currentUserId.ToString()) : false
                                        )
                                    )
                            ).ToList();
            var totalCount = result.Count();

            //var items = ObjectMapper.Map<List<Entities.ChamCong>, List<GetAllChamCongListOutput>>(result);
            var items = new List<GetAllChamCongListOutput>();
            foreach (var item in result)
            {
                var chamCong = new GetAllChamCongListOutput
                {
                    ChamCongId = item.Id,
                    NgayChamCong = item.NgayChamCong,
                    TenSprint = await _sprintDuAnRepository.GetSprintNameById(item.SprintId),
                    TenTask = item.TenTask,
                    ThoiGian = item.ThoiGianChamCong,
                    TrangThaiChamCong = item.TrangThaiChamCong,
                };

                var listCongViec = await _congViecRepository.GetCongViecsByChamCongId(item.Id);
                var congViecs = new List<ChamCongDetail>();
                foreach (var congViec in listCongViec)
                {
                    // ThoiGian của congViecs bằng tổng thời gian của các công việc
                    var chamCongDetail = new ChamCongDetail
                    {
                        TenSubtask = await _subTaskRepository.GetSubtasknameById(congViec.SubtaskId),
                        ThoiGian = congViec.SoGio,
                        KieuViec = await _kieuViecRepository.GetKieuViecNameById(congViec.KieuViecId),
                        LoaiHinh = GlobalModel.SortedLoaiHinhDuAn[(int)congViec.LoaiHinh],
                        NgayDuyetChamCong = item.NgayDuyetChamCong,
                    };

                    var listFiles = await _taiLieuDinhKemCongViecRepository.GetTaiLieuDinhKemCongViecByCongViecId(congViec.Id);
                    var files = new List<ChamCongDinhKemFiles>();
                    foreach (var file in listFiles)
                    {
                        var chamCongDinhKemFile = new ChamCongDinhKemFiles
                        {
                            TenDinhKem = file.TenDinhKem,
                            FileDinhKem = file.FileDinhKem
                        };
                        files.Add(chamCongDinhKemFile);
                    }
                    chamCongDetail.ChamCongDinhKemFiles = files;
                    congViecs.Add(chamCongDetail);
                }
                chamCong.ChamCongDetail = congViecs;
                items.Add(chamCong);
            }

            return new PagedResultDto<GetAllChamCongListOutput>()
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<bool> CreateChamCongAsync(CreateOrUpdateChamCongInputDto input)
        {
            try
            {
                if (input != null)
                {
                    // Kiểm tra xem thời gian chấm công đã vượt quá thời gian của subtask chưa
                    foreach (var item in input.CongViecs)
                    {
                        var subtask = await _subTaskRepository.GetSubTaskById(item.SubtaskId);
                        if (subtask != null)
                        {
                            // Lấy ra list tất cả các công việc của subtask và tính tổng thời gian của các công việc công đó với soGio của subtask ở input
                            var congViecs = await _congViecRepository.GetCongViecsBySubtaskId(item.SubtaskId);
                            var thoiGianCongViecs = congViecs.Sum(c => c.SoGio);
                            if (thoiGianCongViecs + item.SoGio > subtask.TongThoiGian)
                            {
                                throw new ChamCongQuaThoiGianEstException();
                            }
                        }
                    }
                    var currentUserId = CurrentUser.Id;
                    var chamCongInfo = await _chamCongRepository.GetChamCongByNhanVienId((Guid)currentUserId, input.NgayChamCong);
                    if (chamCongInfo != null)
                    {
                        // Kiểm tra xem có công việc nào trong ngày hôm nay chưa
                        var congViecs = await _congViecRepository.GetCongViecsByChamCongId(chamCongInfo.Id);
                        bool isExist = congViecs.Any(c => input.CongViecs.Any(e => e.SubtaskId == c.SubtaskId));

                        // Nếu đã có công việc trong ngày hôm nay và có subtaskId trùng với input
                        if (isExist)
                        {
                            // Cập nhật thông tin của các công việc có subtaskId trùng
                            foreach (var item in input.CongViecs)
                            {
                                var congViec = await _congViecRepository.GetCongViecBySubtaskIdAndNgayBaoCao(item.SubtaskId, input.NgayChamCong);
                                if (congViec != null)
                                {
                                    congViec.LoaiHinh = item.LoaiHinh;
                                    congViec.SoGio = item.SoGio;
                                    congViec.OnSite = item.OnSite;
                                    congViec.KieuViecId = item.KieuViecId;
                                    congViec.GhiChu = item.GhiChu;
                                    congViec.TaiLieuDinhKemCongViec = new List<TaiLieuDinhKemCongViec>();

                                    // cap nhat trang thai Subtask nếu DanhDauHoanThanh = true
                                    if (item.DanhDauHoanThanh)
                                    {
                                        var subtask = await _subTaskRepository.GetSubTaskById(item.SubtaskId);
                                        if (subtask != null && subtask.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.ChuaHoanThanh)
                                        {
                                            subtask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh;
                                            await _subTaskRepository.UpdateAsync(subtask);
                                        }
                                    }

                                    var taiLieuDinhKems = await _taiLieuDinhKemCongViecRepository.GetTaiLieuDinhKemCongViecByCongViecId(congViec.Id);
                                    foreach (var taiLieuDinhKem in taiLieuDinhKems)
                                    {
                                        await _taiLieuDinhKemCongViecRepository.DeleteAsync(taiLieuDinhKem);
                                    }
                                    if (item.ChamCongDinhKemFiles != null && item.ChamCongDinhKemFiles.Count > 0)
                                    {
                                        foreach (var file in item.ChamCongDinhKemFiles)
                                        {
                                            var chamCongDinhKemFile = new TaiLieuDinhKemCongViec
                                            {
                                                CongViecId = congViec.Id,
                                                TenDinhKem = file.TenDinhKem,
                                                FileDinhKem = file.FileDinhKem
                                            };
                                            await _taiLieuDinhKemCongViecRepository.InsertAsync(chamCongDinhKemFile);
                                        }
                                    }
                                    await _congViecRepository.UpdateAsync(congViec);
                                }
                            }

                            // Tính lại ThoiGianChamCong của chamCongInfo
                            chamCongInfo.ThoiGianChamCong = (await _congViecRepository.GetCongViecsByChamCongId(chamCongInfo.Id))
                                                            .Sum(c => c.SoGio);
                            await _chamCongRepository.UpdateAsync(chamCongInfo);

                            return true;
                        }
                    }

                    var chamCong = ObjectMapper.Map<CreateOrUpdateChamCongInputDto, Entities.ChamCong>(input);
                    chamCong.NhanVienId = (Guid)currentUserId;
                    chamCong.TrangThaiChamCong = TrangThaiChamCongConsts.ChoDuyet;
                    chamCong.ThoiGianChamCong = input.CongViecs.Sum(e => e.SoGio);
                    chamCong.NgayChamCong = input.NgayChamCong.ToLocalTime();
                    await _chamCongRepository.InsertAsync(chamCong);

                    // Nếu input có 2 công việc trùng subtaskId thì chỉ lấy 1 công việc còn soGio là tổng của 2 công việc
                    var congViecsList = new List<CongViecDto>();
                    foreach (var item in input.CongViecs)
                    {
                        if (congViecsList.Any(c => c.SubtaskId == item.SubtaskId))
                        {
                            var congViecInfor = congViecsList.FirstOrDefault(c => c.SubtaskId == item.SubtaskId);
                            congViecInfor.SoGio += item.SoGio;
                        }
                        else
                        {
                            congViecsList.Add(item);
                        }
                    }

                    // Thêm các công việc vào CSDL
                    foreach (var item in congViecsList)
                    {
                        var congViec = new Entities.CongViec
                        {
                            ChamCongId = chamCong.Id,
                            SubtaskId = item.SubtaskId,
                            LoaiHinh = item.LoaiHinh,
                            OnSite = item.OnSite,
                            SoGio = item.SoGio,
                            KieuViecId = item.KieuViecId,
                            GhiChu = item.GhiChu,
                        };
                        await _congViecRepository.InsertAsync(congViec);

                        // cap nhat trang thai Subtask nếu DanhDauHoanThanh = true
                        if (item.DanhDauHoanThanh)
                        {
                            var subtask = await _subTaskRepository.GetSubTaskById(item.SubtaskId);
                            if (subtask != null && subtask.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.ChuaHoanThanh)
                            {
                                subtask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh;
                                await _subTaskRepository.UpdateAsync(subtask);
                            }
                        }

                        if (item.ChamCongDinhKemFiles[0] != null && item.ChamCongDinhKemFiles.Count > 0)
                        {
                            foreach (var file in item.ChamCongDinhKemFiles)
                            {
                                var chamCongDinhKemFile = new TaiLieuDinhKemCongViec
                                {
                                    CongViecId = congViec.Id,
                                    TenDinhKem = file.TenDinhKem,
                                    FileDinhKem = file.FileDinhKem
                                };
                                await _taiLieuDinhKemCongViecRepository.InsertAsync(chamCongDinhKemFile);
                            }
                        }

                        //if (item.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh)
                        //{
                        //    var subtask = await _subTaskRepository.GetSubTaskById(item.SubtaskId);
                        //    if (subtask != null && subtask.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.ChuaHoanThanh)
                        //    {
                        //        subtask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh;
                        //        await _subTaskRepository.UpdateAsync(subtask);
                        //    }
                        //}
                    }
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateChamCongAsync(CreateOrUpdateChamCongInputDto input)
        {
            if (input != null)
            {
                // Kiểm tra xem thời gian chấm công đã vượt quá thời gian của subtask chưa
                foreach (var item in input.CongViecs)
                {
                    var subtask = await _subTaskRepository.GetSubTaskById(item.SubtaskId);
                    if (subtask != null)
                    {
                        // Lấy ra list tất cả các công việc của subtask và tính tổng thời gian của các công việc công đó với soGio của subtask ở input
                        var congViecs = await _congViecRepository.GetCongViecsBySubtaskId(item.SubtaskId);
                        var thoiGianCongViecs = congViecs.Sum(c => c.SoGio);
                        if (thoiGianCongViecs + item.SoGio > subtask.TongThoiGian)
                        {
                            throw new ChamCongQuaThoiGianEstException();
                        }
                    }
                }
                var chamCong = await _chamCongRepository.GetChamCongById(input.ChamCongId);
                if (chamCong != null)
                {
                    chamCong.SprintId = input.SprintId;
                    chamCong.TenTask = input.TenTask;
                    chamCong.NgayChamCong = input.NgayChamCong.ToLocalTime();
                    chamCong.ThoiGianChamCong = input.CongViecs.Sum(e => e.SoGio);
                    chamCong.TrangThaiChamCong = TrangThaiChamCongConsts.ChoDuyet;
                    await _chamCongRepository.UpdateAsync(chamCong);

                    // Nếu input có 2 công việc trùng subtaskId thì chỉ lấy 1 công việc còn soGio là tổng của 2 công việc
                    var congViecsList = new List<CongViecDto>();
                    foreach (var item in input.CongViecs)
                    {
                        var existingCongViec = congViecsList.FirstOrDefault(c => c.SubtaskId == item.SubtaskId);
                        if (existingCongViec != null)
                        {
                            existingCongViec.SoGio += item.SoGio;
                        }
                        else
                        {
                            congViecsList.Add(item);
                        }
                    }

                    foreach (var item in congViecsList)
                    {
                        var congViec = await _congViecRepository.GetCongViecById(item.CongViecId);
                        if (congViec != null)
                        {
                            congViec.SubtaskId = item.SubtaskId;
                            congViec.LoaiHinh = item.LoaiHinh;
                            congViec.OnSite = item.OnSite;
                            congViec.SoGio = item.SoGio;
                            congViec.KieuViecId = item.KieuViecId;
                            congViec.GhiChu = item.GhiChu;
                            await _congViecRepository.UpdateAsync(congViec);

                            // cap nhat trang thai Subtask nếu DanhDauHoanThanh = true
                            if (item.DanhDauHoanThanh)
                            {
                                var subtask = await _subTaskRepository.GetSubTaskById(item.SubtaskId);
                                if (subtask != null && subtask.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.ChuaHoanThanh)
                                {
                                    subtask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh;
                                    await _subTaskRepository.UpdateAsync(subtask);
                                }
                            }

                            if (item.ChamCongDinhKemFiles != null && item.ChamCongDinhKemFiles.Count > 0)
                            {
                                var existingFiles = await _taiLieuDinhKemCongViecRepository.GetTaiLieuDinhKemCongViecByCongViecId(congViec.Id);
                                foreach (var existingFile in existingFiles)
                                {
                                    if (!item.ChamCongDinhKemFiles.Any(f => f.TenDinhKem == existingFile.TenDinhKem))
                                    {
                                        await _taiLieuDinhKemCongViecRepository.DeleteAsync(existingFile);
                                    }
                                }

                                if(item.ChamCongDinhKemFiles[0] != null && item.ChamCongDinhKemFiles.Count > 0)
                                {
                                    foreach (var file in item.ChamCongDinhKemFiles)
                                    {
                                        var chamCongDinhKemFile = new TaiLieuDinhKemCongViec
                                        {
                                            CongViecId = congViec.Id,
                                            TenDinhKem = file.TenDinhKem,
                                            FileDinhKem = file.FileDinhKem
                                        };
                                        await _taiLieuDinhKemCongViecRepository.InsertAsync(chamCongDinhKemFile);
                                    }
                                }
                            }
                        }

                        // Xóa công việc cũ đã thêm trước đó trong ngày hôm nay
                        var congViecs = await _congViecRepository.GetCongViecsByChamCongId(chamCong.Id);
                        foreach (var cv in congViecs)
                        {
                            var taiLieuDinhKems = await _taiLieuDinhKemCongViecRepository.GetTaiLieuDinhKemCongViecByCongViecId(cv.Id);
                            foreach (var taiLieuDinhKem in taiLieuDinhKems)
                            {
                                await _taiLieuDinhKemCongViecRepository.DeleteAsync(taiLieuDinhKem);
                            }
                            await _congViecRepository.DeleteAsync(cv);
                        }

                        var newCongViec = new Entities.CongViec
                        {
                            ChamCongId = chamCong.Id,
                            SubtaskId = item.SubtaskId,
                            LoaiHinh = item.LoaiHinh,
                            OnSite = item.OnSite,
                            SoGio = item.SoGio,
                            KieuViecId = item.KieuViecId,
                            GhiChu = item.GhiChu,
                        };
                        await _congViecRepository.InsertAsync(newCongViec);

                        // cap nhat trang thai Subtask nếu DanhDauHoanThanh = true
                        if (item.DanhDauHoanThanh)
                        {
                            var subtask = await _subTaskRepository.GetSubTaskById(item.SubtaskId);
                            if (subtask != null && subtask.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.ChuaHoanThanh)
                            {
                                subtask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh;
                                await _subTaskRepository.UpdateAsync(subtask);
                            }
                        }

                        if (item.ChamCongDinhKemFiles[0] != null && item.ChamCongDinhKemFiles.Count > 0)
                        {
                            foreach (var file in item.ChamCongDinhKemFiles)
                            {
                                var chamCongDinhKemFile = new TaiLieuDinhKemCongViec
                                {
                                    CongViecId = newCongViec.Id,
                                    TenDinhKem = file.TenDinhKem,
                                    FileDinhKem = file.FileDinhKem
                                };
                                await _taiLieuDinhKemCongViecRepository.InsertAsync(chamCongDinhKemFile);
                            }
                        }

                    }
                    return true;
                }
                return false;
            }

            return false;
        }

        public async Task UpdateTrangThaiChamCong(Guid chamCongId, TrangThaiChamCongConsts trangThaiChamCong)
        {
            if (chamCongId != Guid.Empty)
            {
                var chamCong = await _chamCongRepository.GetChamCongById(chamCongId);
                if (chamCong != null)
                {
                    chamCong.TrangThaiChamCong = trangThaiChamCong;
                    if (chamCong.TrangThaiChamCong == TrangThaiChamCongConsts.DaDuyet)
                    {
                        chamCong.NgayDuyetChamCong = DateTime.Now;
                    }
                    await _chamCongRepository.UpdateAsync(chamCong);

                    // Cập nhật trạng thái của subtask
                    // check subtask có nhanSu contains người tạo chấm công không
                    var subtasks = await _subTaskRepository.GetListAsync();
                    var sprints = await _sprintDuAnRepository.GetListAsync();
                    var subtask = subtasks.FirstOrDefault(subtask =>
                            sprints.Any(sprint =>
                                sprint.DuAnId == chamCong.DuAnId &&
                                sprint.Id == subtask.SprintId && subtask.NhanSu != null ? subtask.NhanSu.Split(',').Contains(chamCong.CreatorId.ToString()) : false
                                )
                            );
                    if (subtask != null && subtask.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.GuiYeuCauHoanThanh)
                    {
                        subtask.TrangThaiSubtask = DuAn.TrangThaiSubtaskConsts.DaHoanThanh;
                        await _subTaskRepository.UpdateAsync(subtask);
                    }
                }
            }
        }

        public async Task DeleteChamCong(Guid chamCongId)
        {
            if (chamCongId != Guid.Empty)
            {
                var chamCong = await _chamCongRepository.GetChamCongById(chamCongId);
                if (chamCong != null)
                {
                    // Xóa chấm công và những công việc, tài liệu đính kèm của chấm công đó
                    var congViecs = await _congViecRepository.GetCongViecsByChamCongId(chamCongId);
                    foreach (var congViec in congViecs)
                    {
                        var taiLieuDinhKems = await _taiLieuDinhKemCongViecRepository.GetTaiLieuDinhKemCongViecByCongViecId(congViec.Id);
                        foreach (var taiLieuDinhKem in taiLieuDinhKems)
                        {
                            await _taiLieuDinhKemCongViecRepository.DeleteAsync(taiLieuDinhKem);
                        }
                        await _congViecRepository.DeleteAsync(congViec);
                    }
                    await _chamCongRepository.DeleteAsync(chamCong);
                }
            }
        }

        public async Task GuiPheDuyet(Guid chamCongId)
        {
            if (chamCongId != Guid.Empty)
            {
                var chamCong = await _chamCongRepository.GetChamCongById(chamCongId);
                if (chamCong != null)
                {
                    chamCong.TrangThaiChamCong = TrangThaiChamCongConsts.ChoDuyet;
                    await _chamCongRepository.UpdateAsync(chamCong);
                }
            }
        }

        public async Task<CreateOrUpdateChamCongInputDto> GetChamCongAsync(Guid chamCongId)
        {
            if (chamCongId != Guid.Empty)
            {
                var chamCong = await _chamCongRepository.GetChamCongById(chamCongId);
                if (chamCong != null)
                {
                    var congViecs = await _congViecRepository.GetCongViecsByChamCongId(chamCongId);
                    var congViecsDto = new List<CongViecDto>();
                    foreach (var congViec in congViecs)
                    {
                        var congViecDto = new CongViecDto
                        {
                            CongViecId = congViec.Id,
                            SubtaskId = congViec.SubtaskId,
                            KieuViecId = congViec.KieuViecId,
                            LoaiHinh = congViec.LoaiHinh,
                            OnSite = congViec.OnSite,
                            SoGio = congViec.SoGio,
                            TrangThaiSubtask = await _subTaskRepository.GetTrangThaiById(congViec.SubtaskId),
                            GhiChu = congViec.GhiChu,
                            ChamCongDinhKemFiles = new List<ChamCongDinhKemFiles>()
                        };
                        var files = await _taiLieuDinhKemCongViecRepository.GetTaiLieuDinhKemCongViecByCongViecId(congViec.Id);
                        foreach (var file in files)
                        {
                            var chamCongDinhKemFile = new ChamCongDinhKemFiles
                            {
                                TenDinhKem = file.TenDinhKem,
                                FileDinhKem = file.FileDinhKem
                            };
                            congViecDto.ChamCongDinhKemFiles.Add(chamCongDinhKemFile);
                        }
                        congViecsDto.Add(congViecDto);
                    }
                    var chamCongDto = new CreateOrUpdateChamCongInputDto
                    {
                        ChamCongId = chamCong.Id,
                        SprintId = chamCong.SprintId,
                        TaskId = chamCong.TaskId,
                        TenTask = chamCong.TenTask,
                        DuAnId = chamCong.DuAnId,
                        NgayChamCong = chamCong.NgayChamCong.ToLocalTime(),
                        ThoiGianChamCong = chamCong.ThoiGianChamCong,
                        CongViecs = congViecsDto
                    };
                    return chamCongDto;
                }
            }

            return null;
        }
    }
}
