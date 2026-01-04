using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmaControlPlus.Forms
{
    public partial class NuevoEmpleadoDesdeLogin : EmpleadoFormBase
    {
        // Propiedad para saber si se requiere validación de admin
        public bool RequiereValidacionAdmin { get; set; } = false;

        // ==== CONSTRUCTOR PARA REGISTRO DESDE LOGIN ====
        public NuevoEmpleadoDesdeLogin(bool requiereValidacionAdmin = false)
        {
            InitializeComponent();
            ConfigurarControlesPassword(txtPass, txtRepeatPass);
            RequiereValidacionAdmin = requiereValidacionAdmin;

            this.Text = "Registrar Nuevo Empleado";
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación básica
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // Validar contraseña para nuevo empleado
            if (string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("La contraseña es obligatoria.", "Validación",
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

            // Validación de administrador si es requerida
            if (RequiereValidacionAdmin)
            {
                if (!ValidarContrasenaAdmin("crear empleado"))
                    return;
            }

            // Crear objeto Empleado (SIN CIUDAD)
            EmpleadoCreado = new Empleado
            {
                NombreCompleto = txtNombre.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Direccion = txtDir.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Sucursal = textSucursal.Text.Trim(),
                Rol = dropRol?.Text?.Trim() ?? "Empleado",
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
}