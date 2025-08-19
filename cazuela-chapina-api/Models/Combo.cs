namespace cazuela_chapina_api.Models
{
    public class Combo
    {
        public int ComboId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int IdMasa { get; set; }
        public Inventory Masa { get; set; }

        public int QuantityTamal { get; set; }

        public int IdRelleno { get; set; }
        public Inventory Relleno { get; set; }

        public int IdEnvoltura { get; set; }
        public Inventory Envoltura { get; set; }

        public int IdPicante { get; set; }
        public Inventory Picante { get; set; }

        public int IdBebida { get; set; }
        public Inventory Bebida { get; set; }

        public string Presentation { get; set; }
        public int QuantityBebida { get; set; }
        public float Price { get; set; }
    }

}
