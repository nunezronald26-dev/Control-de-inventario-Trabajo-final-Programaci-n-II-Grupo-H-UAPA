using FarmaControlPlus;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TuProyecto.Views
{
    public partial class Inventario : UserControl
    {
        private bool soloCriticos = false;

        private void AgregarMedicamentoAGrid(Medicamento med)
        {
            dataGridViewInventario.Rows.Add(
                med.Nombre,
                med.Codigo,
                med.Categoria,
                med.Stock,
                med.PrecioUnitario.ToString("N2"),
                med.FechaVencimiento.ToShortDateString()
            );
        }

        private void GuardarMedicamentoBD(Medicamento med)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(ConexionBD.CadenaConexion))
            {
                conn.Open();

                string sql = @"INSERT INTO medicamentos
                       (nombre, codigo, categoria, stock, precio_unitario, fecha_vencimiento)
                       VALUES
                       (@nombre, @codigo, @categoria, @stock, @precio, @fecha)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", med.Nombre);
                    cmd.Parameters.AddWithValue("@codigo", med.Codigo);
                    cmd.Parameters.AddWithValue("@categoria", med.Categoria);
                    cmd.Parameters.AddWithValue("@stock", med.Stock);
                    cmd.Parameters.AddWithValue("@precio", med.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@fecha", med.FechaVencimiento);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void CargarMedicamentos()
        {
            dataGridViewInventario.Rows.Clear();
            using (NpgsqlConnection conn = new NpgsqlConnection(ConexionBD.CadenaConexion))
            {
                conn.Open();
                string sql = "SELECT nombre, codigo, categoria, stock, precio_unitario, fecha_vencimiento FROM medicamentos";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombre = reader.GetString(0);
                            string codigo = reader.GetString(1);
                            string categoria = reader.GetString(2);
                            int stock = reader.GetInt32(3);
                            decimal precio = reader.GetDecimal(4);
                            DateTime fechaVencimiento = reader.GetDateTime(5);
                            dataGridViewInventario.Rows.Add(
                                nombre,
                                codigo,
                                categoria,
                                stock,
                                precio.ToString("N2"),
                                fechaVencimiento.ToShortDateString()
                            );
                        }
                    }
                }
            }
            AplicarFormatoCelda();
        }

        public Inventario()
        {
            InitializeComponent();
            ConfigurarEstilos();
            //CargarDatosEjemplo();
            AplicarFormatoCelda();

            // Place the guide under lblDetalles but keep readable sizes and horizontal rectangular boxes.
            // Replace the previous "shrink" logic with safer sizing that uses the flow panel's preferred size
            // and keeps horizontal layout (no wrapping).
            try
            {
                AdjustGuideLayout();
                // keep it responsive when the right panel resizes
                if (panelDetalles != null)
                    panelDetalles.Resize += (s, e) => AdjustGuideLayout();
            }
            catch
            {
                // ignore layout errors at design time
            }
        }

        private void ConfigurarEstilos()
        {
            dataGridViewInventario.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Configurar interlineado
            dataGridViewInventario.RowTemplate.Height = 30;

            // Estilos de DataGridView
            dataGridViewInventario.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
            dataGridViewInventario.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridViewInventario.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dataGridViewInventario.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridViewInventario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Add horizontal padding for all cells (left/right)
            dataGridViewInventario.DefaultCellStyle.Padding = new Padding(8, 3, 8, 3);


            // Enable automatic row height so wrapped text grows the row
            dataGridViewInventario.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;

            // Ensure default wrap mode is off globally (only enable where needed)
            dataGridViewInventario.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // If the "Nombre" column exists, allow wrapping and top-left alignment
            if (dataGridViewInventario.Columns.Contains("Nombre"))
            {
                var colNombre = dataGridViewInventario.Columns["Nombre"];
                colNombre.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                colNombre.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                // provide slightly larger vertical padding for wrapped lines
                colNombre.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);

            }

            // Apply consistent left-right padding to other columns as well
            foreach (DataGridViewColumn c in dataGridViewInventario.Columns)
            {
                if (c.Name != "Nombre")
                {
                    if (c.DefaultCellStyle.Padding == Padding.Empty)
                        c.DefaultCellStyle.Padding = new Padding(8, 3, 8, 3);
                    if (c.DefaultCellStyle.Alignment == DataGridViewContentAlignment.NotSet)
                        c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
            }
        }

        //private void CargarDatosEjemplo()
        //{
        //    // Limpiar datos existentes
        //    dataGridViewInventario.Rows.Clear();

        //    DateTime hoy = DateTime.Now;

        //    // Datos de ejemplo
        //    AgregarMedicamento("Paracetamol 500mg", "PAR001", "Analgésico", 25, "$8.50", hoy.AddDays(15));
        //    AgregarMedicamento("Amoxicilina 500mg", "AMX002", "Antibiótico", 8, "$18.50", hoy.AddDays(60));
        //    AgregarMedicamento("Ibuprofeno 400mg", "IBU003", "Antiinflamatorio", 42, "$12.00", hoy.AddDays(120));
        //    AgregarMedicamento("Omeprazol 20mg", "OME004", "Gastrointestinal", 0, "$15.75", hoy.AddDays(200));
        //    AgregarMedicamento("Loratadina 10mg", "LOR005", "Antialérgico", 3, "$9.80", hoy.AddDays(180));
        //    AgregarMedicamento("Aspirina 100mg", "ASP006", "Analgésico", 15, "$5.20", hoy.AddDays(-10));
        //    AgregarMedicamento("Ranitidina 150mg", "RAN007", "Gastrointestinal", 12, "$10.30", hoy.AddDays(45));
        //    AgregarMedicamento("Metformina 850mg", "MET008", "Diabetes", 0, "$22.90", hoy.AddDays(300));
        //    AgregarMedicamento("Atorvastatina 20mg", "ATO009", "Colesterol", 28, "$45.50", hoy.AddDays(5));
        //    AgregarMedicamento("Losartán 50mg", "LOS010", "Hipertensión", 6, "$28.75", hoy.AddDays(250));

        //    // Actualizar estadísticas
        //    ActualizarEstadisticas();
        //}

        // Add rows matching the DataGridView columns (do NOT add an extra 'Estado' cell)
        private void AgregarMedicamento(string nombre, string codigo, string categoria, int stock, string precio, DateTime vencimiento)
        {
            // Do not store 'estado' as a separate column in the grid; compute on demand when needed
            dataGridViewInventario.Rows.Add(nombre, codigo, categoria, stock, precio,
                vencimiento.ToString("dd/MM/yyyy"));
        }

        private string DeterminarEstado(int stock, DateTime vencimiento)
        {
            if (vencimiento.Date < DateTime.Today)
                return "VENCIDO";
            if (stock == 0)
                return "SIN STOCK";
            if (vencimiento.Date <= DateTime.Today.AddDays(30))
                return "A PUNTO DE VENCER";
            if (vencimiento.Date <= DateTime.Today.AddDays(90))
                return "POR VENCER";
            if (stock < 10)
                return "BAJO STOCK";
            return "NORMAL";
        }

        // Helper: compute estado from the row's Stock and FechaVencimiento cells
        private string ObtenerEstadoDesdeFila(DataGridViewRow row)
        {
            int stock = 0;
            DateTime fecha = DateTime.MaxValue;

            if (row.Cells["Stock"].Value != null)
                int.TryParse(row.Cells["Stock"].Value.ToString(), out stock);

            if (row.Cells["FechaVencimiento"].Value != null)
            {
                DateTime temp;
                string fechaStr = row.Cells["FechaVencimiento"].Value.ToString();
                if (DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out temp))
                    fecha = temp;
            }

            return DeterminarEstado(stock, fecha);
        }

        private void ActualizarEstadisticas()
        {
            int totalMedicamentos = 0;
            int bajosStock = 0;
            int porVencer = 0;
            int aPuntoDeVencer = 0;
            int vencidos = 0;

            foreach (DataGridViewRow row in dataGridViewInventario.Rows)
            {
                if (row.IsNewRow) continue;
                totalMedicamentos++;

                if (row.Cells["Stock"].Value != null)
                {
                    int stock = 0;
                    if (int.TryParse(row.Cells["Stock"].Value.ToString(), out stock))
                    {
                        if (stock < 10 && stock > 0) bajosStock++;
                    }
                }

                if (row.Cells["FechaVencimiento"].Value != null)
                {
                    DateTime fechaVencimiento;
                    string fechaStr = row.Cells["FechaVencimiento"].Value.ToString();

                    if (DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaVencimiento))
                    {
                        if (fechaVencimiento < DateTime.Now)
                            vencidos++;
                        else if (fechaVencimiento <= DateTime.Now.AddDays(30))
                            aPuntoDeVencer++;
                        else if (fechaVencimiento <= DateTime.Now.AddDays(90))
                            porVencer++;
                    }
                }
            }

            // Actualizar textos
            //lblTotal.Text = totalMedicamentos.ToString("N0");
            //lblBajosStock.Text = bajosStock.ToString("N0");
            //lblPorVencer.Text = (vencidos + aPuntoDeVencer + porVencer).ToString("N0");

            // Mantener siempre blanco
            //lblTotal.ForeColor = Color.White;
            //lblBajosStock.ForeColor = Color.White;
            //lblPorVencer.ForeColor = Color.White;
        }

        private void AplicarFormatoCelda()
        {
            // create fonts once to avoid allocations per-row
            Font fontBold = new Font(dataGridViewInventario.Font, FontStyle.Bold);
            Font fontStrikeout = new Font(dataGridViewInventario.Font, FontStyle.Strikeout);
            Font fontNormal = new Font(dataGridViewInventario.Font, FontStyle.Regular);

            foreach (DataGridViewRow row in dataGridViewInventario.Rows)
            {
                if (row.IsNewRow) continue;

                // Compute estado on demand instead of reading a non-existent 'Estado' column
                string estado = ObtenerEstadoDesdeFila(row);

                // Reset per-cell explicit styles so the row default style is authoritative
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    row.Cells[i].Style = new DataGridViewCellStyle();
                }

                // Aplicar formatos según el estado segun la cantidad en stock y fecha de vencimiento
                switch (estado)
                {
                    case "VENCIDO":
                        // Orange background with high-contrast text
                        AplicarEstiloFila(row, Color.FromArgb(255, 140, 0), Color.White, fontBold);
                        break;
                    case "SIN STOCK":
                        // Gray + strikeout (as requested)
                        AplicarEstiloFila(row, Color.FromArgb(245, 245, 245 ),
                            Color.FromArgb(150, 150, 150), fontStrikeout);
                        break;
                    case "POR VENCER":
                        // Yellow background with black text
                        AplicarEstiloFila(row, Color.FromArgb(255, 255, 200),
                            Color.Black, fontBold);
                        break;
                    case "BAJO STOCK":
                        // Light red/pink background, red text for low stock
                        AplicarEstiloFila(row, Color.FromArgb(200, 0, 0),
                            Color.FromArgb(255, 255, 255), fontBold);
                        break;
                    default:
                        // Alternar colores para filas normales using row default style
                        Color back = (row.Index % 2 == 0) ? Color.White : Color.FromArgb(240, 248, 255);
                        AplicarEstiloFila(row, back, Color.Black, fontNormal);
                        break;
                }
            }
        }

        // Apply style at row-level so colors are consistent and not overridden by per-cell styles
        private void AplicarEstiloFila(DataGridViewRow row, Color backColor, Color foreColor, Font font)
        {
            // Set a selection color that is slightly brighter than the row background
            Color selectionBack = Color.FromArgb(
                Math.Min(backColor.R + 30, 255),
                Math.Min(backColor.G + 30, 255),
                Math.Min(backColor.B + 30, 255));

            row.DefaultCellStyle.BackColor = backColor;
            row.DefaultCellStyle.ForeColor = foreColor;
            row.DefaultCellStyle.Font = font;
            row.DefaultCellStyle.SelectionBackColor = selectionBack;
            row.DefaultCellStyle.SelectionForeColor = foreColor;
        }



        private void btnNuevoMedicamento_Click(object sender, EventArgs e)
        {
            FrmAgregarMedicamento frm = new FrmAgregarMedicamento();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                GuardarMedicamentoBD(frm.MedicamentoCreado);
                AgregarMedicamentoAGrid(frm.MedicamentoCreado);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string criterio = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(criterio))
            {
                // Mostrar todos si no hay criterio
                foreach (DataGridViewRow row in dataGridViewInventario.Rows)
                {
                    row.Visible = true;
                }
                return;
            }

            foreach (DataGridViewRow row in dataGridViewInventario.Rows)
            {
                bool visible = false;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null &&
                        cell.Value.ToString().ToLower().Contains(criterio))
                    {
                        visible = true;
                        break;
                    }
                }

                row.Visible = visible;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargarMedicamentos();  // Este método ya carga desde PostgreSQL
            txtBuscar.Clear();
            AplicarFormatoCelda();
            MessageBox.Show("Datos actualizados desde la base de datos",
                "Actualización",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            string reporte = $"REPORTE DE INVENTARIO - {DateTime.Now:dd/MM/yyyy}\n\n";
            //reporte += $"Total medicamentos: {lblTotal.Text}\n";
            //reporte += $"Bajos en stock: {lblBajosStock.Text}\n";
            //reporte += $"Por vencer: {lblPorVencer.Text}\n\n";
            reporte += "Medicamentos críticos:\n";

            foreach (DataGridViewRow row in dataGridViewInventario.Rows)
            {
                if (row.IsNewRow) continue;

                // Compute estado instead of reading a non-existent 'Estado' cell
                string estado = ObtenerEstadoDesdeFila(row);
                if (estado != "NORMAL")
                {
                    int stock = 0;
                    int.TryParse(row.Cells["Stock"].Value?.ToString(), out stock);
                    reporte += $"• {row.Cells["Nombre"].Value} - Stock: {stock} - Estado: {estado}\n";
                }
            }

            MessageBox.Show(reporte, "Reporte de Inventario",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
                e.Handled = true;
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            foreach (DataGridViewRow row in dataGridViewInventario.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnFiltrarCriticos_Click(object sender, EventArgs e)
        {
            soloCriticos = !soloCriticos;

            if (soloCriticos)
            {
                btnFiltrarCriticos.Text = "Mostrar Todos";
                btnFiltrarCriticos.BackColor = Color.LightGreen;

                // Ocultar filas no críticas
                foreach (DataGridViewRow row in dataGridViewInventario.Rows)
                {
                    if (row.IsNewRow) continue;

                    string estado = ObtenerEstadoDesdeFila(row);
                    row.Visible = (estado != "NORMAL");
                }
            }
            else
            {
                btnFiltrarCriticos.Text = "Mostrar Solo Críticos";
                btnFiltrarCriticos.BackColor = Color.LightCoral;

                // Mostrar todas las filas
                foreach (DataGridViewRow row in dataGridViewInventario.Rows)
                {
                    row.Visible = true;
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de exportación a Excel\n(Se implementaría con una librería como EPPlus o ClosedXML)",
                "Exportar a Excel",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void dataGridViewInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridViewInventario.Rows[e.RowIndex];

                // Mostrar detalles del medicamento seleccionado
                string detalles = $"Medicamento: {row.Cells["Nombre"].Value}\n" +
                                 $"Código: {row.Cells["Codigo"].Value}\n" +
                                 $"Categoría: {row.Cells["Categoria"].Value}\n" +
                                 $"Stock: {row.Cells["Stock"].Value} unidades\n" +
                                 $"Precio: {row.Cells["Precio"].Value}\n" +
                                 $"Vencimiento: {row.Cells["FechaVencimiento"].Value}\n";

                lblDetalles.Text = detalles;
            }
        }

        private void groupBoxGuia_Enter(object sender, EventArgs e)
        {

        }

        private void AdjustGuideLayout()
        {
            if (lblDetalles == null || groupBoxGuia == null || flowLayoutPanelGuia == null || panelDetalles == null)
                return;

            // Keep groupBox inside the details panel, not docked
            groupBoxGuia.AutoSize = false;
            groupBoxGuia.Dock = DockStyle.None;
            groupBoxGuia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Choose a readable width inside the right panel (leave some padding)
            int padding = 5;
            int desiredWidth = Math.Max(180, lblDetalles.Width);
            desiredWidth = Math.Min(desiredWidth, Math.Max(225, panelDetalles.ClientSize.Width - padding));
            groupBoxGuia.Width = desiredWidth;

            // Vertical stacking: top-down single column
            flowLayoutPanelGuia.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelGuia.WrapContents = false;
            flowLayoutPanelGuia.AutoSize = false;

            // Fixed item height (matches Designer panels)
            int itemHeight = 46;
            int itemCount = Math.Max(1, flowLayoutPanelGuia.Controls.Count);

            // Height fits items but not larger than available space under lblDetalles
            int availableHeight = Math.Max(0, panelDetalles.ClientSize.Height - (lblDetalles.Bottom + 16));
            int desiredHeight = Math.Min(itemCount * (itemHeight + flowLayoutPanelGuia.Padding.Vertical), Math.Max(itemHeight, availableHeight));
            flowLayoutPanelGuia.Height = desiredHeight;

            // Make flow panel use full width of group box (account for internal padding)
            flowLayoutPanelGuia.Width = Math.Max(50, groupBoxGuia.ClientSize.Width - flowLayoutPanelGuia.Padding.Horizontal - 6);

            // Ensure each legend panel becomes a horizontal rectangle and fills the flow width
            foreach (Control c in flowLayoutPanelGuia.Controls)
            {
                c.Width = Math.Max(50, flowLayoutPanelGuia.ClientSize.Width - 6);
                c.Height = itemHeight;
                // center label text inside if needed (Designer labels already set)
            }

            // Size the group box to fit header + flow panel
            groupBoxGuia.Height = flowLayoutPanelGuia.Height + 24;

            // Position group box under lblDetalles inside panelDetalles
            groupBoxGuia.Location = new Point(lblDetalles.Left, lblDetalles.Bottom + 6);

            // Make sure groupBox is a child of panelDetalles (Designer may already do this)
            if (groupBoxGuia.Parent != panelDetalles)
            {
                // remove from current parent and add to panelDetalles
                try
                {
                    var prevParent = groupBoxGuia.Parent;
                    prevParent?.Controls.Remove(groupBoxGuia);
                    panelDetalles.Controls.Add(groupBoxGuia);
                }
                catch
                {
                    // ignore at design time
                }
            }
        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void lblPorVencerGuia_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanelGuia_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Inventario_Load(object sender, EventArgs e)
        {
            CargarMedicamentos();
        }
    }
}