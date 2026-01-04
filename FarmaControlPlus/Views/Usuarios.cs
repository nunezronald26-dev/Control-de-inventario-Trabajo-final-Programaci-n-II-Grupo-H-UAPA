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
                            NombreCompleto = dr.GetString(1),
                            Correo = dr.GetString(2),
                            Direccion = dr.GetString(3),
                            Telefono = dr.GetString(4),
                            Sucursal = dr.GetString(5),
                            Rol = dr.GetString(6)
                        };

                        // Agregar a la lista en memoria
                        empleados.Add(emp);

                        // Agregar al DataGridView SOLO los datos
                        dgvUsuarios.Rows.Add(
                            emp.ID,
                            emp.NombreCompleto,
                            emp.Correo,
                            emp.Direccion,
                            emp.Telefono,
                            emp.Sucursal,
                            emp.Rol
                        );
                    }
                }
            }
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
                HeaderText = "NOMBRE COMPL",
                DataPropertyName = "NombreCompleto",
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
                Text = "\uE70F", // icono de lápiz en Segoe MDL2 Assets
                UseColumnTextForButtonValue = true,
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe MDL2 Assets", 16),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.Blue
                }
            };
            dgvUsuarios.Columns.Add(btnModificar);

            // Botón Eliminar (icono zafacón)
            var btnEliminar = new DataGridViewButtonColumn
            {
                Name = "Eliminar",
                HeaderText = "Eliminar",
                Text = "\uE74D", // icono zafacón en Segoe MDL2 Assets
                UseColumnTextForButtonValue = true,
                Width = 50,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe MDL2 Assets", 16),
                    Alignment = DataGridViewContentAlignment.MiddleCenter,
                    ForeColor = Color.Red
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
            MessageBox.Show(
                $"Modificar empleado:\n\n" +
                $"ID: {empleado.ID}\n" +
                $"Nombre: {empleado.NombreCompleto}\n" +
                $"Correo: {empleado.Correo}\n" +
                $"Dirección: {empleado.Direccion}\n" +
                $"Dirección: {empleado.Direccion}\n" +
                $"Sucursal: {empleado.Sucursal}",
                "Modificar Empleado",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void EliminarEmpleado(Empleado empleado)
        {
            if (empleado == null)
                return;

            DialogResult r = MessageBox.Show(
                $"¿Deseas eliminar al empleado?\n\n" +
                $"ID: {empleado.ID}\n" +
                $"Nombre: {empleado.NombreCompleto}\nCorreo: {empleado.Correo}",
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
                    emp.NombreCompleto.ToLower().Contains(textoBusqueda) ||
                    emp.Correo.ToLower().Contains(textoBusqueda) ||
                    emp.Direccion.ToLower().Contains(textoBusqueda) ||
                    emp.Direccion.ToLower().Contains(textoBusqueda) ||
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
                    cmd.Parameters.AddWithValue("@nombre", emp.NombreCompleto);
                    cmd.Parameters.AddWithValue("@correo", emp.Correo);
                    cmd.Parameters.AddWithValue("@direccion", emp.Direccion);
                    cmd.Parameters.AddWithValue("@telefono", emp.Telefono);
                    cmd.Parameters.AddWithValue("@sucursal", emp.Sucursal);
                    cmd.Parameters.AddWithValue("@rol", emp.Rol); // <- aquí agregas el rol

                    cmd.ExecuteNonQuery();
                }
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

}