using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Globalization;

namespace TuProyecto.Views
{
    public partial class Reportes : UserControl
    {
        public DateTime FechaReporte { get; set; }
        public decimal TotalVentas { get; private set; }

        public event EventHandler AnteriorClicked;
        public event EventHandler SiguienteClicked;
        public event EventHandler ExportarClicked;
        public event EventHandler FechaCambiada;

        public Reportes()
        {
            InitializeComponent();

            // Configurar eventos que deben estar en el archivo .cs
            ConfigurarEventos();
            InicializarControl();
        }

        private void ConfigurarEventos()
        {
            this.btnExportar.Click += BtnExportar_Click;
            this.dgvReporte.CellFormatting += DgvReporte_CellFormatting;
            this.dgvReporte.RowPostPaint += DgvReporte_RowPostPaint;

            // Comprobar si dtpFecha existe antes de asignar eventos
            if (this.dtpFecha != null)
            {
                this.dtpFecha.ValueChanged += DtpFecha_ValueChanged;
            }
        }

        private void InicializarControl()
        {
            ConfigurarEstilos();

            // Cargar datos de ejemplo
            CargarDatosEjemplo();
        }
        public void SetFecha(DateTime fecha)
        {
            FechaReporte = fecha;

            // Actualizar dtpFecha si existe
            if (this.dtpFecha != null)
            {
                this.dtpFecha.ValueChanged -= DtpFecha_ValueChanged;
                this.dtpFecha.Value = fecha;
                this.dtpFecha.ValueChanged += DtpFecha_ValueChanged;
            }
        }

        public void CargarDatos(DataTable datos)
        {
            if (datos == null)
                return;

            dgvReporte.Rows.Clear();

            decimal total = 0;

            foreach (DataRow fila in datos.Rows)
            {
                int rowIndex = dgvReporte.Rows.Add();
                DataGridViewRow row = dgvReporte.Rows[rowIndex];

                row.Cells["colCodigo"].Value = fila["Codigo"]?.ToString();
                row.Cells["colNombre"].Value = fila["Nombre"]?.ToString();
                row.Cells["colFabricante"].Value = fila["Fabricante"]?.ToString();
                row.Cells["colGramos"].Value = fila["Gramos"]?.ToString();
                row.Cells["colTipo"].Value = fila["Tipo"]?.ToString();

                decimal precio = 0;
                if (fila["Precio"] != DBNull.Value)
                    decimal.TryParse(fila["Precio"].ToString(), out precio);

                row.Cells["colPrecio"].Value = precio.ToString("F2");

                row.Cells["colCantidad"].Value = fila["Cantidad"]?.ToString();

                decimal subtotal = 0;
                if (fila["SubTotal"] != DBNull.Value)
                    decimal.TryParse(fila["SubTotal"].ToString(), out subtotal);

                row.Cells["colSubtotal"].Value = subtotal.ToString("F2");

                total += subtotal;
            }

            TotalVentas = total;
            lblTotal.Text = $"TOTAL:\n{total.ToString("F2")}";
            lblInfoPaginacion.Text = $"Mostrando 1 a {datos.Rows.Count} de {datos.Rows.Count} registros";
        }

        private void ConfigurarEstilos()
        {
            Color colorPrincipal = Color.FromArgb(52, 152, 219);
            Color colorSecundario = Color.FromArgb(46, 204, 113);
            Color colorAccion = Color.FromArgb(155, 89, 182);

            pnlEncabezado.BackColor = Color.WhiteSmoke;

            btnExportar.BackColor = colorAccion;

            btnExportar.ForeColor = Color.White;

            // Configurar botones con estilo plano
            btnExportar.FlatAppearance.BorderSize = 0;

            // Estilos para DateTimePicker si existe
            if (this.dtpFecha != null)
            {
                this.dtpFecha.CalendarForeColor = Color.Black;
                this.dtpFecha.CalendarMonthBackground = Color.White;
                this.dtpFecha.BackColor = Color.White;
                this.dtpFecha.CalendarTitleBackColor = colorPrincipal;
                this.dtpFecha.Font = new Font("Segoe UI", 9F);
            }

            // Estilo alternado filas
            dgvReporte.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 230, 250);
            dgvReporte.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        public void CargarDatosEjemplo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo", typeof(string));
            dt.Columns.Add("Nombre", typeof(string));
            dt.Columns.Add("Fabricante", typeof(string));
            dt.Columns.Add("Gramos", typeof(string));
            dt.Columns.Add("Tipo", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("SubTotal", typeof(decimal));

            dt.Rows.Add("557-11", "Gabapentin", "Actavis", "400", "Tabletas", 60.00m, 1, 60.00m);
            dt.Rows.Add("557-22", "Gabapentin", "Senica", "100", "Tabletas", 0.80m, 1, 0.80m);
            dt.Rows.Add("557-22", "Gabapentin", "Senica", "100", "Tabletas", 0.80m, 1, 0.80m);
            dt.Rows.Add("557-23", "Omeprazol", "Senica", "100", "Tabletas", 0.90m, 1, 0.90m);
            dt.Rows.Add("557-23", "Omeprazol", "Senica", "100", "Tabletas", 0.90m, 1, 0.90m);

            CargarDatos(dt);
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            ExportarClicked?.Invoke(this, EventArgs.Empty);
        }

        private void DtpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpFecha == null)
                return;

            // Actualizar la fecha del reporte y notificar
            FechaReporte = this.dtpFecha.Value.Date;
            FechaCambiada?.Invoke(this, EventArgs.Empty);
        }

        private void DgvReporte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvReporte.Columns["colPrecio"].Index ||
                e.ColumnIndex == dgvReporte.Columns["colSubtotal"].Index)
            {
                if (e.Value != null)
                {
                    decimal valor;
                    if (decimal.TryParse(e.Value.ToString(), out valor))
                    {
                        e.Value = valor.ToString("F2", CultureInfo.InvariantCulture);
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void DgvReporte_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                dgvReporte.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Lavender;
            }
            else
            {
                dgvReporte.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            }
        }

        // Método para exportar a diferentes formatos
        public bool ExportarReporte(string formato, string rutaArchivo)
        {
            try
            {
                switch (formato.ToLower())
                {
                    case "csv":
                        return ExportarCSV(rutaArchivo);
                    case "pdf":
                        return ExportarPDF(rutaArchivo);
                    default:
                        MessageBox.Show("Formato no soportado", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool ExportarCSV(string rutaArchivo)
        {
            // Implementación básica de exportación a CSV
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(rutaArchivo))
            {
                // Escribir encabezados
                writer.WriteLine("Código,Nombre,Fabricante,Grams,Tipo,Precio,Cant.,Sub Total");

                // Escribir datos
                foreach (DataGridViewRow row in dgvReporte.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        writer.WriteLine(
                            $"{row.Cells["colCodigo"].Value}," +
                            $"{row.Cells["colNombre"].Value}," +
                            $"{row.Cells["colFabricante"].Value}," +
                            $"{row.Cells["colGramos"].Value}," +
                            $"{row.Cells["colTipo"].Value}," +
                            $"{row.Cells["colPrecio"].Value}," +
                            $"{row.Cells["colCantidad"].Value}," +
                            $"{row.Cells["colSubtotal"].Value}"
                        );
                    }
                }
            }
            return true;
        }

        private bool ExportarPDF(string rutaArchivo)
        {
            // Para PDF necesitarías una librería como iTextSharp
            MessageBox.Show("Exportación a PDF requiere iTextSharp o similar", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
    }
}