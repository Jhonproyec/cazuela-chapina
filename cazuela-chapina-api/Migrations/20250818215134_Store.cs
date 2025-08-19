using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cazuela_chapina_api.Migrations
{
    /// <inheritdoc />
    public partial class Store : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "store",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    ComboId = table.Column<int>(type: "int", nullable: true),
                    BebidaId = table.Column<int>(type: "int", nullable: true),
                    EndulzarId = table.Column<int>(type: "int", nullable: true),
                    EnvolturaId = table.Column<int>(type: "int", nullable: true),
                    MasaId = table.Column<int>(type: "int", nullable: true),
                    PicanteId = table.Column<int>(type: "int", nullable: true),
                    RellenoId = table.Column<int>(type: "int", nullable: true),
                    Malvavisco = table.Column<bool>(type: "bit", nullable: true),
                    Presentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    QuantityBebida = table.Column<int>(type: "int", nullable: true),
                    QuantityTamal = table.Column<int>(type: "int", nullable: true),
                    Topping = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_store", x => x.StoreId);
                    table.ForeignKey(
                        name: "FK_store_combo_ComboId",
                        column: x => x.ComboId,
                        principalTable: "combo",
                        principalColumn: "ComboId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_store_inventory_BebidaId",
                        column: x => x.BebidaId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_store_inventory_EndulzarId",
                        column: x => x.EndulzarId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_store_inventory_EnvolturaId",
                        column: x => x.EnvolturaId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_store_inventory_MasaId",
                        column: x => x.MasaId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_store_inventory_PicanteId",
                        column: x => x.PicanteId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_store_inventory_RellenoId",
                        column: x => x.RellenoId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_store_BebidaId",
                table: "store",
                column: "BebidaId");

            migrationBuilder.CreateIndex(
                name: "IX_store_ComboId",
                table: "store",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_store_EndulzarId",
                table: "store",
                column: "EndulzarId");

            migrationBuilder.CreateIndex(
                name: "IX_store_EnvolturaId",
                table: "store",
                column: "EnvolturaId");

            migrationBuilder.CreateIndex(
                name: "IX_store_MasaId",
                table: "store",
                column: "MasaId");

            migrationBuilder.CreateIndex(
                name: "IX_store_PicanteId",
                table: "store",
                column: "PicanteId");

            migrationBuilder.CreateIndex(
                name: "IX_store_RellenoId",
                table: "store",
                column: "RellenoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "store");
        }
    }
}
