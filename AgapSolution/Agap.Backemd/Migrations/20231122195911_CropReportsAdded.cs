using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agap.Backemd.Migrations
{
    /// <inheritdoc />
    public partial class CropReportsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CropReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CropId = table.Column<int>(type: "int", nullable: false),
                    TotalSale = table.Column<float>(type: "real", nullable: false),
                    AssignedBudget = table.Column<float>(type: "real", nullable: false),
                    ExpectedExpense = table.Column<float>(type: "real", nullable: false),
                    RealExpense = table.Column<float>(type: "real", nullable: false),
                    Profit = table.Column<float>(type: "real", nullable: false),
                    Profitability = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CropReports_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CropReports_CropId",
                table: "CropReports",
                column: "CropId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CropReports");
        }
    }
}
