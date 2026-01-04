using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Globalization;
using FarmaControlPlus; // Para ConexionBD
using Npgsql;

namespace TuProyecto.Views
{
    public partial class Reportes : UserControl
    {
        public DateTime FechaReporte { get; set; }
        public decimal TotalVentas { get; private set; }
        private bool enModoDetalles = false;

        public event EventHandler ExportarClicked;
        public event EventHandler FechaCambiada;

        public Reportes()
        {
            InitializeComponent();
            ConfigurarEventos();
            InicializarControl();
        }

        private void ConfigurarEventos()
        {
            this.btnExportar.Click += BtnExportar_Click;
            this.dgvReporte.CellFormatting += DgvReporte_CellFormatting;
            this.dgvReporte.RowPostPaint += DgvReporte_RowPostPaint;

            if (this.dtpFecha != null)
                this.dtpFecha.ValueChanged += DtpFecha_ValueChanged;
        }

        private void InicializarControl()
        {
            ConfigurarEstilos();
            FechaReporte = DateTime.Now.Date;
            CargarDatosDelDia();
        }

        public void SetFecha(DateTime fecha)
        {
            FechaReporte = fecha;
            if (this.dtpFecha != null)
            {
                this.dtpFecha.ValueChanged -= DtpFecha_ValueChanged;
                this.dtpFecha.Value = fecha;
                this.dtpFecha.ValueChanged += DtpFecha_ValueChanged;
            }
        }

        // Método principal para cargar datos de ventas del día
        public void CargarDatosDelDia()
        {
            if (dtpFecha == null) return;

            DateTime fechaSeleccionada = dtpFecha.Value.Date;
            DateTime fechaSiguiente = fechaSeleccionada.AddDays(1);

            try
            {
                using (var conn = ConexionBD.ObtenerConexion())
                {
                    conn.Open();

                    // Consulta para obtener el resumen de ventas del día
                    string sql = @"
                    SELECT 
                        v.codigo_venta,
                        v.fecha_venta,
                        e.nombre_completo as empleado,
                        v.subtotal,
                        v.total,
                        v.metodo_pago,
                        COUNT(dv.id) as items
                    FROM ventas v
                    LEFT JOIN empleados e ON v.id_empleado = e.id
                    LEFT JOIN detalle_ventas dv ON v.id = dv.id_venta
                    WHERE v.fecha_venta >= @fechaInicio 
                      AND v.fecha_venta < @fechaFin
                    GROUP BY v.id, v.codigo_venta, v.fecha_venta, e.nombre_completo, 
                             v.subtotal, v.total, v.metodo_pago
                    ORDER BY v.fecha_venta DESC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaSeleccionada);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaSiguiente);

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            CargarDatosResumen(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                CargarDatosEjemplo();
            }
        }

        // Método para cargar datos de resumen en el DataGridView
        public void CargarDatosResumen(DataTable datos)
        {
            if (datos == null)
                return;

            dgvReporte.Rows.Clear();
            enModoDetalles = false;
            decimal totalVentasDia = 0;

            foreach (DataRow fila in datos.Rows)
            {
                int rowIndex = dgvReporte.Rows.Add();
                DataGridViewRow row = dgvReporte.Rows[rowIndex];

                // Código de venta
                row.Cells["colCodigo"].Value = fila["codigo_venta"]?.ToString();

                // Fecha y hora formateada
                if (fila["fecha_venta"] != DBNull.Value)
                {
                    DateTime fechaVenta = Convert.ToDateTime(fila["fecha_venta"]);
                    row.Cells["colNombre"].Value = fechaVenta.ToString("HH:mm");
                }
                else
                {
                    row.Cells["colNombre"].Value = "";
                }

                // Empleado
                row.Cells["colFabricante"].Value = fila["empleado"]?.ToString();

                // Método de pago
                row.Cells["colGramos"].Value = fila["metodo_pago"]?.ToString();

                // Cantidad de items
                row.Cells["colTipo"].Value = fila["items"]?.ToString();

                // Obtener lista de medicamentos (separada)
                string codigoVenta = fila["codigo_venta"]?.ToString();
                string medicamentos = ObtenerListaMedicamentos(codigoVenta);
                row.Cells["colPrecio"].Value = medicamentos;

                // Subtotal
                decimal subtotal = 0;
                if (fila["subtotal"] != DBNull.Value)
                    decimal.TryParse(fila["subtotal"].ToString(), out subtotal);
                row.Cells["colCantidad"].Value = subtotal.ToString("F2");

                // Total
                decimal total = 0;
                if (fila["total"] != DBNull.Value)
                    decimal.TryParse(fila["total"].ToString(), out total);
                row.Cells["colSubtotal"].Value = total.ToString("F2");

                totalVentasDia += total;
            }

            TotalVentas = totalVentasDia;
            lblTotal.Text = $"TOTAL DEL DÍA: {totalVentasDia.ToString("C", CultureInfo.CreateSpecificCulture("es-PA"))}";
            lblInfoPaginacion.Text = $"Mostrando {datos.Rows.Count} ventas del {FechaReporte.ToString("dd/MM/yyyy")}";
            lblTitulo.Text = "Ventas diarias:";
        }

        // Método auxiliar para obtener la lista de medicamentos de una venta
        private string ObtenerListaMedicamentos(string codigoVenta)
        {
            if (string.IsNullOrEmpty(codigoVenta))
                return "";

            try
            {
                using (var conn = ConexionBD.ObtenerConexion())
                {
                    conn.Open();

                    string sql = @"
                    SELECT m.nombre
                    FROM detalle_ventas dv
                    INNER JOIN medicamentos m ON dv.id_medicamento = m.id
                    INNER JOIN ventas v ON dv.id_venta = v.id
                    WHERE v.codigo_venta = @codigoVenta
                    ORDER BY m.nombre
                    LIMIT 3"; // Limitar a 3 medicamentos para el resumen

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigoVenta", codigoVenta);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var medicamentos = new System.Text.StringBuilder();
                            while (reader.Read())
                            {
                                if (medicamentos.Length > 0)
                                    medicamentos.Append(", ");
                                medicamentos.Append(reader.GetString(0));
                            }

                            // Si hay más de 3, agregar "..."
                            if (!reader.IsClosed) reader.Close();

                            // Verificar si hay más
                            cmd.CommandText = "SELECT COUNT(*) FROM detalle_ventas dv INNER JOIN ventas v ON dv.id_venta = v.id WHERE v.codigo_venta = @codigoVenta";
                            int total = Convert.ToInt32(cmd.ExecuteScalar());

                            if (total > 3)
                                medicamentos.Append("...");

                            return medicamentos.ToString();
                        }
                    }
                }
            }
            catch
            {
                return "";
            }
        }

        // Método para cargar detalles de una venta específica
        public void CargarDetallesVenta(string codigoVenta)
        {
            try
            {
                using (var conn = ConexionBD.ObtenerConexion())
                {
                    conn.Open();

                    string sql = @"
                    SELECT 
                        m.codigo,
                        m.nombre,
                        m.fabricante,
                        m.gramaje,
                        m.tipo,
                        dv.precio_unitario,
                        dv.cantidad,
                        dv.subtotal
                    FROM detalle_ventas dv
                    INNER JOIN medicamentos m ON dv.id_medicamento = m.id
                    INNER JOIN ventas v ON dv.id_venta = v.id
                    WHERE v.codigo_venta = @codigoVenta
                    ORDER BY m.nombre";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@codigoVenta", codigoVenta);

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            CargarDetallesEnGrid(dt, codigoVenta);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar detalles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para mostrar detalles en el grid
        private void CargarDetallesEnGrid(DataTable datos, string codigoVenta)
        {
            dgvReporte.Rows.Clear();
            enModoDetalles = true;
            decimal totalVenta = 0;

            foreach (DataRow fila in datos.Rows)
            {
                int rowIndex = dgvReporte.Rows.Add();
                DataGridViewRow row = dgvReporte.Rows[rowIndex];

                row.Cells["colCodigo"].Value = fila["codigo"]?.ToString();
                row.Cells["colNombre"].Value = fila["nombre"]?.ToString();
                row.Cells["colFabricante"].Value = fila["fabricante"]?.ToString();
                row.Cells["colGramos"].Value = fila["gramaje"]?.ToString();
                row.Cells["colTipo"].Value = fila["tipo"]?.ToString();

                decimal precio = 0;
                if (fila["precio_unitario"] != DBNull.Value)
                    decimal.TryParse(fila["precio_unitario"].ToString(), out precio);
                row.Cells["colPrecio"].Value = precio.ToString("F2");

                row.Cells["colCantidad"].Value = fila["cantidad"]?.ToString();

                decimal subtotal = 0;
                if (fila["subtotal"] != DBNull.Value)
                    decimal.TryParse(fila["subtotal"].ToString(), out subtotal);
                row.Cells["colSubtotal"].Value = subtotal.ToString("F2");

                totalVenta += subtotal;
            }

            TotalVentas = totalVenta;
            lblTotal.Text = $"TOTAL VENTA: {totalVenta.ToString("C", CultureInfo.CreateSpecificCulture("es-PA"))}";
            lblInfoPaginacion.Text = $"Mostrando {datos.Rows.Count} items de la venta {codigoVenta}";
            lblTitulo.Text = $"Detalles de Venta: {codigoVenta}";
        }

        private void ConfigurarEstilos()
        {
            pnlEncabezado.BorderStyle = BorderStyle.FixedSingle;

            lblTitulo.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // Configurar DataGridView
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReporte.RowTemplate.Height = 30;
            dgvReporte.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 255);
            dgvReporte.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvReporte.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvReporte.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
            dgvReporte.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvReporte.DefaultCellStyle.Padding = new Padding(6, 3, 6, 3);

            // Habilitar doble clic para ver detalles
            dgvReporte.CellDoubleClick += DgvReporte_CellDoubleClick;

            // Botón Exportar
            btnExportar.BackColor = Color.FromArgb(46, 204, 113);
            btnExportar.FlatStyle = FlatStyle.Flat;
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnExportar.ForeColor = Color.White;
            btnExportar.Text = "Exportar Reporte";

            // Labels
            lblTotal.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblInfoPaginacion.Font = new Font("Segoe UI", 9);
            lblInfoPaginacion.ForeColor = Color.FromArgb(100, 100, 100);

            // DateTimePicker
            if (this.dtpFecha != null)
            {
                this.dtpFecha.Font = new Font("Segoe UI", 9);
            }
        }

        // Evento para ver detalles al hacer doble clic
        private void DgvReporte_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvReporte.Rows.Count) return;

            if (enModoDetalles)
            {
                // Si ya está en modo detalles, no hacer nada o regresar
                return;
            }

            string codigoVenta = dgvReporte.Rows[e.RowIndex].Cells["colCodigo"].Value?.ToString();
            if (!string.IsNullOrEmpty(codigoVenta))
            {
                CargarDetallesVenta(codigoVenta);
            }
        }

        // Método para regresar a la vista de resumen del día
        public void VolverAResumenDia()
        {
            CargarDatosDelDia();
        }

        // Evento cuando se presiona una tecla en el control
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && enModoDetalles)
            {
                VolverAResumenDia();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CargarDatosEjemplo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("codigo_venta", typeof(string));
            dt.Columns.Add("fecha_venta", typeof(DateTime));
            dt.Columns.Add("empleado", typeof(string));
            dt.Columns.Add("subtotal", typeof(decimal));
            dt.Columns.Add("total", typeof(decimal));
            dt.Columns.Add("metodo_pago", typeof(string));
            dt.Columns.Add("items", typeof(int));

            dt.Rows.Add("VTA-001", DateTime.Now.AddHours(-2), "Juan Pérez", 120.50m, 120.50m, "Efectivo", 3);
            dt.Rows.Add("VTA-002", DateTime.Now.AddHours(-1), "María López", 85.75m, 85.75m, "Tarjeta", 2);
            dt.Rows.Add("VTA-003", DateTime.Now, "Carlos Gómez", 210.25m, 210.25m, "Efectivo", 4);

            CargarDatosResumen(dt);
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "CSV files (*.csv)|*.csv|Todos los archivos (*.*)|*.*";
            saveDialog.FileName = enModoDetalles ?
                $"Detalles_Venta_{DateTime.Now:yyyyMMdd_HHmmss}.csv" :
                $"Reporte_Ventas_{FechaReporte:yyyyMMdd}.csv";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                ExportarReporte("csv", saveDialog.FileName);
            }
        }

        private void DtpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (this.dtpFecha == null) return;

            FechaReporte = this.dtpFecha.Value.Date;
            CargarDatosDelDia();
            FechaCambiada?.Invoke(this, EventArgs.Empty);
        }

        private void DgvReporte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Formatear columnas de moneda
            if ((e.ColumnIndex == dgvReporte.Columns["colCantidad"].Index && enModoDetalles) ||
                (e.ColumnIndex == dgvReporte.Columns["colSubtotal"].Index))
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    decimal valor;
                    if (decimal.TryParse(e.Value.ToString(), out valor))
                    {
                        e.Value = valor.ToString("C", CultureInfo.CreateSpecificCulture("es-PA"));
                        e.FormattingApplied = true;
                    }
                }
            }

            // Para colCantidad en modo resumen (es subtotal)
            if (e.ColumnIndex == dgvReporte.Columns["colCantidad"].Index && !enModoDetalles)
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    decimal valor;
                    if (decimal.TryParse(e.Value.ToString(), out valor))
                    {
                        e.Value = valor.ToString("C", CultureInfo.CreateSpecificCulture("es-PA"));
                        e.FormattingApplied = true;
                    }
                }
            }

            // Para colPrecio en modo detalles (es precio unitario)
            if (e.ColumnIndex == dgvReporte.Columns["colPrecio"].Index && enModoDetalles)
            {
                if (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()))
                {
                    decimal valor;
                    if (decimal.TryParse(e.Value.ToString(), out valor))
                    {
                        e.Value = valor.ToString("C", CultureInfo.CreateSpecificCulture("es-PA"));
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void DgvReporte_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Filas alternadas
            if (e.RowIndex % 2 == 0)
                dgvReporte.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(248, 248, 255);
            else
                dgvReporte.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }

        public bool ExportarReporte(string formato, string rutaArchivo)
        {
            try
            {
                switch (formato.ToLower())
                {
                    case "csv":
                        return ExportarCSV(rutaArchivo);
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
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(rutaArchivo))
                {
                    if (enModoDetalles)
                    {
                        writer.WriteLine("Reporte de Detalles de Venta");
                        writer.WriteLine($"Fecha: {FechaReporte.ToString("dd/MM/yyyy")}");
                        writer.WriteLine($"Código de Venta: {lblTitulo.Text.Replace("Detalles de Venta: ", "")}");
                        writer.WriteLine();
                        writer.WriteLine("Código,Nombre,Fabricante,Gramaje,Tipo,Precio Unitario,Cantidad,Subtotal");
                    }
                    else
                    {
                        writer.WriteLine("Reporte de Ventas Diarias");
                        writer.WriteLine($"Fecha: {FechaReporte.ToString("dd/MM/yyyy")}");
                        writer.WriteLine($"Total del día: {TotalVentas.ToString("C", CultureInfo.CreateSpecificCulture("es-PA"))}");
                        writer.WriteLine();
                        writer.WriteLine("Código Venta,Hora,Empleado,Método Pago,Items,Medicamentos,Subtotal,Total");
                    }

                    foreach (DataGridViewRow row in dgvReporte.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            if (enModoDetalles)
                            {
                                writer.WriteLine(
                                    $"\"{row.Cells["colCodigo"].Value}\"," +
                                    $"\"{row.Cells["colNombre"].Value}\"," +
                                    $"\"{row.Cells["colFabricante"].Value}\"," +
                                    $"\"{row.Cells["colGramos"].Value}\"," +
                                    $"\"{row.Cells["colTipo"].Value}\"," +
                                    $"{row.Cells["colPrecio"].Value}," +
                                    $"{row.Cells["colCantidad"].Value}," +
                                    $"{row.Cells["colSubtotal"].Value}"
                                );
                            }
                            else
                            {
                                writer.WriteLine(
                                    $"\"{row.Cells["colCodigo"].Value}\"," +
                                    $"\"{row.Cells["colNombre"].Value}\"," +
                                    $"\"{row.Cells["colFabricante"].Value}\"," +
                                    $"\"{row.Cells["colGramos"].Value}\"," +
                                    $"{row.Cells["colTipo"].Value}," +
                                    $"\"{row.Cells["colPrecio"].Value}\"," +
                                    $"{row.Cells["colCantidad"].Value}," +
                                    $"{row.Cells["colSubtotal"].Value}"
                                );
                            }
                        }
                    }
                }

                MessageBox.Show($"Reporte exportado exitosamente a:\n{rutaArchivo}", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar CSV: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}