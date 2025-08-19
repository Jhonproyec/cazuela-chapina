namespace cazuela_chapina_api.Models
{
    public class UnitMeasurement
    {
        public int UnitMeasurementId { get; set; }
        public string Name { get; set; }
        public float ConvertionBase { get; set; }
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}
