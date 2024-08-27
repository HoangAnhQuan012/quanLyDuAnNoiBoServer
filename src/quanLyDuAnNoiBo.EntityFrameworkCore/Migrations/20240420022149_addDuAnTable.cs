using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quanLyDuAnNoiBo.Migrations
{
    /// <inheritdoc />
    public partial class addDuAnTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuanLyDuAn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDuAn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenDuAn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaTriHopDong = table.Column<double>(type: "float", nullable: true),
                    SoHopDong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDungPhatTrien = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    QuyTrinhPhatTrien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CongNgheSuDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuanLyDuAnId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LuongCoSo = table.Column<double>(type: "float", nullable: false),
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
                    table.PrimaryKey("PK_QuanLyDuAn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModuleDuAn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PM = table.Column<double>(type: "float", nullable: true),
                    Dev = table.Column<double>(type: "float", nullable: true),
                    Test = table.Column<double>(type: "float", nullable: true),
                    BA = table.Column<double>(type: "float", nullable: true),
                    DuAnId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuanLyDuAnId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_ModuleDuAn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleDuAn_QuanLyDuAn_QuanLyDuAnId",
                        column: x => x.QuanLyDuAnId,
                        principalTable: "QuanLyDuAn",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaiLieuDinhKemDuAn",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDinhKem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDinhKem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DuAnId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuanLyDuAnId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.PrimaryKey("PK_TaiLieuDinhKemDuAn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaiLieuDinhKemDuAn_QuanLyDuAn_QuanLyDuAnId",
                        column: x => x.QuanLyDuAnId,
                        principalTable: "QuanLyDuAn",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleDuAn_QuanLyDuAnId",
                table: "ModuleDuAn",
                column: "QuanLyDuAnId");

            migrationBuilder.CreateIndex(
                name: "IX_TaiLieuDinhKemDuAn_QuanLyDuAnId",
                table: "TaiLieuDinhKemDuAn",
                column: "QuanLyDuAnId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModuleDuAn");

            migrationBuilder.DropTable(
                name: "TaiLieuDinhKemDuAn");

            migrationBuilder.DropTable(
                name: "QuanLyDuAn");
        }
    }
}
