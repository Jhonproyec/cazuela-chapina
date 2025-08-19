using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cazuela_chapina_api.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "store");

            migrationBuilder.CreateTable(
                name: "sell",
                columns: table => new
                {
                    SellId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DateSell = table.Column<DateOnly>(type: "date", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sell", x => x.SellId);
                });

            migrationBuilder.CreateTable(
                name: "sellDetails",
                columns: table => new
                {
                    SellDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellId = table.Column<int>(type: "int", nullable: false),
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
                    QuantityBebida = table.Column<int>(type: "int", nullable: true),
                    QuantityTamal = table.Column<int>(type: "int", nullable: true),
                    Topping = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sellDetails", x => x.SellDetailsId);
                    table.ForeignKey(
                        name: "FK_sellDetails_combo_ComboId",
                        column: x => x.ComboId,
                        principalTable: "combo",
                        principalColumn: "ComboId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sellDetails_inventory_BebidaId",
                        column: x => x.BebidaId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sellDetails_inventory_EndulzarId",
                        column: x => x.EndulzarId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sellDetails_inventory_EnvolturaId",
                        column: x => x.EnvolturaId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sellDetails_inventory_MasaId",
                        column: x => x.MasaId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sellDetails_inventory_PicanteId",
                        column: x => x.PicanteId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sellDetails_inventory_RellenoId",
                        column: x => x.RellenoId,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_sellDetails_sell_SellId",
                        column: x => x.SellId,
                        principalTable: "sell",
                        principalColumn: "SellId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sellDetails_BebidaId",
                table: "sellDetails",
                column: "BebidaId");

            migrationBuilder.CreateIndex(
                name: "IX_sellDetails_ComboId",
                table: "sellDetails",
                column: "ComboId");

            migrationBuilder.CreateIndex(
                name: "IX_sellDetails_EndulzarId",
                table: "sellDetails",
                column: "EndulzarId");

            migrationBuilder.CreateIndex(
                name: "IX_sellDetails_EnvolturaId",
                table: "sellDetails",
                column: "EnvolturaId");

            migrationBuilder.CreateIndex(
                name: "IX_sellDetails_MasaId",
                table: "sellDetails",
                column: "MasaId");

            migrationBuilder.CreateIndex(
                name: "IX_sellDetails_PicanteId",
                table: "sellDetails",
                column: "PicanteId");

            migrationBuilder.CreateIndex(
                name: "IX_sellDetails_RellenoId",
                table: "sellDetails",
                column: "RellenoId");

            migrationBuilder.CreateIndex(
                name: "IX_sellDetails_SellId",
                table: "sellDetails",
                column: "SellId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sellDetails");

            migrationBuilder.DropTable(
                name: "sell");

            migrationBuilder.CreateTable(
                name: "store",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BebidaId = table.Column<int>(type: "int", nullable: true),
                    ComboId = table.Column<int>(type: "int", nullable: true),
                    EndulzarId = table.Column<int>(type: "int", nullable: true),
                    EnvolturaId = table.Column<int>(type: "int", nullable: true),
                    MasaId = table.Column<int>(type: "int", nullable: true),
                    PicanteId = table.Column<int>(type: "int", nullable: true),
                    RellenoId = table.Column<int>(type: "int", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
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
    }
}
