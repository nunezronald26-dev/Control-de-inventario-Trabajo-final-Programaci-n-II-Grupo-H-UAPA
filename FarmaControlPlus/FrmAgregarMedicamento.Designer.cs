namespace FarmaControlPlus
{
    partial class FrmAgregarMedicamento
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.numStock = new System.Windows.Forms.NumericUpDown();
            this.numPrecio = new System.Windows.Forms.NumericUpDown();
            this.dtpVencimiento = new System.Windows.Forms.DateTimePicker();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVencimiento = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecio)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Location = new System.Drawing.Point(30, 105);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(330, 35);
            this.txtNombre.TabIndex = 14;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCodigo.Location = new System.Drawing.Point(380, 105);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(330, 35);
            this.txtCodigo.TabIndex = 13;
            // 
            // numStock
            // 
            this.numStock.Location = new System.Drawing.Point(380, 175);
            this.numStock.Name = "numStock";
            this.numStock.Size = new System.Drawing.Size(330, 35);
            this.numStock.TabIndex = 11;
            // 
            // numPrecio
            // 
            this.numPrecio.Location = new System.Drawing.Point(30, 245);
            this.numPrecio.Name = "numPrecio";
            this.numPrecio.Size = new System.Drawing.Size(330, 35);
            this.numPrecio.Height = 35;
            this.numPrecio.TabIndex = 10;
            // 
            // dtpVencimiento
            // 
            this.dtpVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVencimiento.Location = new System.Drawing.Point(380, 245);
            this.dtpVencimiento.Name = "dtpVencimiento";
            this.dtpVencimiento.Size = new System.Drawing.Size(330, 23);
            this.dtpVencimiento.TabIndex = 9;
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(30, 301);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(332, 45);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Agregar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelar.Location = new System.Drawing.Point(380, 301);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(330, 45);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.Location = new System.Drawing.Point(30, 85);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(215, 17);
            this.lblNombre.TabIndex = 6;
            this.lblNombre.Text = "Nombre del medicamento";
            // 
            // lblCategoria
            // 
            this.lblCategoria.Location = new System.Drawing.Point(30, 155);
            this.lblCategoria.Margin = new System.Windows.Forms.Padding(0);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(215, 17);
            this.lblCategoria.TabIndex = 5;
            this.lblCategoria.Text = "Categoría";
            // 
            // lblCodigo
            // 
            this.lblCodigo.Location = new System.Drawing.Point(380, 85);
            this.lblCodigo.Margin = new System.Windows.Forms.Padding(0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(215, 17);
            this.lblCodigo.TabIndex = 4;
            this.lblCodigo.Text = "Código";
            // 
            // lblStock
            // 
            this.lblStock.Location = new System.Drawing.Point(380, 155);
            this.lblStock.Margin = new System.Windows.Forms.Padding(0);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(215, 17);
            this.lblStock.TabIndex = 3;
            this.lblStock.Text = "Stock Disponible";
            // 
            // lblPrecio
            // 
            this.lblPrecio.Location = new System.Drawing.Point(30, 225);
            this.lblPrecio.Margin = new System.Windows.Forms.Padding(0);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(204, 17);
            this.lblPrecio.TabIndex = 2;
            this.lblPrecio.Text = "Precio Unitario";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Agregar medicamentos";
            // 
            // lblVencimiento
            // 
            this.lblVencimiento.Location = new System.Drawing.Point(380, 225);
            this.lblVencimiento.Margin = new System.Windows.Forms.Padding(0);
            this.lblVencimiento.Name = "lblVencimiento";
            this.lblVencimiento.Size = new System.Drawing.Size(204, 17);
            this.lblVencimiento.TabIndex = 0;
            this.lblVencimiento.Text = "Fecha de Vencimiento";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.Items.AddRange(new object[] {
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
            "Vitaminas"});
            this.cmbCategoria.Location = new System.Drawing.Point(30, 175);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(330, 23);
            this.cmbCategoria.TabIndex = 12;
            // 
            // FrmAgregarMedicamento
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(750, 409);
            this.Controls.Add(this.lblVencimiento);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.dtpVencimiento);
            this.Controls.Add(this.numPrecio);
            this.Controls.Add(this.numStock);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.txtNombre);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmAgregarMedicamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Medicamento";
            ((System.ComponentModel.ISupportInitialize)(this.numStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPrecio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.NumericUpDown numStock;
        private System.Windows.Forms.NumericUpDown numPrecio;
        private System.Windows.Forms.DateTimePicker dtpVencimiento;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVencimiento;
        private System.Windows.Forms.ComboBox cmbCategoria;
    }
}