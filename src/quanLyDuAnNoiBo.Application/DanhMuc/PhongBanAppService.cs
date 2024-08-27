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
    public class PhongBanAppService : quanLyDuAnNoiBoAppService, IPhongBanAppService
    {
        private readonly IPhongBanRepository _phongBanRepository;
        public PhongBanAppService(
            IPhongBanRepository phongBanRepository
            ) 
        {
            _phongBanRepository = phongBanRepository;
        }
        public async Task<bool> CreatePhongBan(PhongBanDto input)
        {
            if (input == null)
            {
                return false;
            }

            var checkExist = await _phongBanRepository.CheckExist(input.MaPhongBan);
            if (checkExist)
            {
                throw new PhongBanExistException();
            }

            var phongBan = ObjectMapper.Map<PhongBanDto, PhongBan>(input);
            await _phongBanRepository.InsertAsync(phongBan);
            return true;
        }

        public async Task<bool> DeletePhongBan(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            var phongBan = await _phongBanRepository.GetAsync(id);
            if (phongBan == null)
            {
                return false;
            }

            await _phongBanRepository.DeleteAsync(phongBan);
            return true;
        }

        public async Task<PagedResultDto<PhongBanDto>> GetAllPhongBan(GetAllInputPhongBanDto input)
        {
            var chuyenMon = await _phongBanRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount, input.Keyword);
            var totalCount = chuyenMon.Count();
            var items = ObjectMapper.Map<List<PhongBan>, List<PhongBanDto>>(chuyenMon);

            return new PagedResultDto<PhongBanDto>()
            {
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<PhongBanDto?> GetPhongBanById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }
            var phongBan = await _phongBanRepository.GetAsync(id);
            if (phongBan == null)
            {
                return null;
            }
            var result = ObjectMapper.Map<PhongBan, PhongBanDto>(phongBan);
            return result;
        }

        public async Task<bool> UpdatePhongBan(PhongBanDto input)
        {
            if (input == null)
            {
                return false;
            }

            var checkExist = await _phongBanRepository.CheckExist(input.MaPhongBan);
            if (checkExist)
            {
                throw new PhongBanExistException();
            }

            var phongBan = await _phongBanRepository.GetAsync(input.Id);
            if (phongBan == null)
            {
                return false;
            }

            phongBan.MaPhongBan = input.MaPhongBan;
            phongBan.TenPhongBan = input.TenPhongBan;
            phongBan.MoTa = input.MoTa;
            await _phongBanRepository.UpdateAsync(phongBan);
            return true;
        }
    }
}
