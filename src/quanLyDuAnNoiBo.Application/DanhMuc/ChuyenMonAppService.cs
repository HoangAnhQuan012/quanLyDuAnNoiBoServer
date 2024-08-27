using quanLyDuAnNoiBo.DanhMuc.Dtos;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.Global.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace quanLyDuAnNoiBo.DanhMuc
{
    public class ChuyenMonAppService : quanLyDuAnNoiBoAppService, IChuyenMonAppService
    {
        private readonly IChuyenMonRepository _chuyenMonRepository;
        public ChuyenMonAppService(
            IChuyenMonRepository chuyenMonRepository
            )
        {
            _chuyenMonRepository = chuyenMonRepository;
        }

        public async Task<bool> CreateChuyenMon(ChuyenMonDto input)
        {
            if (input == null)
            {
                return false;
            }

            var chuyenMon = ObjectMapper.Map<ChuyenMonDto, ChuyenMon>(input);
            await _chuyenMonRepository.InsertAsync(chuyenMon);

            return true;
        }

        public async Task<bool> DeleteChuyenMon(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            var chuyenMon = await _chuyenMonRepository.GetAsync(id);
            if (chuyenMon == null)
            {
                return false;
            }

            await _chuyenMonRepository.DeleteAsync(chuyenMon);

            return true;
        }

        public async Task<PagedResultDto<ChuyenMonDto>> GetAllChuyenMon(GetAllInputChuyenMonDto input)
        {
            var chuyenMon = await _chuyenMonRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword);
            var totalCount = await _chuyenMonRepository.GetCountAsync(input.Keyword);
            var items = ObjectMapper.Map<List<ChuyenMon>, List<ChuyenMonDto>>(chuyenMon);

            return new PagedResultDto<ChuyenMonDto>()
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<ChuyenMonDto?> GetChuyenMonById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            var chuyenMon = await _chuyenMonRepository.GetAsync(id);
            return ObjectMapper.Map<ChuyenMon?, ChuyenMonDto>(chuyenMon);
        }

        public async Task<bool> UpdateChuyenMon(ChuyenMonDto input)
        {
            if (input == null || input.Id == null)
            {
                return false;
            }

            var chuyenMon = await _chuyenMonRepository.GetAsync(input.Id);
            if (chuyenMon == null)
            {
                return false;
            }

            chuyenMon.TenChuyenMon = input.TenChuyenMon;
            chuyenMon.MoTa = input.MoTa;
            await _chuyenMonRepository.UpdateAsync(chuyenMon);

            return true;
        }

        public async Task<List<LookupTableDto>> GetAllChuyenMonLookupTable()
        {
            var chuyenMon = await _chuyenMonRepository.GetListAsync(null, 0, int.MaxValue, null);
            var result = chuyenMon.Select(e => new LookupTableDto()
            {
                Id = e.Id,
                DisplayName = e.TenChuyenMon
            }).ToList();

            return result;
        }
    }
}
