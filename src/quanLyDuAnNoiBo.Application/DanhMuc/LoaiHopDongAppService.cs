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
    public class LoaiHopDongAppService : quanLyDuAnNoiBoAppService, ILoaiHopDongAppService
    {
        private readonly ILoaiHopDongRepository _loaiHopDongRepository;
        public LoaiHopDongAppService(
            ILoaiHopDongRepository loaiHopDongRepository
            )
        {
            _loaiHopDongRepository = loaiHopDongRepository;
        }
        public async Task<bool> CreateLoaiHopDong(LoaiHopDongDto input)
        {
            if (input == null)
            {
                return false;
            }

            var loaiHopDong = ObjectMapper.Map<LoaiHopDongDto, LoaiHopDong>(input);
            await _loaiHopDongRepository.InsertAsync(loaiHopDong);
            return true;
        }

        public async Task<bool> DeleteLoaiHopDong(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            var loaiHopDong = await _loaiHopDongRepository.GetAsync(id);
            if (loaiHopDong == null)
            {
                return false;
            }
            return true;
        }

        public async Task<List<LoaiHopDongDto>> GetAllLoaiHopDong()
        {
            var loaiHopDong = await _loaiHopDongRepository.GetAllListLoaiHopDong();
            var items = ObjectMapper.Map<List<LoaiHopDong>, List<LoaiHopDongDto>>(loaiHopDong);
            return items;
        }

        public async Task<PagedResultDto<LoaiHopDongDto>> GetAllAsync(GetAllInputLoaiHopDongDto input)
        {
            var loaiHopDong = await _loaiHopDongRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword);
            var totalCount = loaiHopDong.Count();
            var items = ObjectMapper.Map<List<LoaiHopDong>, List<LoaiHopDongDto>>(loaiHopDong);

            return new PagedResultDto<LoaiHopDongDto>()
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<LoaiHopDongDto?> GetLoaiHopDongById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var loaiHopDong = await _loaiHopDongRepository.GetAsync(id);
            if (loaiHopDong == null)
            {
                return null;
            }

            return ObjectMapper.Map<LoaiHopDong, LoaiHopDongDto>(loaiHopDong);
        }

        public async Task<bool> UpdateLoaiHopDong(LoaiHopDongDto input)
        {
            if (input == null)
            {
                return false;
            }

            var loaiHopDong = await _loaiHopDongRepository.GetAsync(input.Id);
            if (loaiHopDong == null)
            {
                return false;
            }

            loaiHopDong.TenLoaiHopDong = input.TenLoaiHopDong;
            loaiHopDong.MaLoaiHopDong = input.MaLoaiHopDong;
            loaiHopDong.ThoiHanLoaiHopDong = input.ThoiHanLoaiHopDong;
            await _loaiHopDongRepository.UpdateAsync(loaiHopDong);

            return true;
        }
    }
}
