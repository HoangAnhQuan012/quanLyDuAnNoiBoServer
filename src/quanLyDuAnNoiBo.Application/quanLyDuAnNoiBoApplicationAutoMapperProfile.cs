using AutoMapper;
using quanLyDuAnNoiBo.Accounts.Dtos;
using quanLyDuAnNoiBo.BoDQuanLyDuAn.Dtos;
using quanLyDuAnNoiBo.ChamCong.Dtos;
using quanLyDuAnNoiBo.DanhMuc.Dtos;
using quanLyDuAnNoiBo.Entities;
using quanLyDuAnNoiBo.PmQuanLyDuAn.Dtos;
using quanLyDuAnNoiBo.ThongTinChung.QuanLyNhanVien.Dtos;
using System.Collections.Generic;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity;

namespace quanLyDuAnNoiBo;

public class quanLyDuAnNoiBoApplicationAutoMapperProfile : Profile
{
    public quanLyDuAnNoiBoApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<CreateOrEditAccountDto, AppUser>();
        CreateMap<ChuyenMonDto, ChuyenMon>();
        CreateMap<ChuyenMon?, ChuyenMonDto>();
        CreateMap<AppUser, AccountDto>();
        CreateMap<PhongBanDto, PhongBan>();
        CreateMap<PhongBan, PhongBanDto>();
        CreateMap<ChucDanhDto, ChucDanh>();
        CreateMap<ChucDanh, ChucDanhDto>();
        CreateMap<LoaiHopDongDto, LoaiHopDong>();
        CreateMap<LoaiHopDong, LoaiHopDongDto>();
        CreateMap<CaLamViecDto, CalamViec>();
        CreateMap<CalamViec, CaLamViecDto>();
        CreateMap<TrinhDoHocVanDto, TrinhDoHocVan>();
        CreateMap<TrinhDoHocVan, TrinhDoHocVanDto>();
        CreateMap<ChuongTrinhPhucLoiDto, ChuongTrinhPhucLoi>();
        CreateMap<ChuongTrinhPhucLoi, ChuongTrinhPhucLoiDto>();
        CreateMap<HoSoNhanVienDto, Hs_HoSoNhanVien>();
        CreateMap<Hs_HoSoNhanVien, HoSoNhanVienDto>();
        CreateMap<CreateDuAnDto, QuanLyDuAn>();
        CreateMap<QuanLyDuAn, GetAllDuAnDto>();
        CreateMap<QuanLyDuAn, GetDuAnDto>();
        CreateMap<ModuleDuAn, ModuleDto>();
        CreateMap<QuanLyDuAn, GetAllDuAnByPmDto>();
        CreateMap<QuanLyDuAn, GetDuAnByPmIdDto>();
        CreateMap<CreateOrUpdateSprintDto, SprintDuAn>();
        CreateMap<ModuleDto, ModuleDuAn>();
        CreateMap<SubTaskDto, SubTask>();
        CreateMap<SprintDuAn, SprintDto>();
        CreateMap<CreateSubTaskDto, SubTask>();
        CreateMap<ModuleDuAn, ModuleSubTaskDto>();
        CreateMap<SubTask, SubTaskDto>();
        CreateMap<SubTask, SubTaskInner>();
        CreateMap<QuanLyDuAn, GetAllDuAnChamCongDto>();
        CreateMap<CreateOrUpdateChamCongInputDto, Entities.ChamCong>();
        CreateMap<Entities.ChamCong, GetAllChamCongListOutput>();
    }
}
