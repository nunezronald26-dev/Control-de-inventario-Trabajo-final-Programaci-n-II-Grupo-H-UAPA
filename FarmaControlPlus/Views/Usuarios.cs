using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TuProyecto.Views
{
    public partial class Usuarios : UserControl
    {
        private List<Empleado> empleados;
        private List<Empleado> empleadosFiltrados;

        public Usuarios()
        {
            InitializeComponent();
            ConfigurarEstilos();
            ConfigurarGrid();
            CargarEmpleados();
        }

        private void ConfigurarEstilos()
        {
            panelBusqueda.BackColor = Color.FromArgb(248, 249, 250);

            btnNuevoUsuario.BackColor = Color.FromArgb(46, 204, 113); // Verde
            btnNuevoUsuario.FlatStyle = FlatStyle.Flat;
            btnNuevoUsuario.FlatAppearance.BorderSize = 0;
            btnNuevoUsuario.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnNuevoUsuario.ForeColor = Color.White;

            btnBuscar.BackColor = Color.FromArgb(70, 130, 180);
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;

            btnLimpiar.BackColor = Color.FromArgb(108, 117, 125);
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderSize = 0;
            btnLimpiar.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnLimpiar.ForeColor = Color.White;

            // DataGridView estilo
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.RowTemplate.Height = 35;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(70, 130, 180);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsuarios.DefaultCellStyle.Padding = new Padding(6, 4, 6, 4);
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 255);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void ConfigurarGrid()
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();

            // Columnas
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "ID",
                Width = 10,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9)
                }
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "NOMBRE COMPLETO",
                DataPropertyName = "Nombre",
                Width = 180
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "CORREO",
                DataPropertyName = "Correo",
                Width = 150
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "DIRECCIÓN",
                DataPropertyName = "Direccion",
                Width = 170
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "TELÉFONO",
                DataPropertyName = "Telefono",
                Width = 110
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "SUCURSAL",
                DataPropertyName = "Sucursal",
                Width = 90
            });

            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ROL",
                DataPropertyName = "Rol",
                Width = 90
            });

            // Botón Modificar
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

            // Botón Eliminar
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

        private void CargarEmpleados()
        {
            empleados = new List<Empleado>
            {
                new Empleado {
                    ID = 1,
                    Nombre = "Manuel Ramon Menarguez",
                    Correo = "manuel@email.com",
                    Direccion = "Ramon Menarguez 56",
                    Ciudad = "Murcia",
                    Telefono = "2344444333",
                    Sucursal = "Central",
                    Rol = "Administrador",
                    Contrasena = "manuel123"
                },
                new Empleado {
                    ID = 2,
                    Nombre = "Francisco Juper",
                    Correo = "francisco@email.com",
                    Direccion = "Nacida 23",
                    Ciudad = "Bilbao",
                    Telefono = "34567845678",
                    Sucursal = "Norte",
                    Rol = "Vendedor",
                    Contrasena = "francisco123"
                },
                new Empleado {
                    ID = 3,
                    Nombre = "Marta Cases",
                    Correo = "marta@email.com",
                    Direccion = "Lopez García 23",
                    Ciudad = "Madrid",
                    Telefono = "737763632",
                    Sucursal = "Centro",
                    Rol = "Vendedor",
                    Contrasena = "marta123"
                }
            };

            empleadosFiltrados = new List<Empleado>(empleados);
            ActualizarDataGridView();
        }

        private void ActualizarDataGridView()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = empleadosFiltrados;
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columna = dgvUsuarios.Columns[e.ColumnIndex].Name;
            Empleado empleado = (Empleado)dgvUsuarios.Rows[e.RowIndex].DataBoundItem;

            if (columna == "Modificar")
            {
                ModificarEmpleado(empleado);
            }
            else if (columna == "Eliminar")
            {
                EliminarEmpleado(empleado);
            }
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

        private void EliminarEmpleado(Empleado empleado)
        {
            DialogResult r = MessageBox.Show(
                $"¿Deseas eliminar al empleado?\n\n" +
                $"ID: {empleado.ID}\n" +
                $"Nombre: {empleado.Nombre}",
                "Confirmar Eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                empleados.Remove(empleado);
                empleadosFiltrados.Remove(empleado);
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
                    var nuevoEmpleado = formNuevo.EmpleadoCreado;
                    nuevoEmpleado.ID = empleados.Count > 0 ? empleados.Max(o => o.ID) + 1 : 1;

                    empleados.Add(nuevoEmpleado);
                    empleadosFiltrados.Add(nuevoEmpleado);
                    ActualizarDataGridView();

                    MessageBox.Show($"Empleado {nuevoEmpleado.Nombre} agregado.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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