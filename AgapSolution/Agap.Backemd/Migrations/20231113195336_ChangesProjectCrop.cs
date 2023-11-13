using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agap.Backemd.Migrations
{
    /// <inheritdoc />
    public partial class ChangesProjectCrop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Projects_CropTypeId",
                table: "Projects",
                column: "CropTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_CropTypes_CropTypeId",
                table: "Projects",
                column: "CropTypeId",
                principalTable: "CropTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_CropTypes_CropTypeId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CropTypeId",
                table: "Projects");
        }
    }
}
