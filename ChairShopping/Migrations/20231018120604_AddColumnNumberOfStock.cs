using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChairShopping.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnNumberOfStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfStock",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfStock",
                table: "products");
        }
    }
}
