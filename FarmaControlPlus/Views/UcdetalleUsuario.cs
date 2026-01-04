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
        }

        private void ConfigurarEstilos()
        {
            // Establecer estilos para los controles

            // Configurar estilos de labels
            //foreach (Control control in this.Controls)
            //{
            //    if (control is Label label && label != lblTitulo)
            //    {
            //        label.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            //        label.ForeColor = Color.FromArgb(52, 73, 94);
            //    }
            //    else if (control is TextBox textBox)
            //    {
            //        textBox.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            //        textBox.ForeColor = Color.FromArgb(70, 70, 70);
            //        textBox.BackColor = Color.FromArgb(250, 250, 250);
            //        textBox.BorderStyle = BorderStyle.None;
            //        textBox.ReadOnly = true;
            //    }
            //}

            // Estilo del botón
            btnModificarContraseña.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnModificarContraseña.BackColor = Color.FromArgb(46, 204, 113);
            btnModificarContraseña.ForeColor = Color.White;
            btnModificarContraseña.FlatStyle = FlatStyle.Flat;
            btnModificarContraseña.FlatAppearance.BorderSize = 0;
            btnModificarContraseña.Cursor = Cursors.Hand;

            // Estilo de campos de contraseña
            txtContraseñaActual.BackColor = Color.White;
            txtContraseñaActual.ForeColor = Color.FromArgb(100, 100, 100);
            txtContraseñaActual.BorderStyle = BorderStyle.FixedSingle;

            // Color de fondo de los paneles
            panelNombre.BackColor = Color.FromArgb(250, 250, 250);
            panelCorreo.BackColor = Color.FromArgb(250, 250, 250);
            panelDireccion.BackColor = Color.FromArgb(250, 250, 250);
            panelTelefono.BackColor = Color.FromArgb(250, 250, 250);
            panelSucursal.BackColor = Color.FromArgb(250, 250, 250);
            panelRol.BackColor = Color.FromArgb(250, 250, 250);
            panelContraseña.BackColor = Color.FromArgb(250, 250, 250);
        }

        // Método para cargar los datos del usuario
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
            txtNombre.Text = Nombre ?? "No disponible";
            txtCorreo.Text = Correo ?? "No disponible";
            txtDireccion.Text = Direccion ?? "No disponible";
            txtTelefono.Text = Telefono ?? "No disponible";
            txtSucursal.Text = Sucursal ?? "No disponible";
            txtRol.Text = Rol ?? "No disponible";
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
            ModificarContraseñaClicked?.Invoke(this, EventArgs.Empty);
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