using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmaControlPlus.Forms
{
    public partial class EditarEmpleado : EmpleadoFormBase
    {
        public Empleado EmpleadoAEditar { get; private set; }

        // ==== CONSTRUCTOR PARA EDITAR ====
        public EditarEmpleado(Empleado empleadoExistente)
        {
            InitializeComponent();
            ConfigurarControlesPassword(txtPass, txtRepeatPass);

            EmpleadoAEditar = empleadoExistente;
            this.Text = "Modificar Empleado";
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;

            // Cargar datos
            CargarDatosEmpleado();
        }

        private void CargarDatosEmpleado()
        {
            if (EmpleadoAEditar == null) return;

            txtNombre.Text = EmpleadoAEditar.NombreCompleto ?? "";
            txtCorreo.Text = EmpleadoAEditar.Correo ?? "";
            txtDir.Text = EmpleadoAEditar.Direccion ?? "";
            txtTelefono.Text = EmpleadoAEditar.Telefono ?? "";
            textSucursal.Text = EmpleadoAEditar.Sucursal ?? "";

            if (dropRol != null && !string.IsNullOrEmpty(EmpleadoAEditar.Rol))
            {
                dropRol.Text = EmpleadoAEditar.Rol;
            }

            // Configurar placeholders para contraseñas
            txtPass.Text = "••••••••";
            txtPass.ForeColor = SystemColors.GrayText;
            txtRepeatPass.Text = "••••••••";
            txtRepeatPass.ForeColor = SystemColors.GrayText;

            // Agregar eventos
            txtPass.Enter += TxtPass_Enter;
            txtPass.Leave += TxtPass_Leave;
            txtPass.TextChanged += TxtPass_TextChanged;

            txtRepeatPass.Enter += TxtRepeatPass_Enter;
            txtRepeatPass.Leave += TxtRepeatPass_Leave;
            txtRepeatPass.TextChanged += TxtRepeatPass_TextChanged;
        }

        // Eventos para contraseña
        private void TxtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "••••••••")
            {
                txtPass.Clear();
                txtPass.ForeColor = SystemColors.ControlText;
            }
        }

        private void TxtPass_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPass.Text))
            {
                txtPass.Text = "••••••••";
                txtPass.ForeColor = SystemColors.GrayText;
                contrasenaCambiada = false;
            }
        }

        private void TxtPass_TextChanged(object sender, EventArgs e)
        {
            if (txtPass.Text != "••••••••")
                contrasenaCambiada = true;
        }

        private void TxtRepeatPass_Enter(object sender, EventArgs e)
        {
            if (txtRepeatPass.Text == "••••••••")
            {
                txtRepeatPass.Clear();
                txtRepeatPass.ForeColor = SystemColors.ControlText;
            }
        }

        private void TxtRepeatPass_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRepeatPass.Text))
            {
                txtRepeatPass.Text = "••••••••";
                txtRepeatPass.ForeColor = SystemColors.GrayText;
                repetirContrasenaCambiada = false;
            }
        }

        private void TxtRepeatPass_TextChanged(object sender, EventArgs e)
        {
            if (txtRepeatPass.Text != "••••••••")
                repetirContrasenaCambiada = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // Validación de contraseña si se cambió
            if (contrasenaCambiada || repetirContrasenaCambiada)
            {
                if (txtPass.Text != txtRepeatPass.Text)
                {
                    MessageBox.Show("Las contraseñas no coinciden.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPass.Focus();
                    txtPass.SelectAll();
                    return;
                }

                // Validar administrador para cambiar contraseña
                if (!ValidarContrasenaAdmin("cambiar contraseña"))
                    return;
            }

            // Actualizar objeto (SIN CIUDAD)
            EmpleadoCreado = new Empleado
            {
                ID = EmpleadoAEditar.ID,
                NombreCompleto = txtNombre.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Direccion = txtDir.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Sucursal = textSucursal.Text.Trim(),
                Rol = dropRol?.Text?.Trim() ?? ""
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
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}