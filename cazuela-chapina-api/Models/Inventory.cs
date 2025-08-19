namespace cazuela_chapina_api.Models
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public int SupplierId {  get; set; }
        public Supplier Supplier { get; set; }
        public float Stock {  get; set; }
        public float Mermas { get; set; }
        public int UnitMeasurementId { get; set; }
        public UnitMeasurement UnitMeasurement { get; set; } 
        public float QuantityAprox { get; set; }
        public string Type { get; set; }

    }
}
