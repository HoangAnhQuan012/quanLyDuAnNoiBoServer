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
    public class ChucDanhAppService : quanLyDuAnNoiBoAppService, IChucDanhAppService
    {
        private readonly IChucDanhRepository _chucDanhRepository;
        public ChucDanhAppService(
            IChucDanhRepository chucDanhRepository
            )
        {
            _chucDanhRepository = chucDanhRepository;
        }
        public async Task<bool> CreateChucDanh(ChucDanhDto input)
        {
            if (input == null)
            {
                return false;
            }

            var checkExist = await _chucDanhRepository.CheckExist(input.MaChucDanh);
            if (checkExist)
            {
                throw new ChucDanhExistException();
            }

            var chucDanh = ObjectMapper.Map<ChucDanhDto, ChucDanh>(input);
            await _chucDanhRepository.InsertAsync(chucDanh);
            return true;
        }

        public async Task<bool> DeleteChucDanh(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            var chucDanh = await _chucDanhRepository.GetAsync(id);
            if (chucDanh == null)
            {
                return false;
            }

            await _chucDanhRepository.DeleteAsync(chucDanh);
            return true;
        }

        public async Task<List<ChucDanhDto>> GetAllChucDanh()
        {
            var chucDanh = await _chucDanhRepository.GetAllListChucDanh();
            var items = ObjectMapper.Map<List<ChucDanh>, List<ChucDanhDto>>(chucDanh);
            return items;
        }

        public async Task<ChucDanhDto?> GetChucDanhById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var chucDanh = await _chucDanhRepository.GetAsync(id);
            if (chucDanh == null)
            {
                return null;
            }

            var result = ObjectMapper.Map<ChucDanh, ChucDanhDto>(chucDanh);
            return result;
        }

        public async Task<PagedResultDto<ChucDanhDto>> GetAllAsync(GetAllInputChucDanhDto input)
        {
            var chucDanh = await _chucDanhRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword);
            var totalCount = chucDanh.Count();
            var items = ObjectMapper.Map<List<ChucDanh>, List<ChucDanhDto>>(chucDanh);

            return new PagedResultDto<ChucDanhDto>()
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<bool> UpdateChucDanh(ChucDanhDto input)
        {
            if (input == null)
            {
                return false;
            }

            var checkExist = await _chucDanhRepository.CheckExist(input.MaChucDanh);
            if (checkExist)
            {
                throw new ChucDanhExistException();
            }

            var chucDanh = await _chucDanhRepository.GetAsync(input.Id);
            if (chucDanh == null)
            {
                return false;
            }

            chucDanh.TenChucDanh = input.TenChucDanh;
            chucDanh.MaChucDanh = input.MaChucDanh;
            await _chucDanhRepository.UpdateAsync(chucDanh);

            return true;
        }

        public async Task<List<LookupTableDto>> GetAllChucDanhLookupTable()
        {
            var chucDanh = await _chucDanhRepository.GetAllListChucDanh();
            var result = chucDanh.Select(x => new LookupTableDto
            {
                Id = x.Id,
                DisplayName = x.TenChucDanh
            }).ToList();

            return result;
        }
    }
}
