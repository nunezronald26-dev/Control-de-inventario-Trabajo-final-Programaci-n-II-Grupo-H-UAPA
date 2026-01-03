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
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación básica
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Nombre y Apellidos son obligatorios.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            // Mapeo de datos al objeto Empleado
            EmpleadoCreado = new Empleado
            {
                Nombre = txtNombre.Text.Trim(),
                Apellidos = txtPass.Text.Trim(),
                Direccion = txtRepeatPass.Text.Trim(),
                Ciudad = txtDir.Text.Trim(),
                Telefono = txtTelefono.Text.Trim()
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
                if (control is TextBox && control != txtTelefono)
                {
                    this.SelectNextControl(control, true, true, true, true);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblCiudad_Click(object sender, EventArgs e)
        {

        }
    }
}