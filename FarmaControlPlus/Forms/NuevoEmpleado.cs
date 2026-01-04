using System;
using System.Drawing;
using System.Windows.Forms;
using TuProyecto.Views;

namespace FarmaControlPlus.Forms
{
    public partial class NuevoEmpleado : Form
    {
        // ===== Propiedades =====
        public bool ModoEdicion { get; private set; } = false;
        public Empleado EmpleadoAEditar { get; private set; }
        public Empleado EmpleadoCreado { get; private set; }

        // Control de cambios de contraseña
        private bool contrasenaCambiada = false;
        private bool repetirContrasenaCambiada = false;

        // ===== CONSTRUCTOR PARA NUEVO EMPLEADO =====
        public NuevoEmpleado()
        {
            InitializeComponent();
            ConfigurarControles();
            this.Text = "Registrar Nuevo Usuario";
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;
        }

        // ===== CONSTRUCTOR PARA EDITAR EMPLEADO =====
        public NuevoEmpleado(Empleado empleadoExistente)
        {
            InitializeComponent();
            ConfigurarControles();

            ModoEdicion = true;
            EmpleadoAEditar = empleadoExistente;
            this.Text = "Modificar Empleado";

            // CARGAR DATOS INMEDIATAMENTE EN EL CONSTRUCTOR
            CargarDatosEmpleado();

            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;
        }

        private void ConfigurarControles()
        {
            // Asegurar que los campos de contraseña estén en modo seguro
            txtPass.PasswordChar = '•';
            txtRepeatPass.PasswordChar = '•';
        }

        private void CargarDatosEmpleado()
        {
            if (EmpleadoAEditar == null) return;

            // Cargar los datos en los campos
            txtNombre.Text = EmpleadoAEditar.Nombre ?? "";
            txtCorreo.Text = EmpleadoAEditar.Correo ?? "";
            txtDir.Text = EmpleadoAEditar.Direccion ?? "";
            txtTelefono.Text = EmpleadoAEditar.Telefono ?? "";
            textSucursal.Text = EmpleadoAEditar.Sucursal ?? "";

            // Cargar el rol si existe
            if (dropRol != null && !string.IsNullOrEmpty(EmpleadoAEditar.Rol))
            {
                dropRol.Text = EmpleadoAEditar.Rol;
            }

            // Configurar placeholders para contraseñas
            txtPass.Text = "••••••••";
            txtPass.ForeColor = SystemColors.GrayText;

            txtRepeatPass.Text = "••••••••";
            txtRepeatPass.ForeColor = SystemColors.GrayText;

            // Agregar eventos para los campos de contraseña
            txtPass.Enter += TxtPass_Enter;
            txtPass.Leave += TxtPass_Leave;
            txtPass.TextChanged += TxtPass_TextChanged;

            txtRepeatPass.Enter += TxtRepeatPass_Enter;
            txtRepeatPass.Leave += TxtRepeatPass_Leave;
            txtRepeatPass.TextChanged += TxtRepeatPass_TextChanged;
        }

        private void NuevoEmpleado_Load(object sender, EventArgs e)
        {
            // Si no es modo edición, solo establecer foco
            if (!ModoEdicion || EmpleadoAEditar == null)
            {
                txtNombre.Focus();
                return;
            }

            // Si ya se cargaron los datos en el constructor, solo establecer foco
            txtNombre.Focus();
        }

        // ===== Eventos contraseña =====
        private void TxtPass_Enter(object sender, EventArgs e)
        {
            if (ModoEdicion && txtPass.Text == "••••••••")
            {
                txtPass.Clear();
                txtPass.ForeColor = SystemColors.ControlText;
            }
        }

        private void TxtPass_Leave(object sender, EventArgs e)
        {
            if (ModoEdicion && string.IsNullOrWhiteSpace(txtPass.Text))
            {
                txtPass.Text = "••••••••";
                txtPass.ForeColor = SystemColors.GrayText;
                contrasenaCambiada = false;
            }
        }

        private void TxtPass_TextChanged(object sender, EventArgs e)
        {
            if (ModoEdicion && txtPass.Text != "••••••••")
                contrasenaCambiada = true;
        }

        private void TxtRepeatPass_Enter(object sender, EventArgs e)
        {
            if (ModoEdicion && txtRepeatPass.Text == "••••••••")
            {
                txtRepeatPass.Clear();
                txtRepeatPass.ForeColor = SystemColors.ControlText;
            }
        }

        private void TxtRepeatPass_Leave(object sender, EventArgs e)
        {
            if (ModoEdicion && string.IsNullOrWhiteSpace(txtRepeatPass.Text))
            {
                txtRepeatPass.Text = "••••••••";
                txtRepeatPass.ForeColor = SystemColors.GrayText;
                repetirContrasenaCambiada = false;
            }
        }

        private void TxtRepeatPass_TextChanged(object sender, EventArgs e)
        {
            if (ModoEdicion && txtRepeatPass.Text != "••••••••")
                repetirContrasenaCambiada = true;
        }

        // ===== Guardar =====
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // ===== VALIDACIÓN CONTRASEÑAS =====
            if (!ModoEdicion) // Modo creación
            {
                if (string.IsNullOrWhiteSpace(txtPass.Text) || txtPass.Text == "••••••••")
                {
                    MessageBox.Show("La contraseña es obligatoria para nuevos empleados.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPass.Focus();
                    return;
                }

                if (txtPass.Text != txtRepeatPass.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPass.Focus();
                    txtPass.SelectAll();
                    return;
                }
            }
            else if (contrasenaCambiada || repetirContrasenaCambiada) // Modo edición con cambio de contraseña
            {
                if (txtPass.Text != txtRepeatPass.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPass.Focus();
                    txtPass.SelectAll();
                    return;
                }

                using (var admin = new ValidarAdministradorForm())
                {
                    if (admin.ShowDialog() != DialogResult.OK)
                    {
                        MessageBox.Show("No autorizado para cambiar contraseña.",
                            "Acceso denegado",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // ===== MODO EDICIÓN =====
            if (ModoEdicion)
            {
                // Crear un objeto nuevo con los datos actualizados
                EmpleadoCreado = new Empleado
                {
                    ID = EmpleadoAEditar.ID,
                    Nombre = txtNombre.Text.Trim(),
                    Correo = txtCorreo.Text.Trim(),
                    Direccion = txtDir.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Sucursal = textSucursal.Text.Trim(),
                    Rol = dropRol?.Text?.Trim() ?? "",
                    Ciudad = EmpleadoAEditar.Ciudad // Mantener la ciudad si existe
                };

                // Manejar la contraseña
                if (contrasenaCambiada && txtPass.Text != "••••••••" && !string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    EmpleadoCreado.Contrasena = txtPass.Text;
                }
                else
                {
                    EmpleadoCreado.Contrasena = EmpleadoAEditar.Contrasena;
                }

                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            // ===== MODO CREACIÓN =====
            EmpleadoCreado = new Empleado
            {
                Nombre = txtNombre.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Direccion = txtDir.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Sucursal = textSucursal.Text.Trim(),
                Rol = dropRol?.Text?.Trim() ?? "",
                Contrasena = txtPass.Text
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    // ===== FORM VALIDACIÓN ADMIN =====
    public class ValidarAdministradorForm : Form
    {
        private TextBox txtPass;
        private Button btnOk, btnCancel;
        private Label lblInfo;

        public ValidarAdministradorForm()
        {
            Text = "Validar Administrador";
            Size = new Size(350, 180);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            lblInfo = new Label
            {
                Text = "Ingrese la contraseña de administrador:",
                Location = new Point(20, 20),
                Size = new Size(300, 20),
                Font = new Font("Segoe UI", 9)
            };

            txtPass = new TextBox
            {
                PasswordChar = '•',
                Location = new Point(20, 50),
                Width = 290,
                Font = new Font("Segoe UI", 10)
            };

            btnOk = new Button
            {
                Text = "Aceptar",
                Location = new Point(140, 90),
                Size = new Size(80, 30),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOk.Click += (s, e) =>
            {
                if (txtPass.Text == "admin123") // Cambia esto por tu lógica real
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPass.Focus();
                    txtPass.SelectAll();
                }
            };

            btnCancel = new Button
            {
                Text = "Cancelar",
                Location = new Point(230, 90),
                Size = new Size(80, 30),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            Controls.AddRange(new Control[] { lblInfo, txtPass, btnOk, btnCancel });

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }
    }
}