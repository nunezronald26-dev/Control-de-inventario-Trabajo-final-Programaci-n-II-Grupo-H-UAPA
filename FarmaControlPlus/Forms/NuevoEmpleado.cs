using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TuProyecto.Views;

namespace FarmaControlPlus.Forms
{
    public partial class NuevoEmpleado : Form
    {
        // Propiedades para modo edición
        public bool ModoEdicion { get; set; } = false;
        public Empleado EmpleadoAEditar { get; set; }
        public Empleado EmpleadoCreado { get; private set; }

        // Variables para verificar si se cambiaron las contraseñas
        private bool contrasenaCambiada = false;
        private bool repetirContrasenaCambiada = false;

        public NuevoEmpleado()
        {
            InitializeComponent();
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;
        }

        private void NuevoEmpleado_Load(object sender, EventArgs e)
        {
            // Si está en modo edición, cargar datos del empleado
            if (ModoEdicion && EmpleadoAEditar != null)
            {
                txtNombre.Text = EmpleadoAEditar.Nombre;
                txtCorreo.Text = EmpleadoAEditar.Correo;
                txtDir.Text = EmpleadoAEditar.Direccion;
                txtTelefono.Text = EmpleadoAEditar.Telefono;
                textSucursal.Text = EmpleadoAEditar.Sucursal;

                // Mostrar placeholder para contraseña
                txtPass.PasswordChar = '•';
                txtPass.Text = "••••••••";
                txtPass.ForeColor = SystemColors.GrayText;

                // Mostrar placeholder para repetir contraseña
                txtRepeatPass.PasswordChar = '•';
                txtRepeatPass.Text = "••••••••";
                txtRepeatPass.ForeColor = SystemColors.GrayText;

                // Configurar eventos para detectar cambios en contraseña
                txtPass.Enter += TxtContrasena_Enter;
                txtPass.Leave += TxtContrasena_Leave;
                txtPass.TextChanged += TxtContrasena_TextChanged;

                txtRepeatPass.Enter += TxtRepeatContrasena_Enter;
                txtRepeatPass.Leave += TxtRepeatContrasena_Leave;
                txtRepeatPass.TextChanged += TxtRepeatContrasena_TextChanged;
            }
            else
            {
                // En modo creación, solo mostrar PasswordChar normal
                txtPass.PasswordChar = '•';
                txtRepeatPass.PasswordChar = '•';
            }

            txtNombre.Focus();
        }

        private void TxtContrasena_Enter(object sender, EventArgs e)
        {
            if (ModoEdicion && txtPass.Text == "••••••••")
            {
                txtPass.Text = "";
                txtPass.PasswordChar = '•';
                txtPass.ForeColor = SystemColors.ControlText;
            }
        }

        private void TxtContrasena_Leave(object sender, EventArgs e)
        {
            if (ModoEdicion && string.IsNullOrEmpty(txtPass.Text))
            {
                txtPass.Text = "••••••••";
                txtPass.PasswordChar = '•';
                txtPass.ForeColor = SystemColors.GrayText;
                contrasenaCambiada = false;
            }
        }

        private void TxtContrasena_TextChanged(object sender, EventArgs e)
        {
            if (ModoEdicion && txtPass.Text != "••••••••")
            {
                contrasenaCambiada = true;
            }
        }

        private void TxtRepeatContrasena_Enter(object sender, EventArgs e)
        {
            if (ModoEdicion && txtRepeatPass.Text == "••••••••")
            {
                txtRepeatPass.Text = "";
                txtRepeatPass.PasswordChar = '•';
                txtRepeatPass.ForeColor = SystemColors.ControlText;
            }
        }

        private void TxtRepeatContrasena_Leave(object sender, EventArgs e)
        {
            if (ModoEdicion && string.IsNullOrEmpty(txtRepeatPass.Text))
            {
                txtRepeatPass.Text = "••••••••";
                txtRepeatPass.PasswordChar = '•';
                txtRepeatPass.ForeColor = SystemColors.GrayText;
                repetirContrasenaCambiada = false;
            }
        }

        private void TxtRepeatContrasena_TextChanged(object sender, EventArgs e)
        {
            if (ModoEdicion && txtRepeatPass.Text != "••••••••")
            {
                repetirContrasenaCambiada = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación básica
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre completo es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // Validar contraseñas en modo creación
            if (!ModoEdicion)
            {
                if (string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    MessageBox.Show("La contraseña es obligatoria para nuevos empleados.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtPass.Focus();
                    return;
                }

                if (txtPass.Text != txtRepeatPass.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtPass.Focus();
                    return;
                }
            }

            // Validar contraseñas en modo edición si se cambiaron
            if (ModoEdicion && (contrasenaCambiada || repetirContrasenaCambiada))
            {
                if (txtPass.Text != txtRepeatPass.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden.",
                        "Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtPass.Focus();
                    return;
                }

                // Si se cambió la contraseña, validar con administrador
                if (contrasenaCambiada)
                {
                    // Mostrar formulario para validar contraseña de administrador
                    using (var formAdmin = new ValidarAdministradorForm())
                    {
                        if (formAdmin.ShowDialog() != DialogResult.OK)
                        {
                            MessageBox.Show("La contraseña de administrador es incorrecta. No se puede modificar la contraseña.",
                                "Validación fallida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }

            // Mapeo de datos al objeto Empleado
            EmpleadoCreado = new Empleado
            {
                Nombre = txtNombre.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Direccion = txtDir.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Sucursal = textSucursal.Text.Trim(),
                // Si es edición y no se cambió la contraseña, mantener la original
                Contrasena = ModoEdicion && !contrasenaCambiada ?
                    EmpleadoAEditar.Contrasena : txtPass.Text
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Método para manejar la tecla Enter entre campos
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                Control control = this.ActiveControl;

                // Si es un TextBox, mover al siguiente
                if (control is TextBox)
                {
                    // Verificar que no sea el último campo
                    if (control != textSucursal && control != txtRepeatPass)
                    {
                        this.SelectNextControl(control, true, true, true, true);
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            // Puedes agregar validación en tiempo real aquí si es necesario
        }

        private void lblCiudad_Click(object sender, EventArgs e)
        {
            // Evento del label de dirección (antes era lblCiudad)
        }
    }

    // Formulario para validar contraseña de administrador
    public class ValidarAdministradorForm : Form
    {
        private TextBox txtContrasenaAdmin;
        private Button btnAceptar;
        private Button btnCancelar;
        private Label lblInstruccion;

        public ValidarAdministradorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Validar Administrador";
            this.Size = new Size(350, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            lblInstruccion = new Label
            {
                Text = "Para modificar la contraseña, ingrese la contraseña de administrador:",
                Location = new Point(20, 20),
                Size = new Size(300, 40),
                Font = new Font("Segoe UI", 9)
            };

            txtContrasenaAdmin = new TextBox
            {
                Location = new Point(20, 70),
                Size = new Size(300, 25),
                PasswordChar = '•',
                Font = new Font("Segoe UI", 10)
            };

            btnAceptar = new Button
            {
                Text = "Aceptar",
                Location = new Point(145, 110),
                Size = new Size(85, 30),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnAceptar.Click += (s, e) => ValidarContrasena();

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(235, 110),
                Size = new Size(85, 30),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancelar.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            };

            this.Controls.Add(lblInstruccion);
            this.Controls.Add(txtContrasenaAdmin);
            this.Controls.Add(btnAceptar);
            this.Controls.Add(btnCancelar);

            this.AcceptButton = btnAceptar;
            this.CancelButton = btnCancelar;
        }

        private void ValidarContrasena()
        {
            // Aquí debes implementar la validación real contra tu sistema
            // Por ahora, usaré una contraseña hardcodeada como ejemplo
            string contrasenaAdminValida = "admin123"; // Cambia esto por tu lógica real

            if (txtContrasenaAdmin.Text == contrasenaAdminValida)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta. Intente nuevamente.",
                    "Error de validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtContrasenaAdmin.Focus();
                txtContrasenaAdmin.SelectAll();
            }
        }
    }
}