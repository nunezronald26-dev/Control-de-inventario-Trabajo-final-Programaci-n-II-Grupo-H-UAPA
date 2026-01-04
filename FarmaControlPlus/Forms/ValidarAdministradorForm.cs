using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmaControlPlus.Forms
{
    public partial class ValidarAdministradorForm : Form
    {
        public string Accion { get; set; }

        public ValidarAdministradorForm(string accion = "realizar esta acción")
        {
            Accion = accion;
            // Llama al método definido en el Designer.cs
            InitializeComponent();

            // Actualizamos el texto dinámico después de inicializar componentes
            lblInfo.Text = $"Para {Accion}, ingrese la contraseña de administrador:";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Cambia "admin123" por tu lógica real de validación (ej. consulta a BD)
            if (txtPass.Text == "admin123")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                txtPass.SelectAll();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}