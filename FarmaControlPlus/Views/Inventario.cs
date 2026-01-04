using FarmaControlPlus;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuProyecto.Models;
using TuProyecto.Services;

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
                med.FechaVencimiento.ToString("dd/MM/yyyy") // Cambiado a formato consistente
            );
        }

        private void GuardarMedicamentoBD(Medicamento med)
        {
            using (var conn = ConexionBD.ObtenerConexion())
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
            using (var conn = ConexionBD.ObtenerConexion())
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
                                fechaVencimiento.ToString("dd/MM/yyyy") // Cambiado a formato consistente
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
            AplicarFormatoCelda();

            try
            {
                AdjustGuideLayout();
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
            dataGridViewInventario.RowTemplate.Height = 30;

            dataGridViewInventario.AlternatingRowsDefaultCellStyle.BackColor = Color.LightCyan;
            dataGridViewInventario.DefaultCellStyle.Font = new Font("Arial", 8);
            dataGridViewInventario.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dataGridViewInventario.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridViewInventario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridViewInventario.DefaultCellStyle.Padding = new Padding(8, 3, 8, 3);
            dataGridViewInventario.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dataGridViewInventario.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            if (dataGridViewInventario.Columns.Contains("Nombre"))
            {
                var colNombre = dataGridViewInventario.Columns["Nombre"];
                colNombre.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                colNombre.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                colNombre.DefaultCellStyle.Padding = new Padding(8, 6, 8, 6);
            }

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

        private void AgregarMedicamento(string nombre, string codigo, string categoria, int stock, string precio, DateTime vencimiento)
        {
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

        private DateTime ParsearFecha(string fechaStr)
        {
            // Intentar parsear con el formato usado en el DataGridView
            if (DateTime.TryParseExact(fechaStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
                return fecha;

            // Si falla, intentar con formato corto del sistema
            if (DateTime.TryParse(fechaStr, CultureInfo.CurrentCulture, DateTimeStyles.None, out fecha))
                return fecha;

            // Si todo falla, retornar fecha máxima
            return DateTime.MaxValue;
        }

        private string ObtenerEstadoDesdeFila(DataGridViewRow row)
        {
            int stock = 0;
            DateTime fecha = DateTime.MaxValue;

            if (row.Cells["Stock"].Value != null)
                int.TryParse(row.Cells["Stock"].Value.ToString(), out stock);

            if (row.Cells["FechaVencimiento"].Value != null)
            {
                string fechaStr = row.Cells["FechaVencimiento"].Value.ToString();
                fecha = ParsearFecha(fechaStr);
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
                    string fechaStr = row.Cells["FechaVencimiento"].Value.ToString();
                    DateTime fechaVencimiento = ParsearFecha(fechaStr);

                    if (fechaVencimiento < DateTime.Today)
                        vencidos++;
                    else if (fechaVencimiento <= DateTime.Today.AddDays(30))
                        aPuntoDeVencer++;
                    else if (fechaVencimiento <= DateTime.Today.AddDays(90))
                        porVencer++;
                }
            }
        }

        private void AplicarFormatoCelda()
        {
            Font fontBold = new Font(dataGridViewInventario.Font, FontStyle.Bold);
            Font fontStrikeout = new Font(dataGridViewInventario.Font, FontStyle.Strikeout);
            Font fontNormal = new Font(dataGridViewInventario.Font, FontStyle.Regular);

            foreach (DataGridViewRow row in dataGridViewInventario.Rows)
            {
                if (row.IsNewRow) continue;

                string estado = ObtenerEstadoDesdeFila(row);

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    row.Cells[i].Style = new DataGridViewCellStyle();
                }

                switch (estado)
                {
                    case "VENCIDO":
                        AplicarEstiloFila(row, Color.FromArgb(255, 140, 0), Color.White, fontBold);
                        break;
                    case "SIN STOCK":
                        AplicarEstiloFila(row, Color.FromArgb(245, 245, 245),
                            Color.FromArgb(150, 150, 150), fontStrikeout);
                        break;
                    case "POR VENCER":
                        AplicarEstiloFila(row, Color.FromArgb(255, 255, 200),
                            Color.Black, fontBold);
                        break;
                    case "BAJO STOCK":
                        AplicarEstiloFila(row, Color.FromArgb(200, 0, 0),
                            Color.FromArgb(255, 255, 255), fontBold);
                        break;
                    default:
                        Color back = (row.Index % 2 == 0) ? Color.White : Color.FromArgb(240, 248, 255);
                        AplicarEstiloFila(row, back, Color.Black, fontNormal);
                        break;
                }
            }
        }

        private void AplicarEstiloFila(DataGridViewRow row, Color backColor, Color foreColor, Font font)
        {
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
            CargarMedicamentos();
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
            reporte += "Medicamentos críticos:\n";

            foreach (DataGridViewRow row in dataGridViewInventario.Rows)
            {
                if (row.IsNewRow) continue;

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

                foreach (DataGridViewRow row in dataGridViewInventario.Rows)
                {
                    row.Visible = true;
                }
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            // Crear menú contextual con opciones de exportación
            ContextMenuStrip menu = new ContextMenuStrip();

            menu.Items.Add("Exportar a Excel", null, (s, args) => ExportarAExcel());
            menu.Items.Add("Exportar a PDF", null, (s, args) => ExportarAPDF());
            menu.Items.Add("-");
            menu.Items.Add("Cancelar", null, (s, args) => menu.Close());

            menu.Show(btnExportar, new Point(0, btnExportar.Height));
        }

        private void ExportarAExcel()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo Excel (*.xlsx)|*.xlsx";
                sfd.FileName = $"Inventario_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExcelExportService.ExportarDataGridView(
                        dataGridViewInventario,
                        sfd.FileName,
                        ObtenerEstadoDesdeFila
                    );

                    MessageBox.Show("Inventario exportado a Excel correctamente.",
                        "Exportación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void ExportarAPDF()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";
                sfd.FileName = $"Inventario_{DateTime.Now:yyyyMMdd_HHmm}.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var medicamentos = new List<MedicamentoPDF>();

                    foreach (DataGridViewRow row in dataGridViewInventario.Rows)
                    {
                        if (row.IsNewRow) continue;

                        medicamentos.Add(new MedicamentoPDF
                        {
                            Nombre = row.Cells["Nombre"].Value?.ToString(),
                            Codigo = row.Cells["Codigo"].Value?.ToString(),
                            Categoria = row.Cells["Categoria"].Value?.ToString(),
                            Stock = int.Parse(row.Cells["Stock"].Value.ToString()),
                            Precio = row.Cells["Precio"].Value?.ToString(),
                            Vencimiento = row.Cells["FechaVencimiento"].Value?.ToString(),
                            Estado = ObtenerEstadoDesdeFila(row)
                        });
                    }

                    PdfExportService.ExportarMedicamentos(medicamentos, sfd.FileName);

                    MessageBox.Show("Inventario exportado a PDF correctamente.",
                        "Exportación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }


        private void dataGridViewInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridViewInventario.Rows[e.RowIndex];

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

            groupBoxGuia.AutoSize = false;
            groupBoxGuia.Dock = DockStyle.None;
            groupBoxGuia.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            int padding = 5;
            int desiredWidth = Math.Max(180, lblDetalles.Width);
            desiredWidth = Math.Min(desiredWidth, Math.Max(225, panelDetalles.ClientSize.Width - padding));
            groupBoxGuia.Width = desiredWidth;

            flowLayoutPanelGuia.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelGuia.WrapContents = false;
            flowLayoutPanelGuia.AutoSize = false;

            int itemHeight = 46;
            int itemCount = Math.Max(1, flowLayoutPanelGuia.Controls.Count);

            int availableHeight = Math.Max(0, panelDetalles.ClientSize.Height - (lblDetalles.Bottom + 16));
            int desiredHeight = Math.Min(itemCount * (itemHeight + flowLayoutPanelGuia.Padding.Vertical), Math.Max(itemHeight, availableHeight));
            flowLayoutPanelGuia.Height = desiredHeight;

            flowLayoutPanelGuia.Width = Math.Max(50, groupBoxGuia.ClientSize.Width - flowLayoutPanelGuia.Padding.Horizontal - 6);

            foreach (Control c in flowLayoutPanelGuia.Controls)
            {
                c.Width = Math.Max(50, flowLayoutPanelGuia.ClientSize.Width - 6);
                c.Height = itemHeight;
            }

            groupBoxGuia.Height = flowLayoutPanelGuia.Height + 24;
            groupBoxGuia.Location = new Point(lblDetalles.Left, lblDetalles.Bottom + 6);

            if (groupBoxGuia.Parent != panelDetalles)
            {
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

        private void label4_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void lblPorVencerGuia_Click(object sender, EventArgs e) { }
        private void flowLayoutPanelGuia_Paint(object sender, PaintEventArgs e) { }

        private void Inventario_Load(object sender, EventArgs e)
        {
            CargarMedicamentos();
        }
    }
}