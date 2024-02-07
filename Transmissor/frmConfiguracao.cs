using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transmissor.code;
using System.Data.SqlClient;
using TransmissorDAO;
using TransmissorUtil;
using TransmissorLog;
namespace Transmissor
{
    public partial class frmConfiguracao : Form
    {
        public frmConfiguracao()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            TransmissorCFG.dsConfiguracao.Tables[0].Rows[0].CancelEdit();
            this.Dispose();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private bool AtualizarDadosServidor() {
            try
            {
                bool bPOPConectar =false;
                bool bSMTPConectar =false;
                
                if(TransmissorServico_SMTP.Iniciar()){
                   Util.Mensagem("Servidor SMTP Conectado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information );    
                   bSMTPConectar=true;
                }else{
                    Util.Mensagem("Servidor SMTP não Conectado \n" + TransmissorServico_SMTP.MensagemErro(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                     
                }

                if(TransmissorServicoPOP.Iniciar()){
                   Util.Mensagem("Servidor POP Conectado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information );    
                   bPOPConectar=true;
                }else{
                    Util.Mensagem("Servidor POP não Conectado \n" + TransmissorServicoPOP.MensagemErro(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                     
                }

                TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["SMTP_OK"]= (bSMTPConectar ? 1 : 0);

                TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["POP_OK"]= (bPOPConectar ? 1 : 0);

                TransmissorCFG.dsConfiguracao.Tables[0].Rows[0].EndEdit();

                return (bPOPConectar && bSMTPConectar ? DAO.DataSetEditavel_Gravar(TransmissorCFG.dsConfiguracao, TransmissorCFG.daConfiguracao) : false);
                
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message); 
               Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               return false;
            }
        }
        private bool ValidarDados() {
            try
            {

                if (txtSMTP_Servidor.Text.Trim().Length == 0) {
                    Util.Mensagem("Informe o servidor SMTP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (txtSMTP_Porta.Text.Trim().Length == 0)
                {
                    Util.Mensagem("Informe a porta servidor SMTP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (txtSMTP_Usuario.Text.Trim().Length == 0)
                {
                    Util.Mensagem("Informe o usuario do servidor SMTP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (txtPOP_Servidor.Text.Trim().Length == 0)
                {
                    Util.Mensagem("Informe o servidor POP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (txtPOP_Porta.Text.Trim().Length == 0)
                {
                    Util.Mensagem("Informe a porta servidor POP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (txtPOP_Usuario.Text.Trim().Length == 0)
                {
                    Util.Mensagem("Informe o usuario do servidor POP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }


                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private void btGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarDados())
                {
                    if (Util.Mensagem("Confirma a gravação dos dados?",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==System.Windows.Forms.DialogResult.Yes)
                    {                    
                        if (AtualizarDadosServidor())
                        {
                           Util.Mensagem("Informações atualizadas com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information ); 
                        }else
	                    {
                           Util.Mensagem("Não foi possível Atualizar as informações", MessageBoxButtons.OK, MessageBoxIcon.Exclamation  ); 
	                    }
                    }
                }               
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool InicializarDbg(int nCodigoLoja) {
            try
            {

                dbgEmails.AutoGenerateColumns = false;
                dbgEmails.DataSource = TransmissorCFG.CFG_ListarEmail(nCodigoLoja).Tables[0];

                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private void frmConfiguracao_Load(object sender, EventArgs e)
        {
            try
            {
                TransmissorCFG.dsConfiguracao.Tables[0].Rows[0].BeginEdit();
                Util.Bind(this.Controls[0],TransmissorCFG.dsConfiguracao);
                cbLoja.DataSource = DAO.Pesquisar("select codigo,nome from lojas where codigo<>" + TransmissorApp.nCodigoLoja).Tables[0];
                cbLoja.DisplayMember = "nome";
                cbLoja.ValueMember = "codigo";
                InicializarDbg(TransmissorApp.nCodigoLoja);
                
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void btIncluirEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (txEMailPrincipal.Text.Trim().Length == 0) { return; }

                if (Util.Mensagem("Confirma a gravação dos dados?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {

                    int nLoja = 0;
                    
                    int.TryParse(cbLoja.SelectedValue.ToString(), out nLoja);


                    if(TransmissorCFG.CFG_Incluir_Email(TransmissorApp.nCodigoLoja,nLoja,txEMailPrincipal.Text,"")){
                        InicializarDbg(TransmissorApp.nCodigoLoja);
                        Util.Mensagem("Email Incluido com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information ); 
                    }else{
                        Util.Mensagem("Não foi possível incluir o email", MessageBoxButtons.OK, MessageBoxIcon.Exclamation  ); 
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btRemover_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable ds = (DataTable)dbgEmails.DataSource;
                if (ds.Rows.Count == 0) { return; }
                if (Util.Mensagem("Confirma a exclusão do email?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    int nLoja = 0;
                    int nLojaAtual = 0;
                    
                    int.TryParse( ds.Rows[dbgEmails.CurrentRow.Index]["codigo"].ToString(),out nLoja);

                    int.TryParse(ds.Rows[dbgEmails.CurrentRow.Index]["lojaatual"].ToString(), out nLojaAtual);

                    if (TransmissorCFG.CFG_Excluir_Email(nLojaAtual,nLoja))
                    {
                        InicializarDbg(TransmissorApp.nCodigoLoja);
                        Util.Mensagem("Email Excluido com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Util.Mensagem("Não foi possível excluir o email", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
