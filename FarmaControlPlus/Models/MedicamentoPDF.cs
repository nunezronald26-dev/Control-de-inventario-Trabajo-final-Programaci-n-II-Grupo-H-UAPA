using System;

namespace TuProyecto.Models
{
    public class MedicamentoPDF
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Categoria { get; set; }
        public int Stock { get; set; }
        public string Precio { get; set; }
        public string Vencimiento { get; set; }
        public string Estado { get; set; }
    }
}