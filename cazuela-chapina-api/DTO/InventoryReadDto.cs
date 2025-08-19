namespace cazuela_chapina_api.DTO
{
    public class InventoryReadDto
    {
        public int InventoryId { get; set; }
        public string Name { get; set; }
        public float Stock { get; set; }
        public float Mermas { get; set; }
        public float QuantityAprox { get; set; }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public int UnitMeasurementId { get; set; }
        public string UnitMeasurementName { get; set; }
        public string Type { get; set; }
    }
}
