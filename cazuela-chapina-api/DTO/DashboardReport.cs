namespace cazuela_chapina_api.DTO
{
    public class DashboardReport
    {
        public decimal TotalVentasDiarias { get; set; }
        public decimal TotalVentasMensuales { get; set; }

        public string TamalMasVendido { get; set; }
        public int CantidadTamalMasVendido { get; set; }

        public string TamalMenosVendido { get; set; }
        public int CantidadTamalMenosVendido { get; set; }

        public string BebidaMasVendida { get; set; }
        public Dictionary<string, int> BebidasPorHorario { get; set; }

        public int Picantes { get; set; }
        public int NoPicantes { get; set; }

        public int Mermas { get; set; }
    }
}
