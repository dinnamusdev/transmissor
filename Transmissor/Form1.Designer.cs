namespace Transmissor
{
    partial class frmPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.lblLoja = new System.Windows.Forms.Label();
            this.lblNomeAPP = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btLog = new System.Windows.Forms.Button();
            this.btReceber = new System.Windows.Forms.Button();
            this.btEnviar = new System.Windows.Forms.Button();
            this.btIniciar = new System.Windows.Forms.Button();
            this.btParar = new System.Windows.Forms.Button();
            this.btConfigurar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btFechar = new System.Windows.Forms.Button();
            this.btMinimizar = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.VerificarServico = new System.Windows.Forms.Timer(this.components);
            this.ICONE = new System.Windows.Forms.NotifyIcon(this.components);
            this.chkReinicio = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLoja
            // 
            this.lblLoja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLoja.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoja.Location = new System.Drawing.Point(3, 39);
            this.lblLoja.Name = "lblLoja";
            this.lblLoja.Size = new System.Drawing.Size(290, 23);
            this.lblLoja.TabIndex = 1;
            this.lblLoja.Text = "LOJA:";
            this.lblLoja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNomeAPP
            // 
            this.lblNomeAPP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblNomeAPP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNomeAPP.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeAPP.Location = new System.Drawing.Point(0, 0);
            this.lblNomeAPP.Name = "lblNomeAPP";
            this.lblNomeAPP.Size = new System.Drawing.Size(601, 31);
            this.lblNomeAPP.TabIndex = 2;
            this.lblNomeAPP.Text = "TRANSMISSOR";
            this.lblNomeAPP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblNomeAPP.Click += new System.EventHandler(this.lblNomeAPP_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btLog);
            this.panel1.Controls.Add(this.btReceber);
            this.panel1.Controls.Add(this.btEnviar);
            this.panel1.Controls.Add(this.btIniciar);
            this.panel1.Controls.Add(this.btParar);
            this.panel1.Controls.Add(this.btConfigurar);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btFechar);
            this.panel1.Controls.Add(this.btMinimizar);
            this.panel1.Controls.Add(this.txtStatus);
            this.panel1.Controls.Add(this.lblNomeAPP);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblLoja);
            this.panel1.Controls.Add(this.chkReinicio);
            this.panel1.Location = new System.Drawing.Point(3, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(605, 172);
            this.panel1.TabIndex = 3;
            // 
            // btLog
            // 
            this.btLog.BackColor = System.Drawing.Color.White;
            this.btLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLog.Image = global::Transmissor.Properties.Resources.log1;
            this.btLog.Location = new System.Drawing.Point(523, 72);
            this.btLog.Name = "btLog";
            this.btLog.Size = new System.Drawing.Size(75, 91);
            this.btLog.TabIndex = 12;
            this.btLog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btLog.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btLog.UseVisualStyleBackColor = false;
            this.btLog.Click += new System.EventHandler(this.btLog_Click);
            // 
            // btReceber
            // 
            this.btReceber.AutoSize = true;
            this.btReceber.BackColor = System.Drawing.Color.White;
            this.btReceber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btReceber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btReceber.Image = global::Transmissor.Properties.Resources.RECEBER_ARQ;
            this.btReceber.Location = new System.Drawing.Point(446, 72);
            this.btReceber.Name = "btReceber";
            this.btReceber.Size = new System.Drawing.Size(76, 91);
            this.btReceber.TabIndex = 11;
            this.btReceber.Text = "&Receber";
            this.btReceber.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btReceber.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btReceber.UseVisualStyleBackColor = false;
            this.btReceber.Click += new System.EventHandler(this.btReceber_Click);
            // 
            // btEnviar
            // 
            this.btEnviar.AutoSize = true;
            this.btEnviar.BackColor = System.Drawing.Color.White;
            this.btEnviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btEnviar.Image = global::Transmissor.Properties.Resources.download;
            this.btEnviar.Location = new System.Drawing.Point(369, 72);
            this.btEnviar.Name = "btEnviar";
            this.btEnviar.Size = new System.Drawing.Size(76, 91);
            this.btEnviar.TabIndex = 10;
            this.btEnviar.Text = "&Enviar";
            this.btEnviar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btEnviar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btEnviar.UseVisualStyleBackColor = false;
            this.btEnviar.Click += new System.EventHandler(this.btEnviar_Click);
            // 
            // btIniciar
            // 
            this.btIniciar.AutoSize = true;
            this.btIniciar.BackColor = System.Drawing.Color.White;
            this.btIniciar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btIniciar.Enabled = false;
            this.btIniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btIniciar.Image = global::Transmissor.Properties.Resources.INICIAR;
            this.btIniciar.Location = new System.Drawing.Point(293, 72);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(76, 91);
            this.btIniciar.TabIndex = 9;
            this.btIniciar.Text = "&Iniciar";
            this.btIniciar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btIniciar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btIniciar.UseVisualStyleBackColor = false;
            this.btIniciar.Click += new System.EventHandler(this.btIniciar_Click);
            // 
            // btParar
            // 
            this.btParar.AutoSize = true;
            this.btParar.BackColor = System.Drawing.Color.White;
            this.btParar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btParar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btParar.Image = global::Transmissor.Properties.Resources.DESLIGAR;
            this.btParar.Location = new System.Drawing.Point(217, 72);
            this.btParar.Name = "btParar";
            this.btParar.Size = new System.Drawing.Size(76, 91);
            this.btParar.TabIndex = 8;
            this.btParar.Text = "&Parar";
            this.btParar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btParar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btParar.UseVisualStyleBackColor = false;
            this.btParar.Click += new System.EventHandler(this.btParar_Click);
            // 
            // btConfigurar
            // 
            this.btConfigurar.AutoSize = true;
            this.btConfigurar.BackColor = System.Drawing.Color.White;
            this.btConfigurar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btConfigurar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btConfigurar.Image = global::Transmissor.Properties.Resources.configurar;
            this.btConfigurar.Location = new System.Drawing.Point(143, 72);
            this.btConfigurar.Name = "btConfigurar";
            this.btConfigurar.Size = new System.Drawing.Size(74, 91);
            this.btConfigurar.TabIndex = 6;
            this.btConfigurar.Text = "&Configurar";
            this.btConfigurar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btConfigurar.UseVisualStyleBackColor = false;
            this.btConfigurar.Click += new System.EventHandler(this.btConfigurar_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(138, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(463, 103);
            this.label3.TabIndex = 7;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btFechar
            // 
            this.btFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btFechar.Location = new System.Drawing.Point(572, 2);
            this.btFechar.Name = "btFechar";
            this.btFechar.Size = new System.Drawing.Size(25, 28);
            this.btFechar.TabIndex = 5;
            this.btFechar.Text = "X";
            this.btFechar.UseVisualStyleBackColor = true;
            this.btFechar.Click += new System.EventHandler(this.btFechar_Click);
            // 
            // btMinimizar
            // 
            this.btMinimizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btMinimizar.Location = new System.Drawing.Point(548, 2);
            this.btMinimizar.Name = "btMinimizar";
            this.btMinimizar.Size = new System.Drawing.Size(25, 28);
            this.btMinimizar.TabIndex = 4;
            this.btMinimizar.Text = "-";
            this.btMinimizar.UseVisualStyleBackColor = true;
            this.btMinimizar.Click += new System.EventHandler(this.btMinimizar_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStatus.Location = new System.Drawing.Point(293, 39);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(229, 23);
            this.txtStatus.TabIndex = 3;
            this.txtStatus.Text = "STATUS: INICIANDO..";
            this.txtStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::Transmissor.Properties.Resources.integração;
            this.pictureBox1.Location = new System.Drawing.Point(3, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 103);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // VerificarServico
            // 
            this.VerificarServico.Enabled = true;
            this.VerificarServico.Interval = 5000;
            this.VerificarServico.Tick += new System.EventHandler(this.VerificarServico_Tick);
            // 
            // ICONE
            // 
            this.ICONE.Icon = ((System.Drawing.Icon)(resources.GetObject("ICONE.Icon")));
            this.ICONE.Text = "TRANSMISSOR DINNAMUS";
            this.ICONE.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ICONE_MouseDoubleClick);
            this.ICONE.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ICONE_MouseMove);
            // 
            // chkReinicio
            // 
            this.chkReinicio.AutoSize = true;
            this.chkReinicio.Checked = true;
            this.chkReinicio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReinicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReinicio.Location = new System.Drawing.Point(526, 43);
            this.chkReinicio.Name = "chkReinicio";
            this.chkReinicio.Size = new System.Drawing.Size(71, 17);
            this.chkReinicio.TabIndex = 13;
            this.chkReinicio.Text = "Reiniciar?";
            this.chkReinicio.UseVisualStyleBackColor = true;
            this.chkReinicio.CheckedChanged += new System.EventHandler(this.chkReinicio_CheckedChanged);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(615, 188);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "Transmissor";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.Resize += new System.EventHandler(this.frmPrincipal_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblLoja;
        private System.Windows.Forms.Label lblNomeAPP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btConfigurar;
        private System.Windows.Forms.Button btFechar;
        private System.Windows.Forms.Button btMinimizar;
        private System.Windows.Forms.Label txtStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btParar;
        private System.Windows.Forms.Button btIniciar;
        private System.Windows.Forms.Button btEnviar;
        private System.Windows.Forms.Button btReceber;
        private System.Windows.Forms.Button btLog;
        private System.Windows.Forms.Timer VerificarServico;
        private System.Windows.Forms.NotifyIcon ICONE;
        private System.Windows.Forms.CheckBox chkReinicio;
    }
}

