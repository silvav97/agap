using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agap.Backemd.Migrations
{
    /// <inheritdoc />
    public partial class ProjectReportAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Crops",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "ProjectReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TotalSale = table.Column<float>(type: "real", nullable: false),
                    TotalBudget = table.Column<float>(type: "real", nullable: false),
                    ExpectedExpense = table.Column<float>(type: "real", nullable: false),
                    RealExpense = table.Column<float>(type: "real", nullable: false),
                    Profit = table.Column<float>(type: "real", nullable: false),
                    Profitability = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectReports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crops_UserId",
                table: "Crops",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectReports_ProjectId",
                table: "ProjectReports",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Crops_AspNetUsers_UserId",
                table: "Crops",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Crops_AspNetUsers_UserId",
                table: "Crops");

            migrationBuilder.DropTable(
                name: "ProjectReports");

            migrationBuilder.DropIndex(
                name: "IX_Crops_UserId",
                table: "Crops");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Crops",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
