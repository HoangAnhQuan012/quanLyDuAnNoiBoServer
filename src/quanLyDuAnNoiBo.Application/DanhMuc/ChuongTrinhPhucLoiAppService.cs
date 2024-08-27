using quanLyDuAnNoiBo.DanhMuc.Dtos;
using quanLyDuAnNoiBo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public class ChuongTrinhPhucLoiAppService : quanLyDuAnNoiBoAppService, IChuongTrinhPhucLoiAppService
    {
        private readonly IChuongTrinhPhucLoiRepository _chuongTrinhPhucLoiRepository;
        public ChuongTrinhPhucLoiAppService(
            IChuongTrinhPhucLoiRepository chuongTrinhPhucLoiRepository
            )
        {
            _chuongTrinhPhucLoiRepository = chuongTrinhPhucLoiRepository;
        }
        public async Task<bool> CreateChuongTrinhPhucLoi(ChuongTrinhPhucLoiDto input)
        {
            var chuongTrinhPhucLoi = ObjectMapper.Map<ChuongTrinhPhucLoiDto, ChuongTrinhPhucLoi>(input);
            await _chuongTrinhPhucLoiRepository.InsertAsync(chuongTrinhPhucLoi);
            return true;
        }

        public async Task<bool> DeleteChuongTrinhPhucLoi(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            var chuongTrinhPhucLoi = await _chuongTrinhPhucLoiRepository.GetAsync(id);
            if (chuongTrinhPhucLoi == null)
            {
                return false;
            }

            await _chuongTrinhPhucLoiRepository.DeleteAsync(chuongTrinhPhucLoi);
            return true;
        }

        public async Task<PagedResultDto<ChuongTrinhPhucLoiDto>> GetAllAsync(GetAllInputChuongTrinhPhucLoiDto input)
        {
            var chuongTrinhPhucLoi = await _chuongTrinhPhucLoiRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword);
            var totalCount = chuongTrinhPhucLoi.Count();
            var items = ObjectMapper.Map<List<ChuongTrinhPhucLoi>, List<ChuongTrinhPhucLoiDto>>(chuongTrinhPhucLoi);

            return new PagedResultDto<ChuongTrinhPhucLoiDto>()
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<List<ChuongTrinhPhucLoiDto>> GetAllChuongTrinhPhucLoi()
        {
            var chuongTrinhPhucLoi = await _chuongTrinhPhucLoiRepository.GetAllListChuongTrinhPhucLoi();
            var items = ObjectMapper.Map<List<ChuongTrinhPhucLoi>, List<ChuongTrinhPhucLoiDto>>(chuongTrinhPhucLoi);
            return items;
        }

        public async Task<ChuongTrinhPhucLoiDto> GetChuongTrinhPhucLoiById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var chuongTrinhPhucLoi = await _chuongTrinhPhucLoiRepository.GetAsync(id);
            if (chuongTrinhPhucLoi == null)
            {
                return null;
            }

            return ObjectMapper.Map<ChuongTrinhPhucLoi, ChuongTrinhPhucLoiDto>(chuongTrinhPhucLoi);
        }

        public async Task<bool> UpdateChuongTrinhPhucLoi(ChuongTrinhPhucLoiDto input)
        {
            if (input == null)
            {
                return false;
            }

            var chuongTrinhPhucLoi = await _chuongTrinhPhucLoiRepository.GetAsync(input.Id);
            if (chuongTrinhPhucLoi == null)
            {
                return false;
            }

            chuongTrinhPhucLoi.TenChuongTrinh = input.TenChuongTrinhPhucLoi;
            chuongTrinhPhucLoi.MaChuongTrinh = input.MaChuongTrinhPhucLoi;
            chuongTrinhPhucLoi.ThoiGianBatDau = input.ThoiGianBatDau;
            chuongTrinhPhucLoi.ThoiGianKetThuc = input.ThoiGianKetThuc;
            chuongTrinhPhucLoi.MoTa = input.MoTa;
            chuongTrinhPhucLoi.TrangThai = input.TrangThai;
            chuongTrinhPhucLoi.DiaDiem = input.DiaDiem;
            await _chuongTrinhPhucLoiRepository.UpdateAsync(chuongTrinhPhucLoi);
            return true;
        }
    }
}
