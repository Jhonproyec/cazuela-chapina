namespace cazuela_chapina_api.DTO
{
    public class InventoryCreateDto
    {
        public string Name { get; set; }
        public int SupplierId { get; set; }
        public float Stock { get; set; }
        public float Mermas { get; set; }
        public int UnitMeasurementId { get; set; }
        public float QuantityAprox { get; set; }
        public string Type { get; set; }
    }
}
