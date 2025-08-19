namespace cazuela_chapina_api.Models
{
    public class SellDetails
    {
        public int SellDetailsId { get; set; }
        public int SellId { get; set; }
        public int ProductId { get; set; }
        public Sell? Sell { get; set; }

        public int Cantidad { get; set; }

        public int? ComboId { get; set; }
        public Combo? Combo { get; set; }

        public int? BebidaId { get; set; }
        public Inventory? Bebida { get; set; }

        public int? EndulzarId { get; set; }
        public Inventory? Endulzar { get; set; }

        public int? EnvolturaId { get; set; }
        public Inventory? Envoltura { get; set; }

        public int? MasaId { get; set; }
        public Inventory? Masa { get; set; }

        public int? PicanteId { get; set; }
        public Inventory? Picante { get; set; }

        public int? RellenoId { get; set; }
        public Inventory? Relleno { get; set; }

        public bool? Malvavisco { get; set; }
        public string? Presentation { get; set; }

        public decimal Price { get; set; }  


        public int? QuantityBebida { get; set; }
        public int? QuantityTamal { get; set; }

        public bool? Topping { get; set; }
    }

}
