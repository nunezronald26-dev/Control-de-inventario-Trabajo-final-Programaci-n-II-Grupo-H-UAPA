using FarmaControlPlus;
using Npgsql;
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
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                using (var conn = ConexionBD.ObtenerConexion())
                {
                    conn.Open();

                    // 1. Total medicamentos
                    int totalMedicamentos = ObtenerTotalMedicamentos(conn);

                    // 2. Medicamentos críticos
                    int medicamentosCriticos = ObtenerMedicamentosCriticos(conn);

                    // 3. Total empleados
                    int totalEmpleados = ObtenerTotalUsuarios(conn);

                    // 4. Sucursales únicas (sin duplicados)
                    int totalSucursales = ObtenerSucursalesUnicas(conn);

                    // 5. Medicamentos agotados
                    int medicamentosAgotados = ObtenerMedicamentosAgotados(conn);

                    // 6. Medicamentos disponibles
                    int medicamentosDisponibles = totalMedicamentos - medicamentosAgotados;

                    // 7. Ventas del día
                    decimal ventasHoy = ObtenerVentasHoy(conn);

                    // 8. Fecha actual
                    string fecha = DateTime.Now.ToString("dd MMMM yyyy");
                    card6Month.Text = char.ToUpper(fecha[0]) + fecha.Substring(1);

                    // Actualizar todos los controles
                    ActualizarControlesDashboard(
                        totalMedicamentos,
                        medicamentosDisponibles,
                        medicamentosAgotados,
                        medicamentosCriticos,
                        totalSucursales,
                        totalEmpleados,
                        ventasHoy
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error cargando dashboard: " + ex.Message);
                // Mostrar valores por defecto en caso de error
                MostrarValoresPorDefecto();
            }
        }

        private int ObtenerTotalMedicamentos(NpgsqlConnection conn)
        {
            string sql = "SELECT COUNT(*) FROM medicamentos";
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int ObtenerMedicamentosCriticos(NpgsqlConnection conn)
        {
            string sql = @"
                SELECT COUNT(*) 
                FROM medicamentos 
                WHERE stock < 10 
                   OR fecha_vencimiento <= CURRENT_DATE + INTERVAL '30 days'";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private int ObtenerTotalUsuarios(NpgsqlConnection conn)
        {
            string sql = "SELECT COUNT(*) FROM empleados";
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // NUEVO: Método para obtener sucursales únicas
        private int ObtenerSucursalesUnicas(NpgsqlConnection conn)
        {
            string sql = @"
                SELECT COUNT(DISTINCT sucursal) 
                FROM empleados 
                WHERE sucursal IS NOT NULL 
                  AND sucursal != ''
                  AND sucursal != ' '";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                var result = cmd.ExecuteScalar();
                return result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }

        private int ObtenerMedicamentosAgotados(NpgsqlConnection conn)
        {
            string sql = "SELECT COUNT(*) FROM medicamentos WHERE stock = 0";
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private decimal ObtenerVentasHoy(NpgsqlConnection conn)
        {
            try
            {
                string checkSql = @"
                    SELECT EXISTS (
                        SELECT FROM information_schema.tables 
                        WHERE table_name = 'ventas'
                    )";

                using (var checkCmd = new NpgsqlCommand(checkSql, conn))
                {
                    bool existe = Convert.ToBoolean(checkCmd.ExecuteScalar());

                    if (existe)
                    {
                        string sql = @"
                            SELECT COALESCE(SUM(total), 0) 
                            FROM ventas 
                            WHERE DATE(fecha_venta) = CURRENT_DATE";

                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            var result = cmd.ExecuteScalar();
                            return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch { }

            return 0;
        }

        private void ActualizarControlesDashboard(
            int totalMedicamentos,
            int disponibles,
            int agotados,
            int criticos,
            int sucursales,
            int empleados,
            decimal ventas)
        {
            // Si tienes un control para total medicamentos, descomenta:
            // if (card1StatValue != null)
            //     card1StatValue.Text = totalMedicamentos.ToString("N0");

            // Medicamentos disponibles
            if (card2StatValue != null)
                card2StatValue.Text = disponibles.ToString("N0");

            // Escasez - Medicamentos agotados
            if (card3Stat1Value != null)
                card3Stat1Value.Text = agotados.ToString("00");

            // Escasez - Medicamentos críticos
            if (card3Stat2Value != null)
                card3Stat2Value.Text = criticos.ToString("00");

            // Mi Farmacia - Sucursales (sin duplicados)
            if (card5Stat1Value != null)
                card5Stat1Value.Text = sucursales.ToString("00");

            // Mi Farmacia - Empleados
            if (card5Stat2Value != null)
                card5Stat2Value.Text = empleados.ToString("00");

            // Reporte Rápido - Ventas hoy
            if (card6StatValue != null)
                card6StatValue.Text = ((int)ventas).ToString("N0");
        }

        private void MostrarValoresPorDefecto()
        {
            // Fecha
            string fecha = DateTime.Now.ToString("dd MMMM yyyy");
            if (card6Month != null)
                card6Month.Text = char.ToUpper(fecha[0]) + fecha.Substring(1);

            // Valores por defecto
            if (card2StatValue != null) card2StatValue.Text = "0";
            if (card3Stat1Value != null) card3Stat1Value.Text = "00";
            if (card3Stat2Value != null) card3Stat2Value.Text = "00";
            if (card5Stat1Value != null) card5Stat1Value.Text = "00";
            if (card5Stat2Value != null) card5Stat2Value.Text = "00";
            if (card6StatValue != null) card6StatValue.Text = "0";
        }

        // Botones de navegación
        private void btnInventario_Click(object sender, EventArgs e)
        {
            var padre = this.ParentForm as Form1;
            if (padre != null)
            {
                padre.btnInventario_Click(null, null);
            }
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            var padre = this.ParentForm as Form1;
            if (padre != null)
            {
                padre.btnUsuarios_Click(null, null);
            }
        }

        // Botón para recargar datos
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
            MessageBox.Show("Datos actualizados", "Dashboard",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // También puedes llamar este método desde el formulario principal
        public void RecargarDatos()
        {
            LoadDashboardData();
        }
    }
}