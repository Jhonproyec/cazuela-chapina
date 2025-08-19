using cazuela_chapina_api.Models;

namespace cazuela_chapina_api.DTO
{
    public class StoreSnapshotDto
    {
        public List<ProductDto> Products { get; set; }
        public List<SellDto> Sells { get; set; }
        public List<SellDetailDto> SellDetails { get; set; }
        public List<ComboDto> Combos { get; set; }
        public List<InventoryDto> Inventory { get; set; }
        public List<SupplierDto> Suppliers { get; set; }
    }

    // Productos
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
    }

    // Ventas
    public class SellDto
    {
        public int SellId { get; set; }
        public DateOnly DateSell { get; set; }
        public decimal Total { get; set; }
    }

    // Detalles de ventas
    public class SellDetailDto
    {
        public int SellDetailsId { get; set; }
        public int SellId { get; set; }
        public int ProductId { get; set; }
        public int Cantidad { get; set; }
        public decimal Price { get; set; }
    }

    // Combos
    public class ComboDto
    {
        public int ComboId { get; set; }
        public string Name { get; set; }
    }

    // Inventario
    public class InventoryDto
    {
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
    }

    // Proveedores
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
    }

}
