namespace cazuela_chapina_api.DTO
{
    public class StoreCreateDto
    {
        public int StoreId { get; set; }
        public int Cantidad { get; set; }
        public int? ComboId { get; set; }
        public int? IdBebida { get; set; }
        public int? IdEndulzar { get; set; }
        public int? IdEnvoltura { get; set; }
        public int? IdMasa { get; set; }
        public int? IdPicante { get; set; }
        public int? IdRelleno { get; set; }
        public bool? Malvavisco { get; set; }
        public string? Presentation { get; set; }
        public float Price { get; set; }
        public int ProductId { get; set; }
        public int? QuantityBebida { get; set; }
        public int? QuantityTamal { get; set; }
        public bool? Topping { get; set; }

    }
}
