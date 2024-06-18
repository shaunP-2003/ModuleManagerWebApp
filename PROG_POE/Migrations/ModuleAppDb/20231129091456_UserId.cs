using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG_POE.Migrations.ModuleAppDb
{
    public partial class UserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_AppUser_Id",
                table: "Modules");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Modules",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Modules_Id",
                table: "Modules",
                newName: "IX_Modules_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_AppUser_UserId",
                table: "Modules",
                column: "UserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_AppUser_UserId",
                table: "Modules");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Modules",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Modules_UserId",
                table: "Modules",
                newName: "IX_Modules_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_AppUser_Id",
                table: "Modules",
                column: "Id",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
