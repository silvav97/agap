using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agap.Backemd.Migrations
{
    /// <inheritdoc />
    public partial class CropTypeCRUD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CropTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weather = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PlantQuantityPerSquareMeter = table.Column<int>(type: "int", nullable: false),
                    HarvestTime = table.Column<int>(type: "int", nullable: false),
                    FertilizerId = table.Column<int>(type: "int", nullable: false),
                    FertilizerQuantityPerPlant = table.Column<int>(type: "int", nullable: false),
                    FertilizerFrequency = table.Column<int>(type: "int", nullable: false),
                    PesticideId = table.Column<int>(type: "int", nullable: false),
                    PesticideQuantityPerPlant = table.Column<int>(type: "int", nullable: false),
                    PesticideFrequency = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CropTypes_Fertilizers_FertilizerId",
                        column: x => x.FertilizerId,
                        principalTable: "Fertilizers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CropTypes_Pesticides_PesticideId",
                        column: x => x.PesticideId,
                        principalTable: "Pesticides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CropTypes_FertilizerId",
                table: "CropTypes",
                column: "FertilizerId");

            migrationBuilder.CreateIndex(
                name: "IX_CropTypes_Name",
                table: "CropTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CropTypes_PesticideId",
                table: "CropTypes",
                column: "PesticideId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CropTypes");
        }
    }
}
