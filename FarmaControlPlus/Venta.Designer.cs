namespace FarmaControlPlus
{
    partial class Venta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnPagar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Ventagrid = new System.Windows.Forms.DataGridView();
            this.form4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Ventagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(758, 327);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(223, 20);
            this.txtTotal.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(5, 324);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnPagar
            // 
            this.btnPagar.Location = new System.Drawing.Point(677, 327);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(75, 23);
            this.btnPagar.TabIndex = 4;
            this.btnPagar.Text = "Pagar";
            this.btnPagar.UseVisualStyleBackColor = true;
            this.btnPagar.Click += new System.EventHandler(this.btnPagar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(755, 311);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Total:";
            // 
            // Ventagrid
            // 
            this.Ventagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Ventagrid.Location = new System.Drawing.Point(5, 22);
            this.Ventagrid.Name = "Ventagrid";
            this.Ventagrid.Size = new System.Drawing.Size(483, 262);
            this.Ventagrid.TabIndex = 10;
            this.Ventagrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ventagrid_CellClick);
            // 
            // form4
            // 
            this.form4.AutoSize = true;
            this.form4.Location = new System.Drawing.Point(491, 6);
            this.form4.Name = "form4";
            this.form4.Size = new System.Drawing.Size(94, 13);
            this.form4.TabIndex = 8;
            this.form4.Text = "Detalles de Venta:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Medicamentos:";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Location = new System.Drawing.Point(494, 22);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.Size = new System.Drawing.Size(487, 262);
            this.dgvDetalle.TabIndex = 12;
            this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellContentClick);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(5, 298);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(188, 20);
            this.txtBuscar.TabIndex = 13;
            // 
            // Venta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 450);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Ventagrid);
            this.Controls.Add(this.form4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPagar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtTotal);
            this.Name = "Venta";
            this.Text = "Venta";
            this.Load += new System.EventHandler(this.Venta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Ventagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView Ventagrid;
        private System.Windows.Forms.Label form4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.TextBox txtBuscar;
    }
}