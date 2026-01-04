using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TuProyecto.Views;

namespace FarmaControlPlus.Forms
{
    public partial class NuevoEmpleado : Form
    {
        // Propiedad para devolver el empleado creado al formulario principal
        public Empleado EmpleadoCreado { get; private set; }

        public NuevoEmpleado()
        {
            InitializeComponent();
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;
        }

        private void NuevoEmpleado_Load(object sender, EventArgs e)
        {
            // Establecer foco en el primer campo
            txtNombreCompleto.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación básica
            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text) || string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Nombre y Apellidos son obligatorios.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNombreCompleto.Focus();
                return;
            }

            // Mapeo de datos al objeto Empleado
            EmpleadoCreado = new Empleado
            {
                NombreCompleto = txtNombreCompleto.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Direccion = txtDireccion.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Sucursal = txtSucursal.Text.Trim(),
                Rol = txtRol.Text.Trim()
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
                if (control is TextBox && control != txtSucursal)
                {
                    this.SelectNextControl(control, true, true, true, true);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void NuevoEmpleado_Load_1(object sender, EventArgs e)
        {

        }
    }
}