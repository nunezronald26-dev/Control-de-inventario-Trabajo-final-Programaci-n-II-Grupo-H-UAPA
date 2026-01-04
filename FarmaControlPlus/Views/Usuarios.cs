using FarmaControlPlus;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TuProyecto.Views
{
    public partial class Usuarios : UserControl
    {
        private List<Empleado> empleados;
        private List<Empleado> empleadosFiltrados;
        private Color colorCabecera = Color.FromArgb(52, 152, 219);
        private Color colorBoton = Color.FromArgb(46, 204, 113);
        private Color colorBotonBuscar = Color.FromArgb(52, 152, 219);
        private Color colorBotonLimpiar = Color.FromArgb(149, 165, 166);
        private Color colorTexto = Color.FromArgb(52, 73, 94);
        private Color colorFondoBusqueda = Color.FromArgb(245, 246, 250);
        private Color colorBotonModificar = Color.FromArgb(52, 152, 219); // Azul para modificar
        private Color colorBotonEliminar = Color.FromArgb(231, 76, 60);   // Rojo para eliminar

        public Usuarios()
        {
            InitializeComponent();
            ConfigurarGrid();
            CargarEmpleados();
            ConfigurarEstilos();
        }

        private void ConfigurarEstilos()
        {
            // Panel de búsqueda
            panelBusqueda.BackColor = Color.FromArgb(248, 249, 250);

            // Botón Nuevo Usuario
            btnNuevoUsuario.BackColor = Color.FromArgb(46, 204, 113); // Verde
            btnNuevoUsuario.FlatStyle = FlatStyle.Flat;
            btnNuevoUsuario.FlatAppearance.BorderSize = 0;
            btnNuevoUsuario.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnNuevoUsuario.ForeColor = Color.White;

            // Botón Buscar
            btnBuscar.BackColor = Color.FromArgb(70, 130, 180);
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;

            // Botón Limpiar
            btnLimpiar.BackColor = Color.FromArgb(108, 117, 125);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.White;

            // Configurar estilos del DataGridView
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.RowTemplate.Height = 35;
            dgvUsuarios.DefaultCellStyle.Padding = new Padding(6, 4, 6, 4);
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 255);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            dgvUsuarios.DefaultCellStyle.ForeColor = colorTexto;
            dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionBackColor = colorFondoBusqueda;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionForeColor = colorTexto;
        }

        private void CargarEmpleados()
        {
            // Limpiar la lista en memoria
            empleados = new List<Empleado>();

            using (var conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                string sql = @"SELECT id, nombre_completo, correo, direccion, telefono, sucursal, rol
                       FROM empleados
                       ORDER BY id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    dgvUsuarios.Rows.Clear();

                    while (dr.Read())
                    {
                        var emp = new Empleado
                        {
                            ID = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                            Correo = dr.GetString(2),
                            Direccion = dr.GetString(3),
                            Telefono = dr.GetString(4),
                            Sucursal = dr.GetString(5),
                            Rol = dr.GetString(6)
                        };

                        // Agregar a la lista en memoria
                        empleados.Add(emp);
                    }
                }
            }

            empleadosFiltrados = new List<Empleado>(empleados);
            ActualizarDataGridView();
        }

        private void ConfigurarGrid()
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();

            // ID
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colID",
                HeaderText = "ID",
                DataPropertyName = "ID",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10, FontStyle.Regular)
                }
            });

            // Nombre completo
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNombre",
                HeaderText = "NOMBRE COMPLETO",
                DataPropertyName = "Nombre",
                Width = 150
            });

            // Correo
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colCorreo",
                HeaderText = "CORREO",
                DataPropertyName = "Correo",
                Width = 150
            });

            // Dirección
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDireccion",
                HeaderText = "DIRECCIÓN",
                DataPropertyName = "Direccion",
                Width = 180
            });

            // Teléfono
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTelefono",
                HeaderText = "TELÉFONO",
                DataPropertyName = "Telefono",
                Width = 120
            });

            // Sucursal
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSucursal",
                HeaderText = "SUCURSAL",
                DataPropertyName = "Sucursal",
                Width = 100
            });

            // Rol
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colRol",
                HeaderText = "ROL",
                DataPropertyName = "Rol",
                Width = 100
            });

            // Botón Modificar (icono lápiz)
            var btnModificar = new DataGridViewButtonColumn
            {
                Name = "Modificar",
                HeaderText = "Modificar",
                Text = "✏️",
                UseColumnTextForButtonValue = true,
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                }
            };
            dgvUsuarios.Columns.Add(btnModificar);

            // Botón Eliminar (icono zafacón)
            var btnEliminar = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "Eliminar",
                Text = "🗑️",
                UseColumnTextForButtonValue = true,
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold)
                }
            };
            dgvUsuarios.Columns.Add(btnEliminar);
        }

        private void ActualizarDataGridView()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = empleadosFiltrados;
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            Empleado empleado = (Empleado)dgvUsuarios.Rows[e.RowIndex].DataBoundItem;
            string columna = dgvUsuarios.Columns[e.ColumnIndex].Name;

            if (columna == "Modificar")
                ModificarEmpleado(empleado);
            else if (columna == "Eliminar")
                EliminarEmpleado(empleado);
        }

        private void ModificarEmpleado(Empleado empleado)
        {
            // Usar el constructor que recibe el empleado directamente
            using (var formModificar = new FarmaControlPlus.Forms.NuevoEmpleado(empleado))
            {
                if (formModificar.ShowDialog() == DialogResult.OK)
                {
                    var empleadoModificado = formModificar.EmpleadoCreado;

                    // Actualizar en la lista principal
                    var empleadoOriginal = empleados.FirstOrDefault(e => e.ID == empleado.ID);
                    if (empleadoOriginal != null)
                    {
                        ActualizarEmpleado(empleadoOriginal, empleadoModificado);
                        ActualizarEmpleadoBD(empleadoModificado);
                    }

                    // Actualizar en la lista filtrada
                    var empleadoFiltrado = empleadosFiltrados.FirstOrDefault(e => e.ID == empleado.ID);
                    if (empleadoFiltrado != null)
                    {
                        ActualizarEmpleado(empleadoFiltrado, empleadoModificado);
                    }

                    ActualizarDataGridView();
                    MessageBox.Show("Empleado modificado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ActualizarEmpleado(Empleado destino, Empleado origen)
        {
            destino.Nombre = origen.Nombre;
            destino.Correo = origen.Correo;
            destino.Direccion = origen.Direccion;
            destino.Telefono = origen.Telefono;
            destino.Sucursal = origen.Sucursal;
            destino.Rol = origen.Rol;
            destino.Ciudad = origen.Ciudad;

            if (!string.IsNullOrEmpty(origen.Contrasena) && origen.Contrasena != "••••••••")
            {
                destino.Contrasena = origen.Contrasena;
            }
        }

        private void ActualizarEmpleadoBD(Empleado empleado)
        {
            using (var conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                string sql = @"UPDATE empleados 
                       SET nombre_completo = @nombre,
                           correo = @correo,
                           direccion = @direccion,
                           telefono = @telefono,
                           sucursal = @sucursal,
                           rol = @rol
                       WHERE id = @id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", empleado.ID);
                    cmd.Parameters.AddWithValue("@nombre", empleado.Nombre);
                    cmd.Parameters.AddWithValue("@correo", empleado.Correo);
                    cmd.Parameters.AddWithValue("@direccion", empleado.Direccion);
                    cmd.Parameters.AddWithValue("@telefono", empleado.Telefono);
                    cmd.Parameters.AddWithValue("@sucursal", empleado.Sucursal);
                    cmd.Parameters.AddWithValue("@rol", empleado.Rol);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void EliminarEmpleado(Empleado empleado)
        {
            if (empleado == null)
                return;

            DialogResult r = MessageBox.Show(
                $"¿Deseas eliminar al empleado?\n\n" +
                $"ID: {empleado.ID}\n" +
                $"Nombre: {empleado.Nombre}",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                // 1️⃣ Eliminar de la BD
                using (var conn = ConexionBD.ObtenerConexion())
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("DELETE FROM empleados WHERE id = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", empleado.ID);
                        cmd.ExecuteNonQuery();
                    }
                }

                // 2️⃣ Eliminar de la lista en memoria
                empleados.Remove(empleado);
                empleadosFiltrados?.Remove(empleado);

                // 3️⃣ Actualizar el DataGridView
                ActualizarDataGridView();

                MessageBox.Show("Empleado eliminado correctamente.", "Eliminado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            using (var formNuevo = new FarmaControlPlus.Forms.NuevoEmpleado())
            {
                if (formNuevo.ShowDialog() == DialogResult.OK)
                {
                    GuardarEmpleadoBD(formNuevo.EmpleadoCreado);
                    CargarEmpleados();

                    MessageBox.Show(
                        "Empleado agregado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            RealizarBusqueda();
        }

        private void RealizarBusqueda()
        {
            string textoBusqueda = txtBusqueda.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                empleadosFiltrados = new List<Empleado>(empleados);
            }
            else
            {
                empleadosFiltrados = empleados.Where(emp =>
                    (emp.Nombre ?? "").ToLower().Contains(textoBusqueda) ||
                    (emp.Correo ?? "").ToLower().Contains(textoBusqueda) ||
                    (emp.Direccion ?? "").ToLower().Contains(textoBusqueda) ||
                    (emp.Telefono ?? "").Contains(textoBusqueda) ||
                    (emp.Sucursal ?? "").ToLower().Contains(textoBusqueda) ||
                    (emp.Rol ?? "").ToLower().Contains(textoBusqueda) ||
                    emp.ID.ToString().Contains(textoBusqueda)
                ).ToList();
            }

            ActualizarDataGridView();

            if (empleadosFiltrados.Count == 0)
            {
                MessageBox.Show("No se encontraron empleados que coincidan con la búsqueda.",
                    "Búsqueda sin resultados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = string.Empty;
            empleadosFiltrados = new List<Empleado>(empleados);
            ActualizarDataGridView();
            txtBusqueda.Focus();
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                RealizarBusqueda();
                e.Handled = true;
            }
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
        }

        private void GuardarEmpleadoBD(Empleado emp)
        {
            using (var conn = ConexionBD.ObtenerConexion())
            {
                conn.Open();

                string sql = @"
            INSERT INTO empleados
            (nombre_completo, correo, direccion, telefono, sucursal, rol)
            VALUES
            (@nombre, @correo, @direccion, @telefono, @sucursal, @rol)";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", emp.Nombre);
                    cmd.Parameters.AddWithValue("@correo", emp.Correo);
                    cmd.Parameters.AddWithValue("@direccion", emp.Direccion);
                    cmd.Parameters.AddWithValue("@telefono", emp.Telefono);
                    cmd.Parameters.AddWithValue("@sucursal", emp.Sucursal);
                    cmd.Parameters.AddWithValue("@rol", emp.Rol); // <- aquí agregas el rol

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
            // No hacer nada
        }
    }

    public class Empleado
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string Sucursal { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
    }
}