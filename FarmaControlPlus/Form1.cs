using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TuProyecto.Views;

namespace FarmaControlPlus
{
    public partial class Form1: Form
    {
        // Propiedad para almacenar el usuario actual
        public Empleado UsuarioActual { get; private set; }

        // Referencias a las vistas...
        private Dashboard dashboardView;
        private Inventario inventarioView;
        private Reportes reportesView;
        private Usuarios usuariosView;
        private UcdetalleUsuario UcdetalleUsuario;

        // Constructor modificado para recibir usuario
        public Form1(Empleado usuario = null)
        {
            InitializeComponent();

            // Guardar usuario actual
            UsuarioActual = usuario ?? new Empleado
            {
                NombreCompleto = "Admin User",
                Rol = "Administrador"
            };

            InicializarVistas();
            ConfigurarUIUsuario();
            MostrarVista(dashboardView);
        }

        // Método para configurar la UI según el usuario
        private void ConfigurarUIUsuario()
        {
            // 1. Mostrar nombre y rol del usuario
            lblNombreUsuario.Text = UsuarioActual.NombreCompleto;
            lblRangoUsuario.Text = UsuarioActual.Rol;

            // 2. Cambiar color del panelUsuario al mismo que btnUsuarios
            panelUsuario.BackColor = btnUsuarios.BackColor;

            // 3. Ocultar botón "Empleados" si no es administrador
            if (UsuarioActual.Rol?.ToLower() != "administrador")
            {
                btnUsuarios.Visible = false;

                // Reorganizar botones
                btnReportes.Top = btnUsuarios.Top; // Mover Reportes a la posición de Usuarios
            }
        }


        private void InicializarVistas()
        {
            // Crear instancias de las vistas
            dashboardView = new Dashboard();
            inventarioView = new Inventario();
            reportesView = new Reportes();
            usuariosView = new Usuarios();
            UcdetalleUsuario = new UcdetalleUsuario();

            // Configurar propiedades comunes
            ConfigurarVista(dashboardView);
            ConfigurarVista(inventarioView);
            ConfigurarVista(reportesView);
            ConfigurarVista(usuariosView);
        }

        private void ConfigurarVista(UserControl vista)
        {
            vista.Dock = DockStyle.Fill;
            vista.Visible = false;
            panelContenedor.Controls.Add(vista);
        }

        private void MostrarVista(UserControl vistaAMostrar)
        {
            if (vistaAMostrar == null)
            {
                throw new ArgumentNullException(nameof(vistaAMostrar));
            }

            // Ocultar todos los controles hijos en el contenedor (paneles y controles de usuario)
            foreach (Control control in panelContenedor.Controls)
            {
                control.Visible = false;
            }

            // Mostrar la vista solicitada y asegurarse de que esté al frente
            vistaAMostrar.Visible = true;
            vistaAMostrar.BringToFront();
        }

        private void MostrarPanel(Panel panelAMostrar)
        {
            if (panelAMostrar == null)
            {
                throw new ArgumentNullException(nameof(panelAMostrar));
            }
            panelAMostrar.Visible = true;
            // Ocultar todos los paneles
            pnlDashboard.Visible = false;
            pnlInventario.Visible = false;
            pnlReportes.Visible = false;
            pnlConfiguracion.Visible = false;
            pnlUsuarios.Visible = false;

            // Mostrar y llevar el panel solicitado al frente
            panelAMostrar.Visible = true;
            panelAMostrar.BringToFront();
        }

        // Event handlers del menú
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            MostrarVista(dashboardView);
            lblTitulo.Text = "Inicio";
        }

        public void btnInventario_Click(object sender, EventArgs e)
        {
            MostrarVista(inventarioView);
            lblTitulo.Text = "Inventario";
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            MostrarVista(reportesView);
            lblTitulo.Text = "Reportes";
        }

        public void btnUsuarios_Click(object sender, EventArgs e)
        {
            MostrarVista(usuariosView);
            lblTitulo.Text = "Gestión de Empleados";
        }

        // Efectos hover para panelUsuario
        private void panelUsuario_MouseEnter(object sender, EventArgs e)
        {
            panelUsuario.BackColor = Color.FromArgb(68, 89, 109); // Color más claro
            panelUsuario.Cursor = Cursors.Hand;
        }

        private void panelUsuario_MouseLeave(object sender, EventArgs e)
        {
            panelUsuario.BackColor = Color.FromArgb(58, 79, 99); // Color original
        }

        private void lblNombreUsuario_MouseEnter(object sender, EventArgs e)
        {
            panelUsuario_MouseEnter(sender, e);
        }

        private void lblNombreUsuario_MouseLeave(object sender, EventArgs e)
        {
            panelUsuario_MouseLeave(sender, e);
        }

        private void lblRangoUsuario_MouseEnter(object sender, EventArgs e)
        {
            panelUsuario_MouseEnter(sender, e);
        }

        private void lblRangoUsuario_MouseLeave(object sender, EventArgs e)
        {
            panelUsuario_MouseLeave(sender, e);
        }

        private void InformaciónUsuario(object sender, EventArgs e)
        {
            // 1. Ensure instance exists
            if (UcdetalleUsuario == null)
            {
                UcdetalleUsuario = new UcdetalleUsuario();
            }

            // 2. Pasar datos del usuario actual al control
            UcdetalleUsuario.CargarDatosUsuario(UsuarioActual);

            // 3. If not added to the container, configure and add it
            if (!panelContenedor.Controls.Contains(UcdetalleUsuario))
            {
                ConfigurarVista(UcdetalleUsuario);
            }

            // 4. Show the user control using the existing helper (hides other views)
            MostrarVista(UcdetalleUsuario);

            // 5. Update title
            lblTitulo.Text = "Mi Perfil";
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
        "¿Está seguro que desea cerrar sesión?",
        "Confirmar Cierre de Sesión",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question,
        MessageBoxDefaultButton.Button2); // El No como opción por defecto

            if (result == DialogResult.Yes)
            {
                CerrarSesion();
            }
        }

        private void CerrarSesion()
        {
            try
            {
                // 1. Mostrar mensaje de despedida
                using (var modal = new SuccessModal("Sesión finalizada. ¡Hasta pronto!", 1500))
                {
                    modal.ShowDialog(this);
                }

                // 2. Cerrar el formulario principal
                this.Hide();

                // 3. Mostrar nuevamente el formulario de login
                Login loginForm = new Login();
                loginForm.Show();

                // 4. Cerrar este formulario cuando se cierre el login
                loginForm.FormClosed += (s, args) => this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cerrar sesión: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
