using Microsoft.VisualBasic;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using TuProyecto.Services;
using TuProyecto.Models;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuProyecto.Services;
namespace FarmaControlPlus



{
    public partial class Venta : Form
    {
        public Venta()
        {
            InitializeComponent();
        }

        private VentaService ventaService = new VentaService();

        private void Venta_Load(object sender, EventArgs e)
        {
            CargarMedicamentos();

            
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Acción";
            btn.Text = "Agregar";
            btn.Name = "btnAgregar";
            btn.UseColumnTextForButtonValue = true;
            Ventagrid.Columns.Add(btn);

            
            dgvDetalle.Columns.Add("Nombre", "Medicamento");
            dgvDetalle.Columns.Add("Cantidad", "Cant.");
            dgvDetalle.Columns.Add("Precio", "Precio");
            dgvDetalle.Columns.Add("Subtotal", "Subtotal");
        }

        private void CargarMedicamentos()
        {
            using (var conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                string sql = @"
            SELECT 
                nombre,
                precio_unitario AS precio,
                stock
            FROM medicamentos
            WHERE stock > 0
            ORDER BY nombre;
        ";

                using (var da = new NpgsqlDataAdapter(sql, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Ventagrid.DataSource = dt;
                }
            }
        }

        private void AbrirCantidad(int rowIndex)
        {
            string nombre = Ventagrid.Rows[rowIndex].Cells["nombre"].Value.ToString();
            decimal precio = Convert.ToDecimal(Ventagrid.Rows[rowIndex].Cells["precio"].Value);

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Cantidad para {nombre}:", "Cantidad", "1"
            );

            if (int.TryParse(input, out int cant) && cant > 0)
            {
                AgregarDetalle(nombre, cant, precio);
            }
        }

        private void AgregarDetalle(string nombre, int cantidad, decimal precio)
        {
            decimal subtotal = cantidad * precio;
            dgvDetalle.Rows.Add(nombre, cantidad, precio, subtotal);
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                if (row.Cells["Subtotal"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
                }
            }

            txtTotal.Text = total.ToString("N2");
        }

        private void Ventagrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar que no se presione el header
            if (e.RowIndex < 0)
                return;

            // Verificar si se hizo clic en el botón "Agregar"
            if (e.ColumnIndex == Ventagrid.Columns["btnAgregar"].Index)
            {
                string nombre = Ventagrid.Rows[e.RowIndex].Cells["nombre"].Value.ToString();
                decimal precio = Convert.ToDecimal(Ventagrid.Rows[e.RowIndex].Cells["precio"].Value);
                int stock = Convert.ToInt32(Ventagrid.Rows[e.RowIndex].Cells["stock"].Value);

                // Pedir cantidad
                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Cantidad para {nombre} (Disponible: {stock}):",
                    "Cantidad",
                    "1"
                );

                // Validar entrada
                if (int.TryParse(input, out int cantidad) && cantidad > 0)
                {
                    if (cantidad > stock)
                    {
                        MessageBox.Show("La cantidad excede el stock disponible.");
                        return;
                    }

                    // Aquí agregar al detalle
                    AgregarDetalle(nombre, cantidad, precio);
                }
            }
        }

        private void Ventagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void BuscarMedicamentos(string filtro)
        {
            using (var conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                string sql = @"
            SELECT 
                nombre, 
                precio_unitario AS precio,
                stock
            FROM medicamentos
            WHERE nombre ILIKE @filtro
              AND stock > 0
            ORDER BY nombre;
        ";

                using (var da = new NpgsqlDataAdapter(sql, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@filtro", "%" + filtro + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Ventagrid.DataSource = dt;
                }
            }
        }

        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarMedicamentos(txtBuscar.Text.Trim());
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarMedicamentos(txtBuscar.Text.Trim());
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count == 0)
            {
                MessageBox.Show("No hay productos en la venta.");
                return;
            }

            List<DetalleFacturaPDF> detalles = new List<DetalleFacturaPDF>();

            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                if (row.Cells["Nombre"].Value != null)
                {
                    detalles.Add(new DetalleFacturaPDF
                    {
                        Medicamento = row.Cells["Nombre"].Value.ToString(),
                        Cantidad = Convert.ToInt32(row.Cells["Cantidad"].Value),
                        Precio = row.Cells["Precio"].Value.ToString(),
                        Subtotal = row.Cells["Subtotal"].Value.ToString()
                    });
                }
            }

            string ruta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"Factura_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            );

            try
            {
                // 1️⃣ Descontar stock usando el SERVICIO
                foreach (var d in detalles)
                {
                    ventaService.DescontarStock(d.Medicamento, d.Cantidad);
                }

                // 2️⃣ Generar factura PDF
                PdfExportService.ExportarFacturaVenta(detalles, txtTotal.Text, ruta);

                // 3️⃣ Abrir la factura
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = ruta,
                    UseShellExecute = true
                });

                // 4️⃣ Refrescar inventario
                CargarMedicamentos();

                // 5️⃣ Limpiar venta
                dgvDetalle.Rows.Clear();
                txtTotal.Text = "0.00";

                MessageBox.Show(
                    "Venta realizada correctamente",
                    "Venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error en la venta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
