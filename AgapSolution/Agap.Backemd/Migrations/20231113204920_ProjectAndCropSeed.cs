using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agap.Backemd.Migrations
{
    /// <inheritdoc />
    public partial class ProjectAndCropSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Crops",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Crops");
        }
    }
}
