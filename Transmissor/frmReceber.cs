using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transmissor.code;
using System.Diagnostics;
using TransmissorUtil;
using TransmissorLog;
using Transmissor.code.SincronizarTabelas.Recepcao;

namespace Transmissor
{
    public partial class frmReceber : Form
    {
        private String ParametrosDinOutros ="|#|YNTSAU|#|0|#||#||#|40|#|0|#||#|N|#|DESENVOLVEDOR|#||#||#|"+ TransmissorApp.nCodigoLoja +"|#|Gerente|#|ReceberEstoqueMatriz@"+  Receber.PastaRecepcao.Replace(".",Util.DiretorioAPP())   +"|#|0";
        private bool MsgRecebida = false;
        public frmReceber(bool MsgRecebida)
        {
            this.MsgRecebida = MsgRecebida;
            InitializeComponent();
        }

        public frmReceber()
        {
            InitializeComponent();
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private bool AtualizarGrid_Movimento_OK() {
            try
            {
                dbgRecebimentoMovimento_ok.AutoGenerateColumns = false;
                DataSet dsMovimentoOK = Receber.Receptor_ArquivosMovimento_ERP_OK(TransmissorApp.nCodigoLoja, 0, "");
                if (dsMovimentoOK != null)
                {
                    dbgRecebimentoMovimento_ok.DataSource = dsMovimentoOK.Tables[0];
                }
                else
                {
                    return false;
                }
                return false;
            }
            catch (Exception ex)
            {
                
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private bool AtualizarGrid_Movimento_Pendente() {
            try
            {
                dbgRecebimentoMovimento.AutoGenerateColumns = false;
                DataSet dsMovimento = Receber.Receptor_ArquivosMovimento(TransmissorApp.nCodigoLoja, 0, "");
                if (dsMovimento!=null)
                {
                    dbgRecebimentoMovimento.DataSource = dsMovimento.Tables[0];
                }
                else
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private bool IniciarUI() {
            try
            {
                if (MsgRecebida)
                {
                    PainelMsg.Visible = true;
                    timerAviso.Enabled = true;
                }
                DataSet ds = Receber.Receptor_ArquivosRecebidos(TransmissorApp.nCodigoLoja,0,"");
                if (ds!=null)
                {                    
                 
                    dbgRecebimento.AutoGenerateColumns = false;
                    dbgRecebimento.DataSource = ds.Tables[0];
                    
                }
                else
                {
                    return false;
                }

                DataSet dsRecebidos = Receber.Receptor_ArquivosRecebidos_OK(TransmissorApp.nCodigoLoja, 0, "Recebido");
                if (dsRecebidos != null)
                {

                    dbgRecebimentoOK.AutoGenerateColumns = false;
                    dbgRecebimentoOK.DataSource = dsRecebidos.Tables[0];
                   
                }
                else
                {
                    return false;
                }

                AtualizarGrid_Movimento_Pendente();
                AtualizarGrid_Movimento_OK();
                               
                tabControl1.SelectedIndex = 0;
                sstab1.SelectedIndex = 0;
                return true;
                
                
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void btAtualizar_Click(object sender, EventArgs e)
        {
            IniciarUI();
        }

        private void frmReceber_Load(object sender, EventArgs e)
        {
            if (!IniciarUI()) {
                this.Dispose();
            }
        }

        private void btFecharAviso_Click(object sender, EventArgs e)
        {
            PainelMsg.Visible = false;
            MsgRecebida = false;
            timerAviso.Enabled = false;
        }

        private void timerAviso_Tick(object sender, EventArgs e)
        {

            //if (Receber.MensagensRecebida)
            //{
                lblMSG.ForeColor = (lblMSG.ForeColor == Color.Red ? Color.Yellow : Color.Red); 
            //}
        }

        private void btReceber_Click(object sender, EventArgs e)
        {
           
        }

        private bool ReceberTransferencia() {
            try
            {
                List<Int32> lsTransf = new List<Int32>();                
                DataTable dt = (DataTable)dbgRecebimento.DataSource;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    DataGridViewCheckBoxCell chkselecionado = (DataGridViewCheckBoxCell)dbgRecebimento.Rows[i].Cells["selecionar"];
                    if ((bool) chkselecionado.FormattedValue==true)
                    {
                        lsTransf.Add(Int32.Parse(dr["idRegistro"].ToString()));
                    }

                }
                if (lsTransf.Count>0)
                {
                    if (Receber.Receptor_GravarTransferencia(TransmissorApp.nCodigoLoja, lsTransf)) {
                        Process proc = new Process();
                        proc.StartInfo.FileName = Util.DiretorioAPP()+"\\dinoutros.exe";
                        proc.StartInfo.Arguments = ParametrosDinOutros;                        
                        proc.Start();
                        proc.WaitForExit();
                        IniciarUI();
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void frmReceber_Deactivate(object sender, EventArgs e)
        {
            Receber.RecebendoTransferenciaRetaguarda = false;
        }

        private void btReceber_Click_1(object sender, EventArgs e)
        {
            try
            {
                ReceberTransferencia();                   
                
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool ReceberMovimento()
        {
            try
            {
                List<Int32> lsTransf = new List<Int32>();
                DataTable dt = (DataTable)dbgRecebimentoMovimento.DataSource;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    DataGridViewCheckBoxCell chkselecionado = (DataGridViewCheckBoxCell)dbgRecebimentoMovimento.Rows[i].Cells["SelecionarMov"];
                    if ((bool)chkselecionado.FormattedValue == true)
                    {
                        lsTransf.Add(Int32.Parse(dr["idRegistro"].ToString()));
                    }

                }
                if (lsTransf.Count > 0)
                {
                    bool bRet= ReceberMetodosBasicos.Sincronismo_ReceberArquivo(lsTransf,(Int16) TransmissorApp.nCodigoLoja);
                    if (bRet)
                    {

                        AtualizarGrid_Movimento_Pendente();
                        AtualizarGrid_Movimento_OK();
                        Util.Mensagem("Arquivo[s] recebido[s] com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Util.Mensagem("Não foi possível receber o Arquivo[s] recebido[s]\n" + ReceberMetodosBasicos.MSG, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private void btReceberMovimento_Click(object sender, EventArgs e)
        {
            ReceberMovimento();
        }

        private void btFecharAviso_Click_1(object sender, EventArgs e)
        {
            PainelMsg.Visible = false;
            MsgRecebida = false;
            timerAviso.Enabled = false;
        }
    }

}
