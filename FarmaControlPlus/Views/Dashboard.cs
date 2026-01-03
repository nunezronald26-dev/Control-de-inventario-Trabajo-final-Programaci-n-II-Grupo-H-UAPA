using FarmaControlPlus;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TuProyecto.Views
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            SetupDashboard();
        }

        private void SetupDashboard()
        {
            string fecha = DateTime.Now.ToString("dd MMMM yyyy");
            card6Month.Text = char.ToUpper(fecha[0]) + fecha.Substring(1);
        }

        public void UpdateDashboardData(
            int totalMedicines = 298,
            int medicineGroups = 24,
            int totalSuppliers = 4,
            int totalUsers = 5,
            int medicinesAvailable = 298,
            int medicineShortage = 1,
            int medicinesSold = 70856)
        {
            // Actualizar tarjeta 2 - Medicamentos Disponibles
            card2StatValue.Text = medicinesAvailable.ToString();

            // Actualizar tarjeta 3 - Escasez de Medicina
            card3Stat1Value.Text = medicineShortage.ToString("00");

            // Actualizar tarjeta 5 - Mi Farmacia
            card5Stat1Value.Text = totalSuppliers.ToString("00");
            card5Stat2Value.Text = totalUsers.ToString("00");

            // Actualizar tarjeta 6 - Reporte Rápido
            card6StatValue.Text = medicinesSold.ToString("N0");
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            var padre = this.ParentForm as Form1;
            if (padre != null)
            {
                padre.btnInventario_Click(null, null);
            }
        }
    }
}