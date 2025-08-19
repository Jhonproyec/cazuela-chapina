namespace cazuela_chapina_api.Models
{
    public class Supplier
    {
        public int supplierId { get; set; }
        public required string Name { get; set; }
        public required int Nit { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();


    }
}
