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
    public class TrinhDoHocVanAppService : quanLyDuAnNoiBoAppService, ITrinhDoHocVanAppService
    {
        private readonly ITrinhDoHocVanRepository _trinhDoHocVanRepository;
        public TrinhDoHocVanAppService(
            ITrinhDoHocVanRepository trinhDoHocVanRepository
            )
        {
            _trinhDoHocVanRepository = trinhDoHocVanRepository;
        }
        public async Task<bool> CreateTrinhDoHocVan(TrinhDoHocVanDto input)
        {
            var trinhDoHocVan = ObjectMapper.Map<TrinhDoHocVanDto, TrinhDoHocVan>(input);
            await _trinhDoHocVanRepository.InsertAsync(trinhDoHocVan);
            return true;
        }

        public async Task<bool> DeleteTrinhDoHocVan(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            var trinhDoHocVan = await _trinhDoHocVanRepository.GetAsync(id);
            if (trinhDoHocVan == null)
            {
                return false;
            }

            await _trinhDoHocVanRepository.DeleteAsync(trinhDoHocVan);
            return true;
        }

        public async Task<PagedResultDto<TrinhDoHocVanDto>> GetAllAsync(GetAllInputTrinhDoHocVanDto input)
        {
            var trinhDoHocVan = await _trinhDoHocVanRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword);
            var totalCount = trinhDoHocVan.Count();
            var items = ObjectMapper.Map<List<TrinhDoHocVan>, List<TrinhDoHocVanDto>>(trinhDoHocVan);

            return new PagedResultDto<TrinhDoHocVanDto>()
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<List<TrinhDoHocVanDto>> GetAllTrinhDoHocVan()
        {
            var trinhDoHocVan = await _trinhDoHocVanRepository.GetAllListTrinhDoHocVan();
            return ObjectMapper.Map<List<TrinhDoHocVan>, List<TrinhDoHocVanDto>>(trinhDoHocVan);
        }

        public async Task<TrinhDoHocVanDto> GetTrinhDoHocVanById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var trinhDoHocVan = await _trinhDoHocVanRepository.GetAsync(id);
            return ObjectMapper.Map<TrinhDoHocVan, TrinhDoHocVanDto>(trinhDoHocVan);
        }

        public async Task<bool> UpdateTrinhDoHocVan(TrinhDoHocVanDto input)
        {
            if (input == null)
            {
                return false;
            }

            var trinhDoHocVan = await _trinhDoHocVanRepository.GetAsync(input.Id);
            if (trinhDoHocVan == null)
            {
                return false;
            }

            trinhDoHocVan.MaTrinhDoHocVan = input.MaTrinhDoHocVan;
            trinhDoHocVan.TenTrinhDoHocVan = input.TenTrinhDoHocVan;
            trinhDoHocVan.MoTa = input.MoTa;
            await _trinhDoHocVanRepository.UpdateAsync(trinhDoHocVan);
            return true;
        }
    }
}
