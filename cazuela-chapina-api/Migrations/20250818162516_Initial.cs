using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cazuela_chapina_api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    isAtole = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    supplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nit = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suppliers", x => x.supplierId);
                });

            migrationBuilder.CreateTable(
                name: "unit_measurement",
                columns: table => new
                {
                    UnitMeasurementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConvertionBase = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit_measurement", x => x.UnitMeasurementId);
                });

            migrationBuilder.CreateTable(
                name: "inventory",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<float>(type: "real", nullable: false),
                    Mermas = table.Column<float>(type: "real", nullable: false),
                    UnitMeasurementId = table.Column<int>(type: "int", nullable: false),
                    QuantityAprox = table.Column<float>(type: "real", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventory", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_inventory_suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "suppliers",
                        principalColumn: "supplierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_inventory_unit_measurement_UnitMeasurementId",
                        column: x => x.UnitMeasurementId,
                        principalTable: "unit_measurement",
                        principalColumn: "UnitMeasurementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "combo",
                columns: table => new
                {
                    ComboId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdMasa = table.Column<int>(type: "int", nullable: false),
                    QuantityTamal = table.Column<int>(type: "int", nullable: false),
                    IdRelleno = table.Column<int>(type: "int", nullable: false),
                    IdEnvoltura = table.Column<int>(type: "int", nullable: false),
                    IdPicante = table.Column<int>(type: "int", nullable: false),
                    IdBebida = table.Column<int>(type: "int", nullable: false),
                    Presentation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityBebida = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_combo", x => x.ComboId);
                    table.ForeignKey(
                        name: "FK_combo_inventory_IdBebida",
                        column: x => x.IdBebida,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_combo_inventory_IdEnvoltura",
                        column: x => x.IdEnvoltura,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_combo_inventory_IdMasa",
                        column: x => x.IdMasa,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_combo_inventory_IdPicante",
                        column: x => x.IdPicante,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_combo_inventory_IdRelleno",
                        column: x => x.IdRelleno,
                        principalTable: "inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_combo_IdBebida",
                table: "combo",
                column: "IdBebida");

            migrationBuilder.CreateIndex(
                name: "IX_combo_IdEnvoltura",
                table: "combo",
                column: "IdEnvoltura");

            migrationBuilder.CreateIndex(
                name: "IX_combo_IdMasa",
                table: "combo",
                column: "IdMasa");

            migrationBuilder.CreateIndex(
                name: "IX_combo_IdPicante",
                table: "combo",
                column: "IdPicante");

            migrationBuilder.CreateIndex(
                name: "IX_combo_IdRelleno",
                table: "combo",
                column: "IdRelleno");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_SupplierId",
                table: "inventory",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_inventory_UnitMeasurementId",
                table: "inventory",
                column: "UnitMeasurementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "combo");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "inventory");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "unit_measurement");
        }
    }
}
