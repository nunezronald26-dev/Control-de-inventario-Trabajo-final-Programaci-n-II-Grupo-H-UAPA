using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FarmaControlPlus
{
    public partial class FrmAgregarMedicamento : Form
    {
        public Medicamento MedicamentoCreado { get; private set; }

        public FrmAgregarMedicamento()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FrmAgregarMedicamento_Load(object sender, EventArgs e)
        {
            cmbCategoria.SelectedIndex = 0;

            // Configurar fecha mínima para vencimiento (mañana)
            dtpVencimiento.MinDate = DateTime.Today.AddDays(1);

            // Establecer valores iniciales
            numStock.Value = 1;
            numPrecio.Value = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show(
                    "Debe completar el nombre y el código del medicamento",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (numPrecio.Value <= 0)
            {
                MessageBox.Show(
                    "El precio debe ser mayor a cero",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            MedicamentoCreado = new Medicamento
            {
                Nombre = txtNombre.Text.Trim(),
                Codigo = txtCodigo.Text.Trim(),
                Categoria = cmbCategoria.Text,
                Stock = (int)numStock.Value,
                PrecioUnitario = numPrecio.Value,
                FechaVencimiento = dtpVencimiento.Value
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}