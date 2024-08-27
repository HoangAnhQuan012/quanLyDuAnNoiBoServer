using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quanLyDuAnNoiBo.Migrations
{
    /// <inheritdoc />
    public partial class updateUsers_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChuyenMonId",
                table: "AbpUsers",
                newName: "ChucDanhId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChucDanhId",
                table: "AbpUsers",
                newName: "ChuyenMonId");
        }
    }
}
