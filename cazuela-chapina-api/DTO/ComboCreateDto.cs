namespace cazuela_chapina_api.DTO
{
    public class ComboCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int IdMasa { get; set; }

        public int QuantityTamal { get; set; }

        public int IdRelleno { get; set; }

        public int IdEnvoltura { get; set; }

        public int IdPicante { get; set; }

        public int IdBebida { get; set; }
        public string Presentation { get; set; }
        public int QuantityBebida { get; set; }
        public float Price { get; set; }
        
    }
}
