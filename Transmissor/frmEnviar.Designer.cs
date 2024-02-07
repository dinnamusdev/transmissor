namespace Transmissor
{
    partial class frmEnviar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dbgFilaDestino = new System.Windows.Forms.DataGridView();
            this.Loja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transmitir_EM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.dbgEnvio = new System.Windows.Forms.DataGridView();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Arquivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.dbgFilaDestino_OK = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dbgEnvio_OK = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btFechar = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btAtualizar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbgFilaDestino)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbgEnvio)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbgFilaDestino_OK)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbgEnvio_OK)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 399);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 65);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(609, 330);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dbgFilaDestino);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dbgEnvio);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(601, 304);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "PENDENTES";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dbgFilaDestino
            // 
            this.dbgFilaDestino.AllowUserToAddRows = false;
            this.dbgFilaDestino.AllowUserToDeleteRows = false;
            this.dbgFilaDestino.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbgFilaDestino.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Loja,
            this.Email,
            this.Transmitir_EM,
            this.Status});
            this.dbgFilaDestino.Location = new System.Drawing.Point(6, 176);
            this.dbgFilaDestino.Name = "dbgFilaDestino";
            this.dbgFilaDestino.ReadOnly = true;
            this.dbgFilaDestino.Size = new System.Drawing.Size(589, 122);
            this.dbgFilaDestino.TabIndex = 2;
            // 
            // Loja
            // 
            this.Loja.DataPropertyName = "NOME";
            this.Loja.HeaderText = "Loja";
            this.Loja.Name = "Loja";
            this.Loja.ReadOnly = true;
            this.Loja.Width = 160;
            // 
            // Email
            // 
            this.Email.DataPropertyName = "email";
            this.Email.HeaderText = "Email";
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            this.Email.Width = 170;
            // 
            // Transmitir_EM
            // 
            this.Transmitir_EM.DataPropertyName = "DataHoraEnviar";
            this.Transmitir_EM.HeaderText = "Transmitir EM";
            this.Transmitir_EM.Name = "Transmitir_EM";
            this.Transmitir_EM.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(589, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "DESTINO DO ARQUIVO";
            // 
            // dbgEnvio
            // 
            this.dbgEnvio.AllowUserToAddRows = false;
            this.dbgEnvio.AllowUserToDeleteRows = false;
            this.dbgEnvio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbgEnvio.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Tipo,
            this.Codigo,
            this.Data,
            this.Arquivo});
            this.dbgEnvio.Location = new System.Drawing.Point(6, 17);
            this.dbgEnvio.Name = "dbgEnvio";
            this.dbgEnvio.ReadOnly = true;
            this.dbgEnvio.Size = new System.Drawing.Size(589, 133);
            this.dbgEnvio.TabIndex = 0;
            this.dbgEnvio.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dbgEnvio_CellContentClick);
            this.dbgEnvio.SelectionChanged += new System.EventHandler(this.dbgEnvio_SelectionChanged);
            // 
            // Tipo
            // 
            this.Tipo.DataPropertyName = "NomeModulo";
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            // 
            // Codigo
            // 
            this.Codigo.DataPropertyName = "IDRegistro";
            this.Codigo.HeaderText = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 70;
            // 
            // Data
            // 
            this.Data.DataPropertyName = "Data";
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // Arquivo
            // 
            this.Arquivo.DataPropertyName = "NomeArquivo";
            this.Arquivo.HeaderText = "Arquivo";
            this.Arquivo.Name = "Arquivo";
            this.Arquivo.ReadOnly = true;
            this.Arquivo.Width = 260;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.dbgFilaDestino_OK);
            this.tabPage2.Controls.Add(this.dbgEnvio_OK);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(601, 304);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ENVIADOS";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(589, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "DESTINO DO ARQUIVO";
            // 
            // dbgFilaDestino_OK
            // 
            this.dbgFilaDestino_OK.AllowUserToAddRows = false;
            this.dbgFilaDestino_OK.AllowUserToDeleteRows = false;
            this.dbgFilaDestino_OK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbgFilaDestino_OK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.dbgFilaDestino_OK.Location = new System.Drawing.Point(6, 171);
            this.dbgFilaDestino_OK.Name = "dbgFilaDestino_OK";
            this.dbgFilaDestino_OK.ReadOnly = true;
            this.dbgFilaDestino_OK.Size = new System.Drawing.Size(589, 122);
            this.dbgFilaDestino_OK.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "NOME";
            this.dataGridViewTextBoxColumn1.HeaderText = "Loja";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 160;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "email";
            this.dataGridViewTextBoxColumn2.HeaderText = "Email";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 170;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DataHoraEnviar";
            this.dataGridViewTextBoxColumn3.HeaderText = "Transmitir EM";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "status";
            this.dataGridViewTextBoxColumn4.HeaderText = "Status";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dbgEnvio_OK
            // 
            this.dbgEnvio_OK.AllowUserToAddRows = false;
            this.dbgEnvio_OK.AllowUserToDeleteRows = false;
            this.dbgEnvio_OK.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbgEnvio_OK.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dbgEnvio_OK.Location = new System.Drawing.Point(6, 12);
            this.dbgEnvio_OK.Name = "dbgEnvio_OK";
            this.dbgEnvio_OK.ReadOnly = true;
            this.dbgEnvio_OK.Size = new System.Drawing.Size(589, 133);
            this.dbgEnvio_OK.TabIndex = 3;
            this.dbgEnvio_OK.SelectionChanged += new System.EventHandler(this.dbgEnvio_OK_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "NomeModulo";
            this.dataGridViewTextBoxColumn5.HeaderText = "Tipo";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "IDRegistro";
            this.dataGridViewTextBoxColumn6.HeaderText = "Codigo";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 70;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Data";
            this.dataGridViewTextBoxColumn7.HeaderText = "Data";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "NomeArquivo";
            this.dataGridViewTextBoxColumn8.HeaderText = "Arquivo";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 260;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btAtualizar);
            this.panel2.Controls.Add(this.btFechar);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Location = new System.Drawing.Point(54, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 54);
            this.panel2.TabIndex = 4;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btFechar
            // 
            this.btFechar.BackColor = System.Drawing.Color.White;
            this.btFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btFechar.Location = new System.Drawing.Point(453, 3);
            this.btFechar.Name = "btFechar";
            this.btFechar.Size = new System.Drawing.Size(102, 46);
            this.btFechar.TabIndex = 0;
            this.btFechar.Text = "&Fechar";
            this.btFechar.UseVisualStyleBackColor = false;
            this.btFechar.Click += new System.EventHandler(this.btFechar_Click);
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(113, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(334, 27);
            this.label12.TabIndex = 0;
            this.label12.Text = "GERENCIAMENTO DA FILA DE ENVIO";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // btAtualizar
            // 
            this.btAtualizar.BackColor = System.Drawing.Color.White;
            this.btAtualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btAtualizar.Image = global::Transmissor.Properties.Resources.atualizar;
            this.btAtualizar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btAtualizar.Location = new System.Drawing.Point(3, 3);
            this.btAtualizar.Name = "btAtualizar";
            this.btAtualizar.Size = new System.Drawing.Size(102, 46);
            this.btAtualizar.TabIndex = 1;
            this.btAtualizar.Text = "&Atualizar";
            this.btAtualizar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btAtualizar.UseVisualStyleBackColor = false;
            this.btAtualizar.Click += new System.EventHandler(this.btAtualizar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::Transmissor.Properties.Resources.download;
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // frmEnviar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(633, 414);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEnviar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmEnviar_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dbgFilaDestino)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbgEnvio)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dbgFilaDestino_OK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbgEnvio_OK)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btFechar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dbgEnvio;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dbgFilaDestino;
        private System.Windows.Forms.Button btAtualizar;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Arquivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Loja;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transmitir_EM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dbgFilaDestino_OK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridView dbgEnvio_OK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    }
}