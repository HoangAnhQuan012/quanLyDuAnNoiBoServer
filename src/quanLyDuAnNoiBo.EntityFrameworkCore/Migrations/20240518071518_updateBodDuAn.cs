using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quanLyDuAnNoiBo.Migrations
{
    /// <inheritdoc />
    public partial class updateBodDuAn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuanLyDuAnId",
                table: "SprintDuAn",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QuanLyDuAnId",
                table: "ModuleDuAn",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SprintDuAn_QuanLyDuAnId",
                table: "SprintDuAn",
                column: "QuanLyDuAnId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleDuAn_QuanLyDuAnId",
                table: "ModuleDuAn",
                column: "QuanLyDuAnId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleDuAn_DuAn_QuanLyDuAnId",
                table: "ModuleDuAn",
                column: "QuanLyDuAnId",
                principalTable: "DuAn",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SprintDuAn_DuAn_QuanLyDuAnId",
                table: "SprintDuAn",
                column: "QuanLyDuAnId",
                principalTable: "DuAn",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleDuAn_DuAn_QuanLyDuAnId",
                table: "ModuleDuAn");

            migrationBuilder.DropForeignKey(
                name: "FK_SprintDuAn_DuAn_QuanLyDuAnId",
                table: "SprintDuAn");

            migrationBuilder.DropIndex(
                name: "IX_SprintDuAn_QuanLyDuAnId",
                table: "SprintDuAn");

            migrationBuilder.DropIndex(
                name: "IX_ModuleDuAn_QuanLyDuAnId",
                table: "ModuleDuAn");

            migrationBuilder.DropColumn(
                name: "QuanLyDuAnId",
                table: "SprintDuAn");

            migrationBuilder.DropColumn(
                name: "QuanLyDuAnId",
                table: "ModuleDuAn");
        }
    }
}
