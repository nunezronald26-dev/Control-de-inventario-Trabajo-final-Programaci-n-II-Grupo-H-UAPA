namespace TuProyecto.Views
{
    partial class Inventario
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelBusqueda = new System.Windows.Forms.Panel();
            this.btnLimpiarBusqueda = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.groupBoxInventario = new System.Windows.Forms.GroupBox();
            this.dataGridViewInventario = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnFiltrarCriticos = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.btnNuevoMedicamento = new System.Windows.Forms.Button();
            this.groupBoxGuia = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelGuia = new System.Windows.Forms.FlowLayoutPanel();
            this.panelSinStock = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSinStock = new System.Windows.Forms.Label();
            this.panelPuntoVencer = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPuntoVencer = new System.Windows.Forms.Label();
            this.panelPorVencer = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPorVencerGuia = new System.Windows.Forms.Label();
            this.panelBajoStock = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.lblBajoStock = new System.Windows.Forms.Label();
            this.panelNormal = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.lblNormal = new System.Windows.Forms.Label();
            this.panelDetalles = new System.Windows.Forms.Panel();
            this.lblDetalles = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panelBusqueda.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.groupBoxInventario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInventario)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.groupBoxGuia.SuspendLayout();
            this.flowLayoutPanelGuia.SuspendLayout();
            this.panelSinStock.SuspendLayout();
            this.panelPuntoVencer.SuspendLayout();
            this.panelPorVencer.SuspendLayout();
            this.panelBajoStock.SuspendLayout();
            this.panelNormal.SuspendLayout();
            this.panelDetalles.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBusqueda.Controls.Add(this.btnLimpiarBusqueda);
            this.panelBusqueda.Controls.Add(this.btnBuscar);
            this.panelBusqueda.Controls.Add(this.txtBuscar);
            this.panelBusqueda.Controls.Add(this.label2);
            this.panelBusqueda.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBusqueda.Location = new System.Drawing.Point(0, 0);
            this.panelBusqueda.Name = "panelBusqueda";
            this.panelBusqueda.Size = new System.Drawing.Size(1005, 50);
            this.panelBusqueda.TabIndex = 1;
            // 
            // btnLimpiarBusqueda
            // 
            this.btnLimpiarBusqueda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnLimpiarBusqueda.FlatAppearance.BorderSize = 0;
            this.btnLimpiarBusqueda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarBusqueda.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLimpiarBusqueda.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLimpiarBusqueda.Location = new System.Drawing.Point(534, 10);
            this.btnLimpiarBusqueda.Name = "btnLimpiarBusqueda";
            this.btnLimpiarBusqueda.Size = new System.Drawing.Size(100, 30);
            this.btnLimpiarBusqueda.TabIndex = 3;
            this.btnLimpiarBusqueda.Text = "Limpiar";
            this.btnLimpiarBusqueda.UseVisualStyleBackColor = false;
            this.btnLimpiarBusqueda.Click += new System.EventHandler(this.btnLimpiarBusqueda_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnBuscar.Location = new System.Drawing.Point(419, 10);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(100, 30);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(91, 15);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(311, 20);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(20, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Buscar:";
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.White;
            this.panelContenido.Controls.Add(this.groupBoxInventario);
            this.panelContenido.Controls.Add(this.panelBotones);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenido.Location = new System.Drawing.Point(0, 50);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(775, 337);
            this.panelContenido.TabIndex = 2;
            // 
            // groupBoxInventario
            // 
            this.groupBoxInventario.Controls.Add(this.dataGridViewInventario);
            this.groupBoxInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxInventario.Location = new System.Drawing.Point(0, 0);
            this.groupBoxInventario.Name = "groupBoxInventario";
            this.groupBoxInventario.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxInventario.Size = new System.Drawing.Size(775, 287);
            this.groupBoxInventario.TabIndex = 2;
            this.groupBoxInventario.TabStop = false;
            this.groupBoxInventario.Text = "Inventario de Medicamentos";
            // 
            // dataGridViewInventario
            // 
            this.dataGridViewInventario.AllowUserToAddRows = false;
            this.dataGridViewInventario.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dataGridViewInventario.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewInventario.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewInventario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewInventario.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewInventario.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewInventario.ColumnHeadersHeight = 40;
            this.dataGridViewInventario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Codigo,
            this.Categoria,
            this.Stock,
            this.Precio,
            this.FechaVencimiento});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewInventario.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewInventario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewInventario.EnableHeadersVisualStyles = false;
            this.dataGridViewInventario.Location = new System.Drawing.Point(10, 24);
            this.dataGridViewInventario.Name = "dataGridViewInventario";
            this.dataGridViewInventario.ReadOnly = true;
            this.dataGridViewInventario.RowHeadersVisible = false;
            this.dataGridViewInventario.RowTemplate.Height = 35;
            this.dataGridViewInventario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewInventario.Size = new System.Drawing.Size(755, 253);
            this.dataGridViewInventario.TabIndex = 0;
            this.dataGridViewInventario.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewInventario_CellClick);
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre del Medicamento";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 250;
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "Código";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 90;
            // 
            // Categoria
            // 
            this.Categoria.HeaderText = "Categoría";
            this.Categoria.Name = "Categoria";
            this.Categoria.ReadOnly = true;
            // 
            // Stock
            // 
            this.Stock.HeaderText = "Stock";
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Width = 70;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio Unitario";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            // 
            // FechaVencimiento
            // 
            this.FechaVencimiento.HeaderText = "Fecha de Vencimiento";
            this.FechaVencimiento.Name = "FechaVencimiento";
            this.FechaVencimiento.ReadOnly = true;
            this.FechaVencimiento.Width = 160;
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelBotones.Controls.Add(this.btnExportar);
            this.panelBotones.Controls.Add(this.btnFiltrarCriticos);
            this.panelBotones.Controls.Add(this.btnActualizar);
            this.panelBotones.Controls.Add(this.btnNuevoMedicamento);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotones.Location = new System.Drawing.Point(0, 287);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(775, 50);
            this.panelBotones.TabIndex = 1;
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.PaleGreen;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExportar.ForeColor = System.Drawing.Color.Black;
            this.btnExportar.Location = new System.Drawing.Point(644, 10);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(100, 30);
            this.btnExportar.TabIndex = 4;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnFiltrarCriticos
            // 
            this.btnFiltrarCriticos.BackColor = System.Drawing.Color.LightCoral;
            this.btnFiltrarCriticos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrarCriticos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnFiltrarCriticos.ForeColor = System.Drawing.Color.Black;
            this.btnFiltrarCriticos.Location = new System.Drawing.Point(270, 10);
            this.btnFiltrarCriticos.Name = "btnFiltrarCriticos";
            this.btnFiltrarCriticos.Size = new System.Drawing.Size(170, 30);
            this.btnFiltrarCriticos.TabIndex = 2;
            this.btnFiltrarCriticos.Text = "Mostrar Solo Críticos";
            this.btnFiltrarCriticos.UseVisualStyleBackColor = false;
            this.btnFiltrarCriticos.Click += new System.EventHandler(this.btnFiltrarCriticos_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.Color.White;
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnActualizar.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnActualizar.Location = new System.Drawing.Point(140, 10);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(120, 30);
            this.btnActualizar.TabIndex = 1;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnNuevoMedicamento
            // 
            this.btnNuevoMedicamento.BackColor = System.Drawing.Color.PaleGreen;
            this.btnNuevoMedicamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoMedicamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevoMedicamento.ForeColor = System.Drawing.Color.Black;
            this.btnNuevoMedicamento.Location = new System.Drawing.Point(10, 10);
            this.btnNuevoMedicamento.Name = "btnNuevoMedicamento";
            this.btnNuevoMedicamento.Size = new System.Drawing.Size(120, 30);
            this.btnNuevoMedicamento.TabIndex = 0;
            this.btnNuevoMedicamento.Text = "Nuevo +";
            this.btnNuevoMedicamento.UseVisualStyleBackColor = false;
            this.btnNuevoMedicamento.Click += new System.EventHandler(this.btnNuevoMedicamento_Click);
            // 
            // groupBoxGuia
            // 
            this.groupBoxGuia.Controls.Add(this.flowLayoutPanelGuia);
            this.groupBoxGuia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBoxGuia.Location = new System.Drawing.Point(10, 215);
            this.groupBoxGuia.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxGuia.Name = "groupBoxGuia";
            this.groupBoxGuia.Size = new System.Drawing.Size(205, 295);
            this.groupBoxGuia.TabIndex = 3;
            this.groupBoxGuia.TabStop = false;
            this.groupBoxGuia.Text = "Estados de Medicamentos";
            this.groupBoxGuia.Enter += new System.EventHandler(this.groupBoxGuia_Enter);
            // 
            // flowLayoutPanelGuia
            // 
            this.flowLayoutPanelGuia.AutoScroll = true;
            this.flowLayoutPanelGuia.Controls.Add(this.panelSinStock);
            this.flowLayoutPanelGuia.Controls.Add(this.panelPuntoVencer);
            this.flowLayoutPanelGuia.Controls.Add(this.panelPorVencer);
            this.flowLayoutPanelGuia.Controls.Add(this.panelBajoStock);
            this.flowLayoutPanelGuia.Controls.Add(this.panelNormal);
            this.flowLayoutPanelGuia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelGuia.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanelGuia.Name = "flowLayoutPanelGuia";
            this.flowLayoutPanelGuia.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanelGuia.Size = new System.Drawing.Size(199, 275);
            this.flowLayoutPanelGuia.TabIndex = 0;
            this.flowLayoutPanelGuia.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanelGuia_Paint);
            // 
            // panelSinStock
            // 
            this.panelSinStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelSinStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSinStock.Controls.Add(this.label6);
            this.panelSinStock.Controls.Add(this.lblSinStock);
            this.panelSinStock.Location = new System.Drawing.Point(6, 6);
            this.panelSinStock.Name = "panelSinStock";
            this.panelSinStock.Size = new System.Drawing.Size(181, 42);
            this.panelSinStock.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label6.Location = new System.Drawing.Point(4, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Stock 0 unidades";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // lblSinStock
            // 
            this.lblSinStock.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSinStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.lblSinStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.lblSinStock.Location = new System.Drawing.Point(0, 0);
            this.lblSinStock.Name = "lblSinStock";
            this.lblSinStock.Size = new System.Drawing.Size(179, 16);
            this.lblSinStock.TabIndex = 0;
            this.lblSinStock.Text = "SIN STOCK";
            this.lblSinStock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPuntoVencer
            // 
            this.panelPuntoVencer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(200)))));
            this.panelPuntoVencer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPuntoVencer.Controls.Add(this.label7);
            this.panelPuntoVencer.Controls.Add(this.lblPuntoVencer);
            this.panelPuntoVencer.Location = new System.Drawing.Point(6, 54);
            this.panelPuntoVencer.Name = "panelPuntoVencer";
            this.panelPuntoVencer.Size = new System.Drawing.Size(181, 42);
            this.panelPuntoVencer.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label7.Location = new System.Drawing.Point(-1, 20);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "Vence < 30 días";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // lblPuntoVencer
            // 
            this.lblPuntoVencer.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPuntoVencer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.lblPuntoVencer.ForeColor = System.Drawing.Color.Black;
            this.lblPuntoVencer.Location = new System.Drawing.Point(0, 0);
            this.lblPuntoVencer.Name = "lblPuntoVencer";
            this.lblPuntoVencer.Size = new System.Drawing.Size(179, 16);
            this.lblPuntoVencer.TabIndex = 0;
            this.lblPuntoVencer.Text = "POR VENCER";
            this.lblPuntoVencer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelPorVencer
            // 
            this.panelPorVencer.BackColor = System.Drawing.Color.DarkOrange;
            this.panelPorVencer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPorVencer.Controls.Add(this.label8);
            this.panelPorVencer.Controls.Add(this.lblPorVencerGuia);
            this.panelPorVencer.Location = new System.Drawing.Point(6, 102);
            this.panelPorVencer.Name = "panelPorVencer";
            this.panelPorVencer.Size = new System.Drawing.Size(181, 42);
            this.panelPorVencer.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(-1, 20);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(181, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Fecha caducada";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // lblPorVencerGuia
            // 
            this.lblPorVencerGuia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPorVencerGuia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.lblPorVencerGuia.ForeColor = System.Drawing.Color.White;
            this.lblPorVencerGuia.Location = new System.Drawing.Point(0, 0);
            this.lblPorVencerGuia.Name = "lblPorVencerGuia";
            this.lblPorVencerGuia.Size = new System.Drawing.Size(179, 16);
            this.lblPorVencerGuia.TabIndex = 0;
            this.lblPorVencerGuia.Text = "VENCIDO";
            this.lblPorVencerGuia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPorVencerGuia.Click += new System.EventHandler(this.lblPorVencerGuia_Click);
            // 
            // panelBajoStock
            // 
            this.panelBajoStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panelBajoStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBajoStock.Controls.Add(this.label9);
            this.panelBajoStock.Controls.Add(this.lblBajoStock);
            this.panelBajoStock.Location = new System.Drawing.Point(6, 150);
            this.panelBajoStock.Name = "panelBajoStock";
            this.panelBajoStock.Size = new System.Drawing.Size(180, 42);
            this.panelBajoStock.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(-1, 20);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(179, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "< 10 unidades";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // lblBajoStock
            // 
            this.lblBajoStock.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBajoStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.lblBajoStock.ForeColor = System.Drawing.Color.White;
            this.lblBajoStock.Location = new System.Drawing.Point(0, 0);
            this.lblBajoStock.Name = "lblBajoStock";
            this.lblBajoStock.Size = new System.Drawing.Size(178, 16);
            this.lblBajoStock.TabIndex = 0;
            this.lblBajoStock.Text = "BAJO STOCK";
            this.lblBajoStock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelNormal
            // 
            this.panelNormal.BackColor = System.Drawing.Color.White;
            this.panelNormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNormal.Controls.Add(this.label10);
            this.panelNormal.Controls.Add(this.lblNormal);
            this.panelNormal.Location = new System.Drawing.Point(6, 198);
            this.panelNormal.Name = "panelNormal";
            this.panelNormal.Size = new System.Drawing.Size(181, 42);
            this.panelNormal.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label10.Location = new System.Drawing.Point(4, 20);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(176, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Stock normal";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // lblNormal
            // 
            this.lblNormal.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNormal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.lblNormal.ForeColor = System.Drawing.Color.Black;
            this.lblNormal.Location = new System.Drawing.Point(0, 0);
            this.lblNormal.Name = "lblNormal";
            this.lblNormal.Size = new System.Drawing.Size(179, 16);
            this.lblNormal.TabIndex = 0;
            this.lblNormal.Text = "NORMAL";
            this.lblNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDetalles
            // 
            this.panelDetalles.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelDetalles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDetalles.Controls.Add(this.groupBoxGuia);
            this.panelDetalles.Controls.Add(this.lblDetalles);
            this.panelDetalles.Controls.Add(this.label12);
            this.panelDetalles.Controls.Add(this.label13);
            this.panelDetalles.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDetalles.Location = new System.Drawing.Point(775, 50);
            this.panelDetalles.Name = "panelDetalles";
            this.panelDetalles.Size = new System.Drawing.Size(230, 337);
            this.panelDetalles.TabIndex = 3;
            // 
            // lblDetalles
            // 
            this.lblDetalles.AutoEllipsis = true;
            this.lblDetalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblDetalles.Location = new System.Drawing.Point(10, 80);
            this.lblDetalles.Name = "lblDetalles";
            this.lblDetalles.Size = new System.Drawing.Size(205, 184);
            this.lblDetalles.TabIndex = 2;
            this.lblDetalles.Text = "Seleccione un medicamento para ver sus detalles...";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(10, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 17);
            this.label12.TabIndex = 1;
            this.label12.Text = "Detalles:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(10, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(205, 17);
            this.label13.TabIndex = 0;
            this.label13.Text = "Medicamento Seleccionado";
            // 
            // Inventario
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelDetalles);
            this.Controls.Add(this.panelBusqueda);
            this.Name = "Inventario";
            this.Size = new System.Drawing.Size(1005, 387);
            this.Load += new System.EventHandler(this.Inventario_Load);
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.groupBoxInventario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInventario)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.groupBoxGuia.ResumeLayout(false);
            this.flowLayoutPanelGuia.ResumeLayout(false);
            this.panelSinStock.ResumeLayout(false);
            this.panelPuntoVencer.ResumeLayout(false);
            this.panelPorVencer.ResumeLayout(false);
            this.panelBajoStock.ResumeLayout(false);
            this.panelNormal.ResumeLayout(false);
            this.panelDetalles.ResumeLayout(false);
            this.panelDetalles.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelBusqueda;
        private System.Windows.Forms.Button btnLimpiarBusqueda;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.GroupBox groupBoxGuia;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGuia;
        private System.Windows.Forms.Panel panelSinStock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSinStock;
        private System.Windows.Forms.Panel panelPuntoVencer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPuntoVencer;
        private System.Windows.Forms.Panel panelPorVencer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPorVencerGuia;
        private System.Windows.Forms.Panel panelBajoStock;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblBajoStock;
        private System.Windows.Forms.Panel panelNormal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblNormal;
        private System.Windows.Forms.GroupBox groupBoxInventario;
        private System.Windows.Forms.DataGridView dataGridViewInventario;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnFiltrarCriticos;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Button btnNuevoMedicamento;
        private System.Windows.Forms.Panel panelDetalles;
        private System.Windows.Forms.Label lblDetalles;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaVencimiento;
    }
}