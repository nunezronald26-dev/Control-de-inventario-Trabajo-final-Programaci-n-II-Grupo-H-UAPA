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
            cmbCategoria.Items.AddRange(new string[]
            {
        "Analgésicos",
        "Anestésicos",
        "Antialérgicos",
        "Antibióticos",
        "Antifúngicos",
        "Antiinflamatorios",
        "Antiparasitarios",
        "Antipiréticos",
        "Antisépticos",
        "Antivirales",
        "Cardiovasculares",
        "Corticoides",
        "Dermatológicos",
        "Digestivos",
        "Endocrinos",
        "Gastroprotectores",
        "Hematológicos",
        "Inmunológicos",
        "Inyectables",
        "Jarabes",
        "Oftálmicos",
        "Otológicos",
        "Pediátricos",
        "Respiratorios",
        "Suplementos",
        "Urológicos",
        "Vacunas",
        "Vitaminas"
            });

            cmbCategoria.SelectedIndex = 0;
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
            this.Close();
        }
    }
}
