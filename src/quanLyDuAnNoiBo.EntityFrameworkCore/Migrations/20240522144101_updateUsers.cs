using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quanLyDuAnNoiBo.Migrations
{
    /// <inheritdoc />
    public partial class updateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specialize",
                table: "AbpUsers",
                newName: "ChuyenMonId");

            migrationBuilder.RenameColumn(
                name: "Ratio",
                table: "AbpUsers",
                newName: "TyLe");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "AbpUsers",
                newName: "Bac");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TyLe",
                table: "AbpUsers",
                newName: "Ratio");

            migrationBuilder.RenameColumn(
                name: "ChuyenMonId",
                table: "AbpUsers",
                newName: "Specialize");

            migrationBuilder.RenameColumn(
                name: "Bac",
                table: "AbpUsers",
                newName: "Level");
        }
    }
}
