using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transmissor.code;
using TransmissorLog;
using TransmissorUtil;

namespace Transmissor
{
    public partial class frmPrincipal : Form
    {
        
        private bool LigandoServidores = false;
        private bool CarregandoInterface = false;
        private bool bServicoParadoPeloOperador=false;
        private frmLog _frmLog = null;
        public frmPrincipal()
        {
            InitializeComponent();
            
        }

        private void frmPrincipal_Load(object sender, EventArgs e) {
            CarregandoInterface = true;
            IniciarApp();
            CarregandoInterface = false;
        }
        private void IniciarApp()
        {
            Servico.POP_OK = false;
            Servico.Smtp_OK = false;
            
            if (!TransmissorApp.Iniciar())
            {


                Application.Exit();
            }

            if (!IniciarUI())
            {
                Application.Exit();
            }

            if (!btEnviar.Enabled && !btReceber.Enabled)
            {
                Log.Registrar("ERROR: TRANSMISSOR PARADO");
                txtStatus.Text = "STATUS: PARADO";
                btIniciar.Enabled = true;
                btParar.Enabled = false;
                //TransmissorApp.Encerrar();
            }
            else
            {
                Log.Registrar("INFO: TRANSMISSOR INICIADO COM SUCESSO");
                txtStatus.Text = "STATUS: INICIADO";
            }
            Minimizar();
        }

        

        private bool LigarServidores(bool POP, bool Smtp ) {

            try
            {
                //Log.Registrar("INFO: INICIANDO O TRANSMISSOR")
                if(POP){
                    txtStatus.Text = "STATUS: (RE)INICIANDO POP";
                    Application.DoEvents();
                    Log.Registrar("INFO: INICIANDO O SERVIDOR (POP)");

                    //btReceber.Enabled = 
                    TransmissorServicoPOP.Iniciar();
                    if (!btReceber.Enabled)
                    {
                        txtStatus.Text = "STATUS: POP PARADO";
                        Log.Registrar("ERRO: " + TransmissorServicoPOP.MensagemErro());
                    }
                    else {
                        Log.Registrar("INFO: -->SERVIDOR (POP) INICIADO COM SUCESSO");
                        Servico.POP_OK = true;
                        RecepcaoAutomatica.Iniciar();
                    }
                }
                

                if(Smtp){
                    txtStatus.Text = "STATUS: (RE)INICIANDO SMTP";
                    Application.DoEvents();    
                    Log.Registrar("INFO: INICIANDO O SERVIDOR (SMTP)");
                    //btEnviar.Enabled = 
                    TransmissorServico_SMTP.Iniciar();
                    if (!btEnviar.Enabled){
                        txtStatus.Text = "STATUS: SMTP PARADO";
                        Log.Registrar("ERRO: " + TransmissorServico_SMTP.MensagemErro());
                    }
                    else {
                        Log.Registrar("INFO: -->SERVIDOR (SMTP) INICIADO COM SUCESSO");
                        Servico.Smtp_OK = true;
                        EnvioAutomatico.Iniciar(); 
                        
                    }
                }

                if (!btEnviar.Enabled && !btReceber.Enabled){
                    Log.Registrar("ERROR: TRANSMISSOR PARADO");
                    txtStatus.Text = "STATUS: PARADO";
                    if (!bServicoParadoPeloOperador){
                        Servico.Smtp_OK = false;
                        Servico.POP_OK = false;
                    }                    
                }
                else {
                    btIniciar.Enabled = false;
                    btParar.Enabled = true;
                    txtStatus.Text = "STATUS: INICIADO";
                    //'Servico.POP_OK = true;
                    //Servico.Smtp_OK = true;
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " " + ex.StackTrace);
                Util.Mensagem(ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private bool DesLigarServidores()
        {

            try
            {
                Log.Registrar("INFO: INTERROMPENDO O TRANSMISSOR");
                TransmissorServicoPOP.Fechar();
                TransmissorServico_SMTP.Parar();
                EnvioAutomatico.Interromper();
                txtStatus.Text = "STATUS: PARADO";
                Log.Registrar("INFO: -->TRANSMISSOR INTERROMPIDO COM SUCESSO");

                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private bool IniciarUI() {
            try
            {

                lblLoja.Text="LOJA: " + TransmissorApp.dsLojas.Tables[0].Rows[0]["nome"].ToString();

                if (LigarServidores(true,true))
                {
                  btIniciar.Enabled = false;
                  btParar.Enabled = true; 
                }                               
                
                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private void btFechar_Click(object sender, EventArgs e)
        {
            if (Util.Mensagem("Deseja realmente encerra o transmissor?\nTODAS AS TRANSFERÊNCIAS SERÃO INTERROMPIDAS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                TransmissorApp.Encerrar();
                Application.Exit();
                //Application.ExitThread();
            }
        }
        private void Minimizar() {
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();            
            ICONE.Visible = true;
        }

        private void btMinimizar_Click(object sender, EventArgs e)
        {
            Minimizar();
            
        }

        private void btLog_Click(object sender, EventArgs e)
        {
           
            (new frmLog()).ShowDialog(this);
            
        }

        private void btConfigurar_Click(object sender, EventArgs e)
        {
            (new frmConfiguracao()).ShowDialog(this);
        }

        private void btParar_Click(object sender, EventArgs e)
        {
            Log.Registrar("INFO: USUARIO INTERROMPEU O TRANSMISSOR");
            DesLigarServidores();
            btIniciar.Enabled = true;
            btParar.Enabled = false;
            bServicoParadoPeloOperador = true;
        }

        private void btIniciar_Click(object sender, EventArgs e)
        {
            Log.Registrar("INFO: USUARIO (RE)INICIOU O TRANSMISSOR");
            LigarServidores(true,true);
           
        }

        private void lblNomeAPP_Click(object sender, EventArgs e)
        {

        }

        private void btEnviar_Click(object sender, EventArgs e)
        {
            
            (new frmEnviar()).ShowDialog();
            
        }

        private void VerificarServico_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            if (!Servico.Smtp_OK || !Servico.POP_OK)
            {
                 
                Application.DoEvents();
                if (!chkReinicio.Checked)
                {
                    txtStatus.Text = "AUTO-REINICIO PARADO";
                    return;
                }
            }
       

            if (LigandoServidores) {return;}

            if (CarregandoInterface){return;}
            
            if (!Servico.Smtp_OK){

                LigandoServidores = true;
                LigarServidores(false, true);
                LigandoServidores = false;
            }

            if (!Servico.POP_OK)
            {
                
                    LigandoServidores = true;
                    LigarServidores(true, false);
                    LigandoServidores = false;

                
                
            }
            else {

                if (Receber.RecebeuMsg_Movimento)
                {
                    RecepcaoAutomatica.Interromper();
                    
                        
                    Receber.Receptor_ReceberMovimentoAutomaticamento(TransmissorApp.nCodigoLoja);


                    RecepcaoAutomatica.Iniciar() ;
                }

                if (Receber.MensagensRecebida && !Receber.RecebendoTransferenciaRetaguarda)
                {
                    this.Visible = true;
                    this.ICONE.Visible = false;
                    this.WindowState = FormWindowState.Normal;
                    Receber.MensagensRecebida = false;
                    Receber.RecebendoTransferenciaRetaguarda = true;                    
                    (new frmReceber(true)).ShowDialog(this);
                    //Minimizar();                    
                } 
            }
            //ICONE.Text = txtStatus.Text;
            if (Log.LogOK)
            {
                if (Log.dtAtual != DateTime.Today) {
                    Log.Iniciar(DateTime.Today);
                }
            }
           
         }
        
        private void btReceber_Click(object sender, EventArgs e)
        {
            if (!Receber.RecebendoTransferenciaRetaguarda)
            {
                RecepcaoAutomatica.InterromperTemporariamente = true;
                EnvioAutomatico.InterromperTemporariamente = true;
                btReceber.Enabled = !btReceber.Enabled;
                (new frmReceber()).ShowDialog();
                btReceber.Enabled = !btReceber.Enabled;
                RecepcaoAutomatica.InterromperTemporariamente = false;
                EnvioAutomatico.InterromperTemporariamente = false;
            }            
        }

        private void frmPrincipal_Resize(object sender, EventArgs e)
        {
      
        }

        private void ICONE_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            ICONE.Visible = false;
            
        }

        private void chkReinicio_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ICONE_MouseMove(object sender, MouseEventArgs e)
        {
            Application.DoEvents();
            ICONE.Text = txtStatus.Text;
        }
    }
}
