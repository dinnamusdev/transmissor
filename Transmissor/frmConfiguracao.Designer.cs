namespace Transmissor
{
    partial class frmConfiguracao
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btGravar = new System.Windows.Forms.Button();
            this.btFechar = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btRemover = new System.Windows.Forms.Button();
            this.btIncluirEmail = new System.Windows.Forms.Button();
            this.dbgEmails = new System.Windows.Forms.DataGridView();
            this.txEMailPrincipal = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbLoja = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkPOP_Ok = new System.Windows.Forms.CheckBox();
            this.txtPOP_Servidor = new System.Windows.Forms.TextBox();
            this.chkPOP_SSL = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPOP_Senha = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPOP_Porta = new System.Windows.Forms.TextBox();
            this.txtPOP_Usuario = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkSMTP_Ok = new System.Windows.Forms.CheckBox();
            this.chkSMTP_AUTENTICAR = new System.Windows.Forms.CheckBox();
            this.txtSMTP_Servidor = new System.Windows.Forms.TextBox();
            this.chkSMTP_SSL = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSMTP_Senha = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSMTP_Porta = new System.Windows.Forms.TextBox();
            this.txtSMTP_Usuario = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailPrincipal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbgEmails)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::Transmissor.Properties.Resources.configurar;
            this.pictureBox1.Location = new System.Drawing.Point(9, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btGravar);
            this.panel1.Controls.Add(this.btFechar);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Location = new System.Drawing.Point(58, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(504, 54);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btGravar
            // 
            this.btGravar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGravar.Location = new System.Drawing.Point(292, 11);
            this.btGravar.Name = "btGravar";
            this.btGravar.Size = new System.Drawing.Size(102, 27);
            this.btGravar.TabIndex = 0;
            this.btGravar.Text = "&Gravar";
            this.btGravar.UseVisualStyleBackColor = true;
            this.btGravar.Click += new System.EventHandler(this.btGravar_Click);
            // 
            // btFechar
            // 
            this.btFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btFechar.Location = new System.Drawing.Point(395, 11);
            this.btFechar.Name = "btFechar";
            this.btFechar.Size = new System.Drawing.Size(102, 27);
            this.btFechar.TabIndex = 0;
            this.btFechar.Text = "&Fechar";
            this.btFechar.UseVisualStyleBackColor = true;
            this.btFechar.Click += new System.EventHandler(this.btFechar_Click);
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(5, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(282, 27);
            this.label12.TabIndex = 0;
            this.label12.Text = "CONFIGURAÇÃO";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(5, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(571, 436);
            this.panel2.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btRemover);
            this.groupBox3.Controls.Add(this.btIncluirEmail);
            this.groupBox3.Controls.Add(this.dbgEmails);
            this.groupBox3.Controls.Add(this.txEMailPrincipal);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cbLoja);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(7, 251);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(555, 169);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Email das Lojas";
            // 
            // btRemover
            // 
            this.btRemover.Location = new System.Drawing.Point(7, 144);
            this.btRemover.Name = "btRemover";
            this.btRemover.Size = new System.Drawing.Size(68, 23);
            this.btRemover.TabIndex = 10;
            this.btRemover.Text = "&Remover";
            this.btRemover.UseVisualStyleBackColor = true;
            this.btRemover.Click += new System.EventHandler(this.btRemover_Click);
            // 
            // btIncluirEmail
            // 
            this.btIncluirEmail.Location = new System.Drawing.Point(498, 37);
            this.btIncluirEmail.Name = "btIncluirEmail";
            this.btIncluirEmail.Size = new System.Drawing.Size(53, 21);
            this.btIncluirEmail.TabIndex = 9;
            this.btIncluirEmail.Text = "&Incluir";
            this.btIncluirEmail.UseVisualStyleBackColor = true;
            this.btIncluirEmail.Click += new System.EventHandler(this.btIncluirEmail_Click);
            // 
            // dbgEmails
            // 
            this.dbgEmails.AllowUserToAddRows = false;
            this.dbgEmails.AllowUserToDeleteRows = false;
            this.dbgEmails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dbgEmails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nome,
            this.EmailPrincipal});
            this.dbgEmails.Location = new System.Drawing.Point(8, 64);
            this.dbgEmails.MultiSelect = false;
            this.dbgEmails.Name = "dbgEmails";
            this.dbgEmails.ReadOnly = true;
            this.dbgEmails.Size = new System.Drawing.Size(541, 79);
            this.dbgEmails.TabIndex = 8;
            // 
            // txEMailPrincipal
            // 
            this.txEMailPrincipal.Location = new System.Drawing.Point(256, 38);
            this.txEMailPrincipal.Name = "txEMailPrincipal";
            this.txEMailPrincipal.Size = new System.Drawing.Size(241, 20);
            this.txEMailPrincipal.TabIndex = 5;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Location = new System.Drawing.Point(256, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 15);
            this.label10.TabIndex = 4;
            this.label10.Text = "Email Principal";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // cbLoja
            // 
            this.cbLoja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoja.FormattingEnabled = true;
            this.cbLoja.Location = new System.Drawing.Point(8, 38);
            this.cbLoja.Name = "cbLoja";
            this.cbLoja.Size = new System.Drawing.Size(247, 21);
            this.cbLoja.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Location = new System.Drawing.Point(8, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "Loja";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.txtPOP_Servidor);
            this.groupBox2.Controls.Add(this.chkPOP_SSL);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtPOP_Senha);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtPOP_Porta);
            this.groupBox2.Controls.Add(this.txtPOP_Usuario);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(287, 72);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(275, 156);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Servidor Recepção (POP)";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkPOP_Ok);
            this.groupBox4.Location = new System.Drawing.Point(142, 110);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(125, 39);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Status Conexão";
            // 
            // chkPOP_Ok
            // 
            this.chkPOP_Ok.AutoSize = true;
            this.chkPOP_Ok.Location = new System.Drawing.Point(23, 16);
            this.chkPOP_Ok.Name = "chkPOP_Ok";
            this.chkPOP_Ok.Size = new System.Drawing.Size(84, 17);
            this.chkPOP_Ok.TabIndex = 11;
            this.chkPOP_Ok.Text = "Conectado?";
            this.chkPOP_Ok.UseVisualStyleBackColor = true;
            // 
            // txtPOP_Servidor
            // 
            this.txtPOP_Servidor.Location = new System.Drawing.Point(6, 39);
            this.txtPOP_Servidor.Name = "txtPOP_Servidor";
            this.txtPOP_Servidor.Size = new System.Drawing.Size(165, 20);
            this.txtPOP_Servidor.TabIndex = 1;
            // 
            // chkPOP_SSL
            // 
            this.chkPOP_SSL.AutoSize = true;
            this.chkPOP_SSL.Location = new System.Drawing.Point(9, 115);
            this.chkPOP_SSL.Name = "chkPOP_SSL";
            this.chkPOP_SSL.Size = new System.Drawing.Size(120, 17);
            this.chkPOP_SSL.TabIndex = 8;
            this.chkPOP_SSL.Text = "Exige Conexão SSL";
            this.chkPOP_SSL.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Servidor";
            // 
            // txtPOP_Senha
            // 
            this.txtPOP_Senha.Location = new System.Drawing.Point(177, 83);
            this.txtPOP_Senha.Name = "txtPOP_Senha";
            this.txtPOP_Senha.PasswordChar = '@';
            this.txtPOP_Senha.Size = new System.Drawing.Size(87, 20);
            this.txtPOP_Senha.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Location = new System.Drawing.Point(177, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "Senha";
            // 
            // txtPOP_Porta
            // 
            this.txtPOP_Porta.Location = new System.Drawing.Point(177, 39);
            this.txtPOP_Porta.Name = "txtPOP_Porta";
            this.txtPOP_Porta.Size = new System.Drawing.Size(87, 20);
            this.txtPOP_Porta.TabIndex = 3;
            // 
            // txtPOP_Usuario
            // 
            this.txtPOP_Usuario.Location = new System.Drawing.Point(7, 83);
            this.txtPOP_Usuario.Name = "txtPOP_Usuario";
            this.txtPOP_Usuario.Size = new System.Drawing.Size(164, 20);
            this.txtPOP_Usuario.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Location = new System.Drawing.Point(7, 67);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Usuário";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Location = new System.Drawing.Point(177, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 15);
            this.label8.TabIndex = 2;
            this.label8.Text = "Porta";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.chkSMTP_AUTENTICAR);
            this.groupBox1.Controls.Add(this.txtSMTP_Servidor);
            this.groupBox1.Controls.Add(this.chkSMTP_SSL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSMTP_Senha);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSMTP_Porta);
            this.groupBox1.Controls.Add(this.txtSMTP_Usuario);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(7, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 172);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Servidor Envio (SMTP)";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkSMTP_Ok);
            this.groupBox5.Location = new System.Drawing.Point(75, 126);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(125, 39);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Status Conexão";
            // 
            // chkSMTP_Ok
            // 
            this.chkSMTP_Ok.AutoSize = true;
            this.chkSMTP_Ok.Location = new System.Drawing.Point(20, 15);
            this.chkSMTP_Ok.Name = "chkSMTP_Ok";
            this.chkSMTP_Ok.Size = new System.Drawing.Size(84, 17);
            this.chkSMTP_Ok.TabIndex = 10;
            this.chkSMTP_Ok.Text = "Conectado?";
            this.chkSMTP_Ok.UseVisualStyleBackColor = true;
            // 
            // chkSMTP_AUTENTICAR
            // 
            this.chkSMTP_AUTENTICAR.AutoSize = true;
            this.chkSMTP_AUTENTICAR.Location = new System.Drawing.Point(177, 107);
            this.chkSMTP_AUTENTICAR.Name = "chkSMTP_AUTENTICAR";
            this.chkSMTP_AUTENTICAR.Size = new System.Drawing.Size(80, 17);
            this.chkSMTP_AUTENTICAR.TabIndex = 8;
            this.chkSMTP_AUTENTICAR.Text = "Autenticar?";
            this.chkSMTP_AUTENTICAR.UseVisualStyleBackColor = true;
            // 
            // txtSMTP_Servidor
            // 
            this.txtSMTP_Servidor.Location = new System.Drawing.Point(6, 39);
            this.txtSMTP_Servidor.Name = "txtSMTP_Servidor";
            this.txtSMTP_Servidor.Size = new System.Drawing.Size(165, 20);
            this.txtSMTP_Servidor.TabIndex = 1;
            // 
            // chkSMTP_SSL
            // 
            this.chkSMTP_SSL.AutoSize = true;
            this.chkSMTP_SSL.Location = new System.Drawing.Point(8, 107);
            this.chkSMTP_SSL.Name = "chkSMTP_SSL";
            this.chkSMTP_SSL.Size = new System.Drawing.Size(120, 17);
            this.chkSMTP_SSL.TabIndex = 8;
            this.chkSMTP_SSL.Text = "Exige Conexão SSL";
            this.chkSMTP_SSL.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Servidor";
            // 
            // txtSMTP_Senha
            // 
            this.txtSMTP_Senha.Location = new System.Drawing.Point(177, 81);
            this.txtSMTP_Senha.Name = "txtSMTP_Senha";
            this.txtSMTP_Senha.PasswordChar = '@';
            this.txtSMTP_Senha.Size = new System.Drawing.Size(87, 20);
            this.txtSMTP_Senha.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Location = new System.Drawing.Point(177, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Senha";
            // 
            // txtSMTP_Porta
            // 
            this.txtSMTP_Porta.Location = new System.Drawing.Point(177, 39);
            this.txtSMTP_Porta.Name = "txtSMTP_Porta";
            this.txtSMTP_Porta.Size = new System.Drawing.Size(87, 20);
            this.txtSMTP_Porta.TabIndex = 3;
            // 
            // txtSMTP_Usuario
            // 
            this.txtSMTP_Usuario.Location = new System.Drawing.Point(7, 81);
            this.txtSMTP_Usuario.Name = "txtSMTP_Usuario";
            this.txtSMTP_Usuario.Size = new System.Drawing.Size(164, 20);
            this.txtSMTP_Usuario.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(7, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Usuário";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(177, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Porta";
            // 
            // Nome
            // 
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Loja";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 220;
            // 
            // EmailPrincipal
            // 
            this.EmailPrincipal.DataPropertyName = "EmailPrincipal";
            this.EmailPrincipal.HeaderText = "Email";
            this.EmailPrincipal.Name = "EmailPrincipal";
            this.EmailPrincipal.ReadOnly = true;
            this.EmailPrincipal.Width = 250;
            // 
            // frmConfiguracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(583, 452);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConfiguracao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmConfiguracao";
            this.Load += new System.EventHandler(this.frmConfiguracao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbgEmails)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btGravar;
        private System.Windows.Forms.Button btFechar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSMTP_Servidor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSMTP_AUTENTICAR;
        private System.Windows.Forms.CheckBox chkSMTP_SSL;
        private System.Windows.Forms.TextBox txtSMTP_Senha;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSMTP_Usuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSMTP_Porta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txEMailPrincipal;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbLoja;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPOP_Servidor;
        private System.Windows.Forms.CheckBox chkPOP_SSL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPOP_Senha;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPOP_Porta;
        private System.Windows.Forms.TextBox txtPOP_Usuario;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btRemover;
        private System.Windows.Forms.Button btIncluirEmail;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkPOP_Ok;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkSMTP_Ok;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailPrincipal;
        private System.Windows.Forms.DataGridView dbgEmails;
    }
}