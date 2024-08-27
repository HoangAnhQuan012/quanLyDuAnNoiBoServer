using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quanLyDuAnNoiBo.Migrations
{
    /// <inheritdoc />
    public partial class AddBodDuAn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleDuAn_QuanLyDuAn_QuanLyDuAnId",
                table: "ModuleDuAn");

            migrationBuilder.DropForeignKey(
                name: "FK_TaiLieuDinhKemDuAn_QuanLyDuAn_QuanLyDuAnId",
                table: "TaiLieuDinhKemDuAn");

            migrationBuilder.DropIndex(
                name: "IX_ModuleDuAn_QuanLyDuAnId",
                table: "ModuleDuAn");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuanLyDuAn",
                table: "QuanLyDuAn");

            migrationBuilder.DropColumn(
                name: "QuanLyDuAnId",
                table: "ModuleDuAn");

            migrationBuilder.DropColumn(
                name: "LuongCoSo",
                table: "QuanLyDuAn");

            migrationBuilder.DropColumn(
                name: "QuanLyDuAnId",
                table: "QuanLyDuAn");

            migrationBuilder.RenameTable(
                name: "QuanLyDuAn",
                newName: "DuAn");

            migrationBuilder.RenameColumn(
                name: "QuanLyDuAnId",
                table: "TaiLieuDinhKemDuAn",
                newName: "ChiTietDuAnId");

            migrationBuilder.RenameIndex(
                name: "IX_TaiLieuDinhKemDuAn_QuanLyDuAnId",
                table: "TaiLieuDinhKemDuAn",
                newName: "IX_TaiLieuDinhKemDuAn_ChiTietDuAnId");

            migrationBuilder.RenameColumn(
                name: "Module",
                table: "ModuleDuAn",
                newName: "TenModule");

            migrationBuilder.AlterColumn<double>(
                name: "Test",
                table: "ModuleDuAn",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PM",
                table: "ModuleDuAn",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DuAnId",
                table: "ModuleDuAn",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Dev",
                table: "ModuleDuAn",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "BA",
                table: "ModuleDuAn",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TongThoiGian",
                table: "ModuleDuAn",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DuAn",
                table: "DuAn",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChiTietDuAn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DuAnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LuongCoSo = table.Column<double>(type: "float", nullable: false),
                    BanGiaoDungHan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HieuQuaSuDungNhanSu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoLucKhacPhucLoi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDoHaiLongCuaKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDoloiBiPhatHien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MucDoLoiUAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TyLeThucHienDungQuyTrinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NangSuatTaoTestcase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NangSuatThuThiTestcase = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NangSuatDev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NangSuatVietUT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NangSuatThucThiUT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NangSuatBA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GiaTriHopDong = table.Column<double>(type: "float", nullable: false),
                    ChiPhiABH = table.Column<double>(type: "float", nullable: false),
                    ChiPhiOpexPhanBo = table.Column<double>(type: "float", nullable: false),
                    ChiPhiLuongDuKien = table.Column<double>(type: "float", nullable: false),
                    ChiPhiLuongThucTe = table.Column<double>(type: "float", nullable: false),
                    LaiDuKien = table.Column<double>(type: "float", nullable: false),
                    LaiThucTe = table.Column<double>(type: "float", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDuAn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SprintDuAn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSprint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuAnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintDuAn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSubTask = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PM = table.Column<double>(type: "float", nullable: false),
                    Dev = table.Column<double>(type: "float", nullable: false),
                    Test = table.Column<double>(type: "float", nullable: false),
                    BA = table.Column<double>(type: "float", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SprintId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTask", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubTask_NhanVien",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NhanVienId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTask_NhanVien", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TaiLieuDinhKemDuAn_ChiTietDuAn_ChiTietDuAnId",
                table: "TaiLieuDinhKemDuAn",
                column: "ChiTietDuAnId",
                principalTable: "ChiTietDuAn",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaiLieuDinhKemDuAn_ChiTietDuAn_ChiTietDuAnId",
                table: "TaiLieuDinhKemDuAn");

            migrationBuilder.DropTable(
                name: "ChiTietDuAn");

            migrationBuilder.DropTable(
                name: "SprintDuAn");

            migrationBuilder.DropTable(
                name: "SubTask");

            migrationBuilder.DropTable(
                name: "SubTask_NhanVien");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DuAn",
                table: "DuAn");

            migrationBuilder.DropColumn(
                name: "TongThoiGian",
                table: "ModuleDuAn");

            migrationBuilder.RenameTable(
                name: "DuAn",
                newName: "QuanLyDuAn");

            migrationBuilder.RenameColumn(
                name: "ChiTietDuAnId",
                table: "TaiLieuDinhKemDuAn",
                newName: "QuanLyDuAnId");

            migrationBuilder.RenameIndex(
                name: "IX_TaiLieuDinhKemDuAn_ChiTietDuAnId",
                table: "TaiLieuDinhKemDuAn",
                newName: "IX_TaiLieuDinhKemDuAn_QuanLyDuAnId");

            migrationBuilder.RenameColumn(
                name: "TenModule",
                table: "ModuleDuAn",
                newName: "Module");

            migrationBuilder.AlterColumn<double>(
                name: "Test",
                table: "ModuleDuAn",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "PM",
                table: "ModuleDuAn",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<Guid>(
                name: "DuAnId",
                table: "ModuleDuAn",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<double>(
                name: "Dev",
                table: "ModuleDuAn",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "BA",
                table: "ModuleDuAn",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<Guid>(
                name: "QuanLyDuAnId",
                table: "ModuleDuAn",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "LuongCoSo",
                table: "QuanLyDuAn",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "QuanLyDuAnId",
                table: "QuanLyDuAn",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuanLyDuAn",
                table: "QuanLyDuAn",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleDuAn_QuanLyDuAnId",
                table: "ModuleDuAn",
                column: "QuanLyDuAnId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleDuAn_QuanLyDuAn_QuanLyDuAnId",
                table: "ModuleDuAn",
                column: "QuanLyDuAnId",
                principalTable: "QuanLyDuAn",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaiLieuDinhKemDuAn_QuanLyDuAn_QuanLyDuAnId",
                table: "TaiLieuDinhKemDuAn",
                column: "QuanLyDuAnId",
                principalTable: "QuanLyDuAn",
                principalColumn: "Id");
        }
    }
}
