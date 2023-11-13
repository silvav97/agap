using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agap.Backemd.Migrations
{
    /// <inheritdoc />
    public partial class SeedProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CropTypeId",
                table: "Crops");

            migrationBuilder.AddColumn<int>(
                name: "CropTypeId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CropTypeId",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "CropTypeId",
                table: "Crops",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
