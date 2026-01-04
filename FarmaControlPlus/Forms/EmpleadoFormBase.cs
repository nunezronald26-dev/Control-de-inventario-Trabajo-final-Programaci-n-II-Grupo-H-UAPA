using System;
using System.Drawing;
using System.Windows.Forms;

namespace FarmaControlPlus.Forms
{
    public partial class EmpleadoFormBase : Form
    {
        public Empleado EmpleadoCreado { get; protected set; }
        protected bool contrasenaCambiada = false;
        protected bool repetirContrasenaCambiada = false;

        public EmpleadoFormBase()
        {
            InitializeComponent();
        }

        protected void ConfigurarControlesPassword(TextBox txtPass, TextBox txtRepeatPass)
        {
            txtPass.PasswordChar = '•';
            txtRepeatPass.PasswordChar = '•';
        }

        protected bool ValidarContrasenaAdmin(string accion)
        {
            using (var admin = new ValidarAdministradorForm(accion))
            {
                if (admin.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show($"No autorizado para {accion.ToLower()}.",
                        "Acceso denegado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
    }
}