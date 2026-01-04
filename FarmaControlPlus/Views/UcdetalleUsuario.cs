using FarmaControlPlus;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TuProyecto.Views
{
    public partial class UcdetalleUsuario : UserControl
    {
        // Propiedades para almacenar los datos
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Sucursal { get; set; }
        public string Rol { get; set; }

        // Evento para cuando se quiera modificar la contraseña
        public event EventHandler ModificarContraseñaClicked;

        public UcdetalleUsuario()
        {
            InitializeComponent();
            this.Size = new Size(980, 640);
            ConfigurarEstilos();
            AgregarTitulo();
        }

        private void AgregarTitulo()
        {
            // Crear y configurar el título
            Label lblTitulo = new Label
            {
                Text = "INFORMACIÓN DEL USUARIO",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 10),
                Size = new Size(500, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };

            this.Controls.Add(lblTitulo);
            lblTitulo.BringToFront();
        }

        private void ConfigurarEstilos()
        {
            // Establecer estilos para los controles

            // Configurar todos los paneles
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel)
                {
                    panel.BackColor = Color.FromArgb(250, 250, 250);
                    panel.BorderStyle = BorderStyle.FixedSingle;
                }
            }

            // Configurar labels de campos
            lblNombre.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblNombre.ForeColor = Color.FromArgb(52, 73, 94);

            lblCorreo.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblCorreo.ForeColor = Color.FromArgb(52, 73, 94);

            lblDireccion.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblDireccion.ForeColor = Color.FromArgb(52, 73, 94);

            lblTelefono.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTelefono.ForeColor = Color.FromArgb(52, 73, 94);

            lblSucursal.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblSucursal.ForeColor = Color.FromArgb(52, 73, 94);

            lblRol.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblRol.ForeColor = Color.FromArgb(52, 73, 94);

            lblContraseña.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblContraseña.ForeColor = Color.FromArgb(52, 73, 94);

            // Configurar textboxes
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel)
                {
                    foreach (Control child in panel.Controls)
                    {
                        if (child is TextBox textBox)
                        {
                            textBox.Font = new Font("Segoe UI", 10);
                            textBox.ForeColor = Color.FromArgb(70, 70, 70);
                            textBox.BackColor = Color.FromArgb(250, 250, 250);
                            textBox.BorderStyle = BorderStyle.None;
                            textBox.ReadOnly = true;
                        }
                    }
                }
            }

            // Estilo del botón (aunque por ahora lo ignoramos)
            btnModificarContraseña.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnModificarContraseña.BackColor = Color.FromArgb(46, 204, 113);
            btnModificarContraseña.ForeColor = Color.White;
            btnModificarContraseña.FlatStyle = FlatStyle.Flat;
            btnModificarContraseña.FlatAppearance.BorderSize = 0;
            btnModificarContraseña.Cursor = Cursors.Hand;

            // Ocultar sección de contraseña por ahora
            panelContraseña.Visible = false;
        }

        // Método para cargar los datos del usuario
        public void CargarDatosUsuario(Empleado empleado)
        {
            if (empleado == null)
            {
                MessageBox.Show("No se recibieron datos del usuario", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Actualizar propiedades
            Nombre = empleado.NombreCompleto;
            Correo = empleado.Correo;
            Direccion = empleado.Direccion;
            Telefono = empleado.Telefono;
            Sucursal = empleado.Sucursal;
            Rol = empleado.Rol;

            // Actualizar controles con los datos reales
            txtNombre.Text = !string.IsNullOrEmpty(Nombre) ? Nombre : "No disponible";
            txtCorreo.Text = !string.IsNullOrEmpty(Correo) ? Correo : "No disponible";
            txtDireccion.Text = !string.IsNullOrEmpty(Direccion) ? Direccion : "No disponible";
            txtTelefono.Text = !string.IsNullOrEmpty(Telefono) ? Telefono : "No disponible";
            txtSucursal.Text = !string.IsNullOrEmpty(Sucursal) ? Sucursal : "No disponible";
            txtRol.Text = !string.IsNullOrEmpty(Rol) ? Rol : "No disponible";
        }

        // Método sobrecargado para compatibilidad (mantener el anterior)
        public void CargarDatosUsuario(string nombre, string correo, string direccion,
                                       string telefono, string sucursal, string rol)
        {
            Nombre = nombre;
            Correo = correo;
            Direccion = direccion;
            Telefono = telefono;
            Sucursal = sucursal;
            Rol = rol;

            // Actualizar controles
            txtNombre.Text = !string.IsNullOrEmpty(Nombre) ? Nombre : "No disponible";
            txtCorreo.Text = !string.IsNullOrEmpty(Correo) ? Correo : "No disponible";
            txtDireccion.Text = !string.IsNullOrEmpty(Direccion) ? Direccion : "No disponible";
            txtTelefono.Text = !string.IsNullOrEmpty(Telefono) ? Telefono : "No disponible";
            txtSucursal.Text = !string.IsNullOrEmpty(Sucursal) ? Sucursal : "No disponible";
            txtRol.Text = !string.IsNullOrEmpty(Rol) ? Rol : "No disponible";
        }

        public class FormModificarContraseña : Form
        {
            private TextBox txtNueva;
            private TextBox txtConfirmar;
            private Label lblTituloForm;
            private Button btnAceptar;
            private Button btnCancelar;

            public string NuevaContraseña { get; private set; }

            public FormModificarContraseña()
            {
                this.Size = new Size(400, 250);
                this.BackColor = Color.White;
                this.Text = "Modificar Contraseña";
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                // Título
                lblTituloForm = new Label
                {
                    Text = "MODIFICAR CONTRASEÑA",
                    Font = new Font("Segoe UI", 14, FontStyle.Bold),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Location = new Point(20, 20),
                    Size = new Size(360, 30),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                // Campo nueva contraseña
                var lblNueva = new Label
                {
                    Text = "Nueva Contraseña:",
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Location = new Point(20, 70),
                    Size = new Size(150, 25)
                };

                txtNueva = new TextBox
                {
                    Font = new Font("Segoe UI", 10),
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(180, 70),
                    Size = new Size(180, 25),
                    PasswordChar = '•'
                };

                // Campo confirmar contraseña
                var lblConfirmar = new Label
                {
                    Text = "Confirmar Contraseña:",
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.FromArgb(52, 73, 94),
                    Location = new Point(20, 110),
                    Size = new Size(150, 25)
                };

                txtConfirmar = new TextBox
                {
                    Font = new Font("Segoe UI", 10),
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(180, 110),
                    Size = new Size(180, 25),
                    PasswordChar = '•'
                };

                // Botón Aceptar
                btnAceptar = new Button
                {
                    Text = "ACEPTAR",
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Size = new Size(120, 35),
                    Location = new Point(60, 160),
                    DialogResult = DialogResult.OK
                };
                btnAceptar.Click += (sender, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txtNueva.Text))
                    {
                        MessageBox.Show("La contraseña no puede estar vacía.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.None;
                        return;
                    }

                    if (txtNueva.Text != txtConfirmar.Text)
                    {
                        MessageBox.Show("Las contraseñas no coinciden.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.None;
                        return;
                    }

                    NuevaContraseña = txtNueva.Text;
                };

                // Botón Cancelar
                btnCancelar = new Button
                {
                    Text = "CANCELAR",
                    BackColor = Color.FromArgb(149, 165, 166),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Size = new Size(120, 35),
                    Location = new Point(200, 160),
                    DialogResult = DialogResult.Cancel
                };

                // Agregar controles al formulario
                this.Controls.Add(lblTituloForm);
                this.Controls.Add(lblNueva);
                this.Controls.Add(txtNueva);
                this.Controls.Add(lblConfirmar);
                this.Controls.Add(txtConfirmar);
                this.Controls.Add(btnAceptar);
                this.Controls.Add(btnCancelar);
            }
        }

        // Evento del botón modificar contraseña
        private void btnModificarContraseña_Click(object sender, EventArgs e)
        {
            // Por ahora no hacer nada
            // ModificarContraseñaClicked?.Invoke(this, EventArgs.Empty);

            MessageBox.Show("La funcionalidad de cambio de contraseña estará disponible próximamente.",
                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Método para mostrar el formulario de modificación de contraseña
        public void MostrarFormularioModificarContraseña()
        {
            using (var formModificarPass = new FormModificarContraseña())
            {
                if (formModificarPass.ShowDialog() == DialogResult.OK)
                {
                    string nuevaContraseña = formModificarPass.NuevaContraseña;
                    // Aquí deberías implementar la lógica para actualizar la contraseña en la BD
                    MessageBox.Show("Contraseña modificada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void UcdetalleUsuario_Load(object sender, EventArgs e)
        {

        }
    }
}