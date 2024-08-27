using quanLyDuAnNoiBo.Accounts;
using quanLyDuAnNoiBo.DanhMuc;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.Global.Dtos;
using quanLyDuAnNoiBo.ThongTinChung.QuanLyNhanVien.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.ThongTinChung.QuanLyNhanVien
{
    public class HoSoNhanVienAppService : quanLyDuAnNoiBoAppService, IHoSoNhanVienAppService
    {
        private readonly IHoSoNhanVienRepository _hoSoNhanVienRepository;
        private readonly IChucDanhRepository _chucDanhRepository;
        private readonly IPhongBanRepository _phongBanRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICaLamViecRepository _caLamViecRepository;
        private readonly IDanTocRepository _danTocRepository;
        public HoSoNhanVienAppService(
            IHoSoNhanVienRepository hoSoNhanVienRepository,
            IChucDanhRepository chucDanhRepository,
            IPhongBanRepository phongBanRepository,
            IAccountRepository accountRepository,
            ICaLamViecRepository caLamViecRepository,
            IDanTocRepository danTocRepository
            )
        {
            _hoSoNhanVienRepository = hoSoNhanVienRepository;
            _chucDanhRepository = chucDanhRepository;
            _phongBanRepository = phongBanRepository;
            this._accountRepository = accountRepository;
            this._caLamViecRepository = caLamViecRepository;
            this._danTocRepository = danTocRepository;
        }
        public async Task<bool> CreateHoSoNhanVienAsync(HoSoNhanVienDto input)
        {
            try
            {
                if (input == null)
                {
                    return false;
                }

                var checkExist = await _hoSoNhanVienRepository.CheckExistHoSoNhanVien(input.MaNhanVien, input.Id);
                if (checkExist)
                {
                    return false;
                }

                var hoSoNhanVien = ObjectMapper.Map<HoSoNhanVienDto, Hs_HoSoNhanVien>(input);
                await _hoSoNhanVienRepository.InsertAsync(hoSoNhanVien);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteHoSoNhanVienAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            var hoSoNhanVien = await _hoSoNhanVienRepository.GetAsync(id);
            if (hoSoNhanVien == null)
            {
                return false;
            }

            await _hoSoNhanVienRepository.DeleteAsync(hoSoNhanVien);
            return true;
        }

        public async Task<PagedResultDto<HoSoNhanVienDto>> GetAllAsync(GetAllInputHoSoNhanVienDto input)
        {
            var hoSoNhanVien = await _hoSoNhanVienRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword);
            var chucDanh = await _chucDanhRepository.GetAllListChucDanh();
            var phongBan = await _phongBanRepository.GetAllListPhongBan();
            var rs = from hs in hoSoNhanVien
                     join cd in chucDanh on hs.ChucDanhId equals cd.Id
                     join pb in phongBan on hs.PhongBanId equals pb.Id
                     select new HoSoNhanVienDto
                     {
                         Id = hs.Id,
                         MaNhanVien = hs.MaNhanVien,
                         HoTen = hs.HoTen,
                         ChucDanh = cd.TenChucDanh,
                         PhongBan = pb.TenPhongBan,
                         Email = hs.Email,
                     };
            var totalCount = rs.Count();
            return new PagedResultDto<HoSoNhanVienDto>()
            {
                Items = rs.ToList(),
                TotalCount = totalCount
            };
        }

        public async Task<List<HoSoNhanVienDto>> GetAllHoSoNhanVienAsync()
        {
            var hoSoNhanVien = await _hoSoNhanVienRepository.GetAllListHoSoNhanVien();
            var items = ObjectMapper.Map<List<Hs_HoSoNhanVien>, List<HoSoNhanVienDto>>(hoSoNhanVien);
            return items;
        }

        public async Task<HoSoNhanVienDto> GetHoSoNhanVienByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var hoSoNhanVien = await _hoSoNhanVienRepository.GetAsync(id);
            return ObjectMapper.Map<Hs_HoSoNhanVien, HoSoNhanVienDto>(hoSoNhanVien);
        }

        public async Task<bool> UpdateHoSoNhanVienAsync(HoSoNhanVienDto input)
        {
            var hoSoNhanVien = await _hoSoNhanVienRepository.GetAsync(input.Id);
            var checkExist = await _hoSoNhanVienRepository.CheckExistHoSoNhanVien(input.MaNhanVien, input.Id);
            if (hoSoNhanVien == null || checkExist || input == null)
            {
                return false;
            }

            hoSoNhanVien.MaNhanVien = input.MaNhanVien;
            hoSoNhanVien.HoTen = input.HoTen;
            hoSoNhanVien.NgaySinh = input.NgaySinh;
            hoSoNhanVien.GioiTinh = input.GioiTinh;
            hoSoNhanVien.SoDienThoai = input.SoDienThoai;
            hoSoNhanVien.Email = input.Email;
            hoSoNhanVien.MaSoThue = input.MaSoThue;
            hoSoNhanVien.NgayNghiViec = input.NgayNghiViec;
            hoSoNhanVien.NgayVaoLam = input.NgayVaoLam;
            hoSoNhanVien.CMND = input.CMND;
            hoSoNhanVien.NgayCapCMND = input.NgayCapCMND;
            hoSoNhanVien.NoiCapCMND = input.NoiCapCMND;
            hoSoNhanVien.NoiSinh = input.NoiSinh;
            hoSoNhanVien.ChucDanhId = input.ChucDanhId;
            hoSoNhanVien.PhongBanId = input.PhongBanId;
            hoSoNhanVien.CaLamViecId = input.CaLamViecId;
            hoSoNhanVien.HoChieu = input.HoChieu;
            hoSoNhanVien.UserId = input.UserId;

            await _hoSoNhanVienRepository.UpdateAsync(hoSoNhanVien);
            return true;
        }

        public async Task<List<LookupTableDto>> GetAllUserAsync()
        {
            var users = await _accountRepository.GetAllAccountsAsync(null, 0, int.MaxValue, null);
            var result = users.Select(e => new LookupTableDto()
            {
                Id = e.Id,
                DisplayName = e.FullName
            }).ToList();

            return result;
        }

        public async Task<List<LookupTableDto>> GetAllCaLamViec()
        {
            var caLamViec = await _caLamViecRepository.GetAllListCaLamViec();
            var result = caLamViec.Select(e => new LookupTableDto()
            {
                Id = e.Id,
                DisplayName = e.MaCaLamViec
            }).ToList();

            return result;
        }

        public async Task<List<LookupTableDto>> GetAllDanToc()
        {
            var danToc = await _danTocRepository.GetAll();
            var result = danToc.Select(e => new LookupTableDto()
            {
                Id = e.Id,
                DisplayName = e.TenDanToc
            }).ToList();

            return result;
        }

        public async Task<List<LookupTableDto>> GetAllPhongBan()
        {
            var phongBan = await _phongBanRepository.GetAllListPhongBan();
            var result = phongBan.Select(e => new LookupTableDto()
            {
                Id = e.Id,
                DisplayName = e.TenPhongBan
            }).ToList();

            return result;
        }

        public async Task<List<LookupTableDto>> GetAllChucDanh()
        {
            var chucDanh = await _chucDanhRepository.GetAllListChucDanh();
            var result = chucDanh.Select(e => new LookupTableDto()
            {
                Id = e.Id,
                DisplayName = e.TenChucDanh
            }).ToList();

            return result;
        }

    }
}
