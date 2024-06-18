using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG_POE.Migrations
{
    public partial class studyTblIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudyHoursRecords",
                columns: table => new
                {
                    StudyRecordId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoursSpent = table.Column<int>(type: "int", nullable: false),
                    ModulesModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyHoursRecords", x => x.StudyRecordId);
                    table.ForeignKey(
                        name: "FK_StudyHoursRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyHoursRecords_Modules_ModulesModuleId",
                        column: x => x.ModulesModuleId,
                        principalTable: "Modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudyHoursRecords_ModulesModuleId",
                table: "StudyHoursRecords",
                column: "ModulesModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyHoursRecords_UserId",
                table: "StudyHoursRecords",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudyHoursRecords");
        }
    }
}
