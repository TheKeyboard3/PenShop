using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PenShop.Migrations
{
    /// <inheritdoc />
    public partial class FixedRollerballDiameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "RollerballDiameter",
                table: "Product",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RollerballDiameter",
                table: "Product");
        }
    }
}
