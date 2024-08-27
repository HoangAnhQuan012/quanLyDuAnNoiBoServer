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
    public class CaLamViecAppService : quanLyDuAnNoiBoAppService, ICaLamViecAppService
    {
        private readonly ICaLamViecRepository _caLamViecRepository;
        public CaLamViecAppService(
            ICaLamViecRepository caLamViecRepository
            )
        {
            _caLamViecRepository = caLamViecRepository;
        }
        public async Task<bool> CreateCaLamViec(CaLamViecDto input)
        {
            if(input == null)
            {
                return false;
            }

            var caLamViec = ObjectMapper.Map<CaLamViecDto, CalamViec>(input);
            await _caLamViecRepository.InsertAsync(caLamViec);
            return true;
        }

        public async Task<bool> DeleteCaLamViec(Guid id)
        {
            if(id == Guid.Empty)
            {
                return false;
            }

            var caLamViec = await _caLamViecRepository.GetAsync(id);
            if(caLamViec == null)
            {
                return false;
            }

            await _caLamViecRepository.DeleteAsync(caLamViec);
            return true;
        }

        public async Task<PagedResultDto<CaLamViecDto>> GetAllAsync(GetAllInputCaLamViecDto input)
        {
            var caLamViec = await _caLamViecRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword);
            var totalCount = caLamViec.Count();
            var items = ObjectMapper.Map<List<CalamViec>, List<CaLamViecDto>>(caLamViec);

            return new PagedResultDto<CaLamViecDto>()
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<List<CaLamViecDto>> GetAllCaLamViec()
        {
            var caLamViec = await _caLamViecRepository.GetAllListCaLamViec();
            return ObjectMapper.Map<List<CalamViec>, List<CaLamViecDto>>(caLamViec);
        }

        public async Task<CaLamViecDto> GetCaLamViecById(Guid id)
        {
            if(id == Guid.Empty)
            {
                return null;
            }

            var caLamViec = await _caLamViecRepository.GetAsync(id);
            return ObjectMapper.Map<CalamViec, CaLamViecDto>(caLamViec);
        }

        public async Task<bool> UpdateCaLamViec(CaLamViecDto input)
        {
            if(input == null)
            {
                return false;
            }

            var caLamViec = await _caLamViecRepository.GetAsync(input.Id);
            if(caLamViec == null)
            {
                return false;
            }

            caLamViec.MaCaLamViec = input.MaCaLamViec;
            caLamViec.GioVaoLam = input.GioVaoLam;
            caLamViec.GioTanLam = input.GioTanLam;
            caLamViec.Mota = input.Mota;
            await _caLamViecRepository.UpdateAsync(caLamViec);
            return true;
        }
    }
}
