using Microsoft.EntityFrameworkCore;
using quanLyDuAnNoiBo.Entities;
using System.Reflection.Emit;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace quanLyDuAnNoiBo.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class quanLyDuAnNoiBoDbContext :
    AbpDbContext<quanLyDuAnNoiBoDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
    public DbSet<AppUser> UserCustom { get; set; }
    public DbSet<PhongBan> PhongBan { get; set; }
    public DbSet<ChucDanh> ChucDanh { get; set; }
    public DbSet<TrinhDoHocVan> TrinhkDoHocVan { get; set; }
    public DbSet<Hs_HoSoNhanVien> hs_HoSoNhanVien { get; set; }
    public DbSet<DanToc> DanToc { get; set; }
    public DbSet<QuanLyDuAn> QuanLyDuAn { get; set; }
    public DbSet<TaiLieuDinhKemDuAn> TaiLieuDinhKemDuAn { get; set; }
    public DbSet<ModuleDuAn> ModuleDuAn { get; set; }
    public DbSet<SprintDuAn> SprintDuAn { get; set; }
    public DbSet<ChiTietDuAn> ChiTietDuAn { get; set; }
    public DbSet<SubTask> SubTask { get; set; }
    public DbSet<SubTask_NhanVien> SubTask_NhanVien { get; set; }
    public DbSet<Entities.CanhBao> CanhBao { get; set; }
    public DbSet<Entities.ChamCong> ChamCong { get; set; }
    public DbSet<Entities.CongViec> CongViec { get; set; }
    public DbSet<TaiLieuDinhKemCongViec> TaiLieuDinhKemCongViec { get; set; }
    public DbSet<Entities.KieuViec> KieuViec { get; set; }

    #endregion

    public quanLyDuAnNoiBoDbContext(DbContextOptions<quanLyDuAnNoiBoDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /* Include modules to your migration db context */

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureBackgroundJobs();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureOpenIddict();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(quanLyDuAnNoiBoConsts.DbTablePrefix + "YourEntities", quanLyDuAnNoiBoConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        modelBuilder.Entity<AppUser>(b =>
        {
            b.ToTable("AbpUsers");

            b.Property(q => q.FirstName).HasMaxLength(64).IsRequired();
            b.Property(q => q.FullName).HasMaxLength(256).IsRequired();
            b.Property(q => q.ChucDanhId).IsRequired();
        });

        modelBuilder.Entity<PhongBan>(b =>
        {
            b.ToTable("PhongBan");
            b.ConfigureByConvention(); //auto configure for the base class props
        });

        modelBuilder.Entity<ChucDanh>(b =>
        {
            b.ToTable("ChucDanh");
            b.ConfigureByConvention(); //auto configure for the base class props
        });

        modelBuilder.Entity<Hs_HoSoNhanVien>(b =>
        {
            b.ToTable("Hs_HoSoNhanVien");
            b.ConfigureByConvention(); //auto configure for the base class props
        });

        modelBuilder.Entity<DanToc>(b =>
        {
            b.ToTable("DanToc");
            b.ConfigureByConvention(); //auto configure for the base class props
        });

        modelBuilder.Entity<QuanLyDuAn>(b =>
        {
            b.ToTable("DuAn");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<TaiLieuDinhKemDuAn>(b =>
        {
            b.ToTable("TaiLieuDinhKemDuAn");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<ModuleDuAn>(b =>
        {
            b.ToTable("TaskDuAn");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<SprintDuAn>(b =>
        {
            b.ToTable("SprintDuAn");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<ChiTietDuAn>(b =>
        {
            b.ToTable("ChiTietDuAn");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<SubTask>(b =>
        {
            b.ToTable("SubTask");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<SubTask_NhanVien>(b =>
        {
            b.ToTable("SubTask_NhanVien");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<Entities.CanhBao>(b =>
        {
            b.ToTable("CanhBao");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<Entities.ChamCong>(b =>
        {
            b.ToTable("ChamCong");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<Entities.CongViec>(b =>
        {
            b.ToTable("CongViec");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<TaiLieuDinhKemCongViec>(b =>
        {
            b.ToTable("TaiLieuDinhKemCongViec");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<Entities.KieuViec>(b =>
        {
            b.ToTable("KieuViec");
            b.ConfigureByConvention();
        });
    }
}
