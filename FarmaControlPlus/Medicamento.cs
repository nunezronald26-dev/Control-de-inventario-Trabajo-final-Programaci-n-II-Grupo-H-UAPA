using System;

namespace FarmaControlPlus
{
    public class Medicamento
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Categoria { get; set; }
        public int Stock { get; set; }
        public decimal PrecioUnitario { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}