using quanLyDuAnNoiBo.Accounts;
using quanLyDuAnNoiBo.BodQuanLyDuAn;
using quanLyDuAnNoiBo.DanhMuc;
using quanLyDuAnNoiBo.Global.Dtos;
using quanLyDuAnNoiBo.KieuViec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace quanLyDuAnNoiBo.Global
{
    public class LookUpTableAppService : quanLyDuAnNoiBoAppService
    {
        private readonly IQuanLyDuAnRepository _quanLyDuAnRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IChucDanhRepository _chucDanhRepository;
        private readonly ISprintDuAnRepository _sprintDuAnRepository;
        private readonly IModuleDuAnRepository _moduleDuAnRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly IKieuViecRepository _kieuViecRepository;
        private readonly IdentityUserManager _userManagement;
        public LookUpTableAppService(
            IQuanLyDuAnRepository quanLyDuAnRepository,
            IAccountRepository accountRepository,
            IChucDanhRepository chucDanhRepository,
            ISprintDuAnRepository sprintDuAnRepository,
            IModuleDuAnRepository moduleDuAnRepository,
            ISubTaskRepository subTaskRepository,
            IKieuViecRepository kieuViecRepository,
            IdentityUserManager userManagement
        )
        {
            _quanLyDuAnRepository = quanLyDuAnRepository;
            _accountRepository = accountRepository;
            _chucDanhRepository = chucDanhRepository;
            _sprintDuAnRepository = sprintDuAnRepository;
            _moduleDuAnRepository = moduleDuAnRepository;
            _subTaskRepository = subTaskRepository;
            _kieuViecRepository = kieuViecRepository;
            _userManagement = userManagement;
        }

        public async Task<List<LookupTableDto<string>>> GetAllKhachHang()
        {
            var khachHang = await _quanLyDuAnRepository.GetAllKhachHang();
            var result = khachHang.Select(s => new LookupTableDto<string>
            {
                Id = s,
                DisplayName = s
            }).ToList();

            return result;
        }

        public async Task<List<LookupTableIntDto>> GetAllTrangThaiDuAn()
        {
            List<LookupTableIntDto> result = new List<LookupTableIntDto>();
            foreach (var item in GlobalModel.SortedTrangThaiDuAn)
            {
                result.Add(new LookupTableIntDto { Id = item.Key, DisplayName = item.Value });
            }

            return await Task.FromResult(result);
        }

        public async Task<List<LookupTableDto>> GetAllQuanLyDuAn()
        {
            // Lấy ra tất cả các account có chức danh là PM
            var chucDanh = await _chucDanhRepository.GetChucDanhPM();
            if (chucDanh != null)
            {
                var accounts = await _accountRepository.GetAllAccountsForFilter();
                List<LookupTableDto> result = new List<LookupTableDto>();
                accounts.Where(w => w.ChucDanhId == chucDanh.Id).ToList().ForEach(f =>
                {
                    result.Add(new LookupTableDto { Id = f.Id, DisplayName = f.UserName });
                });

                return result;
            }

            return new List<LookupTableDto>();
        }

        public async Task<List<LookupTableDto>> GetAllNhanSu()
        {
            var accounts = await _accountRepository.GetAllAccountsForFilter();
            var chucDanhs = await _chucDanhRepository.GetListAsync();
            accounts = accounts.Where(w => w.ChucDanhId != chucDanhs.FirstOrDefault(f => f.MaChucDanh == "BoD").Id).ToList();
            List<LookupTableDto> result = new List<LookupTableDto>();
            accounts.ToList().ForEach(f =>
            {
                result.Add(new LookupTableDto { Id = f.Id, DisplayName = f.UserName });
            });

            return result;
        }

        public async Task<List<LookupTableIntDto>> GetAllLoaiHinhCongViec()
        {
            List<LookupTableIntDto> result = new List<LookupTableIntDto>();
            foreach (var item in GlobalModel.SortedLoaiHinhDuAn)
            {
                result.Add(new LookupTableIntDto { Id = item.Key, DisplayName = item.Value });
            }

            return await Task.FromResult(result);
        }

        public async Task<List<LookupTableDto>> GetAllSprintDuAnByCurrentUser(Guid DuAnId)
        {
            var currentUserId = CurrentUser.Id;
            var subtasks = await _subTaskRepository.GetListAsync();
            var sprints = await _sprintDuAnRepository.GetAllSprintListByDuAnId(DuAnId);
            // Lấy ra tất cả các Sprint mà người dùng hiện tại tham gia trong đó nhân sự được lấy từ subtask
            var result = sprints.Where(w =>
                subtasks.Any(e =>
                    e.SprintId == w.Id &&
                    w.TrangThaiSprint == DuAn.TrangThaiSprintConsts.DaPheDuyet &&
                    e.NhanSu != null ? e.NhanSu.Split(',').Contains(currentUserId.ToString()) : false
                    )).Select(s => new LookupTableDto
                    {
                        Id = s.Id,
                        DisplayName = s.TenSprint
                    }).ToList();

            return await Task.FromResult(result);
        }

        public async Task<List<LookupTableDto>> GetAllModuleDuAnByCurrentUser(Guid sprintId)
        {
            var currentUserId = CurrentUser.Id;
            var subtasks = await _subTaskRepository.GetSubtaksBySprintId(sprintId);
            var modules = await _moduleDuAnRepository.GetListAsync();
            // Lấy ra tất cả các Module mà người dùng hiện tại tham gia trong đó nhân sự được lấy từ subtask
            var result = modules.Where(w =>
                    subtasks.Any(e =>
                        e.ModuleId == w.Id &&
                        e.NhanSu != null ? e.NhanSu.Split(',').Contains(currentUserId.ToString()) : false
                                    )).Select(s => new LookupTableDto
                                    {
                                        Id = s.Id,
                                        DisplayName = s.TenModule
                                    }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<List<LookupTableDto>> GetAllSubtasksByCurrentUser(Guid sprintId, Guid taskId)
        {
            var currentUserId = CurrentUser.Id;
            var subtasks = await _subTaskRepository.GetSubtaskBySprintIdAndTaskId(sprintId, taskId);
            // Lấy ra tất cả các Subtask mà người dùng hiện tại tham gia
            var result = subtasks.Where(w =>
                            w.TrangThaiSubtask == DuAn.TrangThaiSubtaskConsts.DaDuocPhanCong &&
                            w.NhanSu != null ? w.NhanSu.Split(',').Contains(currentUserId.ToString()) : false
                                ).Select(s => new LookupTableDto
                                {
                                    Id = s.Id,
                                    DisplayName = s.TenSubTask
                                }).ToList();

            return await Task.FromResult(result);
        }

        public async Task<List<LookupTableDto>> GetAllKieuViec()
        {
            var currentUserId = CurrentUser.Id;
            var account = await _accountRepository.FindByIdAsync(currentUserId);
            var kieuViecs = await _kieuViecRepository.GetListAsync();
            // Lấy ra tất cả các KieuViec mà người dùng hiện tại tham gia
            if (account != null)
            {
                var result = kieuViecs.Where(e => account.ChucDanhId == e.ChucDanhId).Select(s => new LookupTableDto
                {
                    Id = s.Id,
                    DisplayName = s.TenKieuViec
                }).ToList();

                return result;
            }

            return new List<LookupTableDto>();
        }

        public async Task<string?> GetCurrentUsername()
        {
            var user = await this._userManagement.FindByIdAsync(CurrentUser.Id.Value.ToString());
            return user?.UserName;
        }
    }
}
