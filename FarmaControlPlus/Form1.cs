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
        // Referencias a las vistas
        private Dashboard dashboardView;
        private Inventario inventarioView;
        private Reportes reportesView;
        private Configuracion configuracionView;
        private Usuarios usuariosView;

        public Form1()
        {
            InitializeComponent();
            InicializarVistas();
            MostrarVista(dashboardView);
        }

        private void InicializarVistas()
        {
            // Crear instancias de las vistas
            dashboardView = new Dashboard();
            inventarioView = new Inventario();
            reportesView = new Reportes();
            configuracionView = new Configuracion();
            usuariosView = new Usuarios();

            // Configurar propiedades comunes
            ConfigurarVista(dashboardView);
            ConfigurarVista(inventarioView);
            ConfigurarVista(reportesView);
            ConfigurarVista(configuracionView);
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

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            MostrarVista(configuracionView);
            lblTitulo.Text = "Configuración";
        }

        public void btnUsuarios_Click(object sender, EventArgs e)
        {
            MostrarVista(usuariosView);
            lblTitulo.Text = "Gestión de Empleados";
        }
    }
}
