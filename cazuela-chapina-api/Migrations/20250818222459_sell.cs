using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cazuela_chapina_api.Migrations
{
    /// <inheritdoc />
    public partial class sell : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "sell");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "sellDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "sellDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "sell",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
