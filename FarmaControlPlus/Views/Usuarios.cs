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
            // Configurar estilos del DataGridView
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = colorFondoBusqueda;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = colorTexto;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            dgvUsuarios.DefaultCellStyle.ForeColor = colorTexto;
            dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 10);

            dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionBackColor = colorFondoBusqueda;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionForeColor = colorTexto;

            //dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void ConfigurarGrid()
        {
            dgvUsuarios.AutoGenerateColumns = false;
            dgvUsuarios.Columns.Clear();

            // ID
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                DataPropertyName = "ID",
                Width = 60,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10, FontStyle.Regular)
                }
            });

            // Nombre
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "NOMBRE",
                DataPropertyName = "Nombre",
                Width = 120
            });

            // Apellidos
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "APELLIDOS",
                DataPropertyName = "Apellidos",
                Width = 150
            });

            // Dirección
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "DIRECCIÓN",
                DataPropertyName = "Direccion",
                Width = 180
            });

            // Ciudad
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "CIUDAD",
                DataPropertyName = "Ciudad",
                Width = 100
            });

            // Teléfono
            dgvUsuarios.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "TELÉFONO",
                DataPropertyName = "Telefono",
                Width = 120
            });

            // Botón Modificar - SIN COLOR DE FONDO
            var btnModificar = new DataGridViewImageColumn
            {
                Name = "Modificar",
                HeaderText = "Modificar",
                Image = global::FarmaControlPlus.Properties.Resources._333_usuarios, // Asegúrate de que este recurso existe
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(255,109,0),
                }
            };

            dgvUsuarios.Columns.Add(btnModificar);

            // Botón Eliminar - SIN COLOR DE FONDO
            var btnEliminar = new DataGridViewImageColumn
            {
                Name = "Eliminar",
                HeaderText = "Eliminar",
                Image = global::FarmaControlPlus.Properties.Resources._333_reportes,
                Width = 70,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    BackColor = Color.FromArgb(255, 0, 0),

                    Padding = new Padding(10),
                }
            };


            dgvUsuarios.Columns.Add(btnEliminar);
        }

        private void CargarEmpleados()
        {
            empleados = new List<Empleado>
            {
                new Empleado { ID = 1, Nombre = "Manuel", Apellidos = "Ramon Menarguez", Direccion = "Ramon Menarguez 56", Ciudad = "Murcia", Telefono = "2344444333" },
                new Empleado { ID = 2, Nombre = "Francisco", Apellidos = "Juper", Direccion = "Nacida 23", Ciudad = "Bilbao", Telefono = "34567845678" },
                new Empleado { ID = 3, Nombre = "Marta", Apellidos = "Cases", Direccion = "Lopez García 23", Ciudad = "Madrid", Telefono = "737763632" },
                new Empleado { ID = 4, Nombre = "Miguel Angel", Apellidos = "Jumilla", Direccion = "Gemesía Sol 23", Ciudad = "Alicante", Telefono = "34567834567" },
                new Empleado { ID = 5, Nombre = "Ana", Apellidos = "García López", Direccion = "Principal 45", Ciudad = "Barcelona", Telefono = "934567890" },
                new Empleado { ID = 6, Nombre = "Carlos", Apellidos = "Martínez Ruiz", Direccion = "Secundaria 78", Ciudad = "Valencia", Telefono = "963456789" },
                new Empleado { ID = 7, Nombre = "Laura", Apellidos = "Fernández Gómez", Direccion = "Central 12", Ciudad = "Sevilla", Telefono = "954321098" },
                new Empleado { ID = 8, Nombre = "David", Apellidos = "Sánchez Pérez", Direccion = "Norte 34", Ciudad = "Zaragoza", Telefono = "976543210" }
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
            MessageBox.Show(
                $"Modificar empleado:\n\n" +
                $"ID: {empleado.ID}\n" +
                $"Nombre: {empleado.Nombre}\n" +
                $"Apellidos: {empleado.Apellidos}\n" +
                $"Dirección: {empleado.Direccion}\n" +
                $"Ciudad: {empleado.Ciudad}\n" +
                $"Teléfono: {empleado.Telefono}",
                "Modificar Empleado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void EliminarEmpleado(Empleado empleado)
        {
            DialogResult r = MessageBox.Show(
                $"¿Deseas eliminar al empleado?\n\n" +
                $"ID: {empleado.ID}\n" +
                $"Nombre: {empleado.Nombre} {empleado.Apellidos}",
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

                    // Generar ID incremental
                    nuevoEmpleado.ID = empleados.Count > 0 ? empleados.Max(o => o.ID) + 1 : 1;

                    // Actualizar listas y Grid
                    empleados.Add(nuevoEmpleado);
                    empleadosFiltrados.Add(nuevoEmpleado);
                    ActualizarDataGridView();

                    MessageBox.Show($"Empleado {nuevoEmpleado.Nombre} agregado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    emp.Nombre.ToLower().Contains(textoBusqueda) ||
                    emp.Apellidos.ToLower().Contains(textoBusqueda) ||
                    emp.Direccion.ToLower().Contains(textoBusqueda) ||
                    emp.Ciudad.ToLower().Contains(textoBusqueda) ||
                    emp.Telefono.Contains(textoBusqueda) ||
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

        // Formulario para nuevo empleado
        //private class FormNuevoEmpleado : Form
        //{
        //    public Empleado EmpleadoCreado { get; private set; }
        //    private TextBox txtNombre, txtApellidos, txtDireccion, txtCiudad, txtTelefono;
        //    private Button btnGuardar, btnCancelar;

        //    public FormNuevoEmpleado()
        //    {
        //        InitializeComponent();
        //        this.StartPosition = FormStartPosition.CenterParent;
        //        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        //        this.MaximizeBox = false;
        //        this.MinimizeBox = false;
        //    }

        //    private void InitializeComponent()
        //    {
        //        this.Text = "Nuevo Empleado";
        //        this.Size = new Size(400, 350);
        //        this.BackColor = Color.White;
        //        this.Padding = new Padding(20);

        //        var panelContenido = new Panel
        //        {
        //            Dock = DockStyle.Fill,
        //            BackColor = Color.White
        //        };

        //        // Etiquetas y campos
        //        var lblTitulo = new Label
        //        {
        //            Text = "Nuevo Empleado",
        //            Font = new Font("Segoe UI", 14, FontStyle.Bold),
        //            ForeColor = Color.FromArgb(52, 73, 94),
        //            Location = new Point(0, 0),
        //            Size = new Size(360, 30),
        //            TextAlign = ContentAlignment.MiddleCenter
        //        };

        //        // Campos de entrada
        //        int yPos = 40;
        //        txtNombre = CrearCampo("Nombre:", yPos);
        //        yPos += 40;
        //        txtApellidos = CrearCampo("Apellidos:", yPos);
        //        yPos += 40;
        //        txtDireccion = CrearCampo("Dirección:", yPos);
        //        yPos += 40;
        //        txtCiudad = CrearCampo("Ciudad:", yPos);
        //        yPos += 40;
        //        txtTelefono = CrearCampo("Teléfono:", yPos);

        //        // Botones
        //        btnGuardar = new Button
        //        {
        //            Text = "Guardar",
        //            BackColor = Color.FromArgb(46, 204, 113),
        //            ForeColor = Color.White,
        //            FlatStyle = FlatStyle.Flat,
        //            Font = new Font("Segoe UI", 10, FontStyle.Bold),
        //            Size = new Size(120, 35),
        //            Location = new Point(60, 260)
        //        };
        //        btnGuardar.Click += BtnGuardar_Click;

        //        btnCancelar = new Button
        //        {
        //            Text = "Cancelar",
        //            BackColor = Color.FromArgb(149, 165, 166),
        //            ForeColor = Color.White,
        //            FlatStyle = FlatStyle.Flat,
        //            Font = new Font("Segoe UI", 10, FontStyle.Bold),
        //            Size = new Size(120, 35),
        //            Location = new Point(200, 260)
        //        };
        //        btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

        //        panelContenido.Controls.Add(lblTitulo);
        //        panelContenido.Controls.Add(txtNombre);
        //        panelContenido.Controls.Add(txtApellidos);
        //        panelContenido.Controls.Add(txtDireccion);
        //        panelContenido.Controls.Add(txtCiudad);
        //        panelContenido.Controls.Add(txtTelefono);
        //        panelContenido.Controls.Add(btnGuardar);
        //        panelContenido.Controls.Add(btnCancelar);

        //        this.Controls.Add(panelContenido);
        //    }

        //    private TextBox CrearCampo(string etiqueta, int posY)
        //    {
        //        var lbl = new Label
        //        {
        //            Text = etiqueta,
        //            Font = new Font("Segoe UI", 10, FontStyle.Regular),
        //            ForeColor = Color.FromArgb(52, 73, 94),
        //            Location = new Point(0, posY),
        //            Size = new Size(100, 25)
        //        };

        //        var txt = new TextBox
        //        {
        //            Font = new Font("Segoe UI", 10),
        //            BorderStyle = BorderStyle.FixedSingle,
        //            Location = new Point(110, posY),
        //            Size = new Size(250, 25)
        //        };

        //        var panel = new Panel
        //        {
        //            Location = new Point(0, posY),
        //            Size = new Size(360, 30)
        //        };
        //        panel.Controls.Add(lbl);
        //        panel.Controls.Add(txt);

        //        this.Controls.Add(panel);
        //        return txt;
        //    }

        //    private void BtnGuardar_Click(object sender, EventArgs e)
        //    {
        //        if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
        //            string.IsNullOrWhiteSpace(txtApellidos.Text))
        //        {
        //            MessageBox.Show("Nombre y Apellidos son obligatorios.",
        //                "Datos incompletos",
        //                MessageBoxButtons.OK,
        //                MessageBoxIcon.Warning);
        //            return;
        //        }

        //        EmpleadoCreado = new Empleado
        //        {
        //            Nombre = txtNombre.Text.Trim(),
        //            Apellidos = txtApellidos.Text.Trim(),
        //            Direccion = txtDireccion.Text.Trim(),
        //            Ciudad = txtCiudad.Text.Trim(),
        //            Telefono = txtTelefono.Text.Trim()
        //        };

        //        this.DialogResult = DialogResult.OK;
        //        this.Close();
        //    }
        //}
    }

    public class Empleado
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
    }
}