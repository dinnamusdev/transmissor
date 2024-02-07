using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;
using TransmissorDAO;
using TransmissorLog;
using TransmissorUtil;

namespace Transmissor.code
{
    class TransmissorApp
    {

        
        public static DataSet dsLojas = new DataSet();
        public static Int16 nCodigoLoja = 0;
      
        public static void Encerrar() {
            Log.Registrar("INFO: TRANSMISSOR ENCERRADO PELO OPERADOR");

            EnvioAutomatico.Terminar();
            RecepcaoAutomatica.Terminar();

            if(TransmissorServico_SMTP.Conectado()) { TransmissorServico_SMTP.Parar(); }
            if (TransmissorServicoPOP.Conectado()) { TransmissorServicoPOP.Fechar(); }
            DAO.cnxDinnamuS.Dispose();
            DAO.cnxDinnamuS.Close();
            Log.Fechar();
        }

        public static bool Iniciar() {
            try
            {
                string processName = Process.GetCurrentProcess().ProcessName;
                Process[] instances = Process.GetProcessesByName(processName);

                if (instances.Length > 1)
                {
                    Util.Mensagem("O sistema já esta em execução", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (!Log.Iniciar(DateTime.Today))
                {
                    Util.Mensagem("Não foi possível iniciar o Log do Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (!DAO.IniciarCNX()) {
                    Util.Mensagem("Não foi possível iniciar a conexão com o servidor \n" + DAO.EMsg , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                try 
	            {	        
                    nCodigoLoja =  Int16.Parse( Registry.LocalMachine.OpenSubKey("SOFTWARE\\DINNAMUS\\CONFIG").GetValue("LojaAtiva").ToString());
		
	            }
	            catch (Exception ex)
	            {
		
		            Util.Mensagem("Não foi encontrado a chave de registro para a loja ativa" , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
	            }
                

                dsLojas= DAO.Pesquisar("select * from lojas where codigo=" + nCodigoLoja);

                if (dsLojas.Tables[0].Rows.Count==0)
	            {
		            Util.Mensagem("Os paramêtros de configuração do sistema não estão corretos[Loja Atual]" , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
	            }

               
             

                Log.Registrar("INFO: INICIANDO TRANSMISSOR....");

                bool bCFGOK = false;
                while (!bCFGOK)
                {
                    TransmissorCFG.dsConfiguracao = TransmissorCFG.CarregarCFG(nCodigoLoja);

                    if (TransmissorCFG.dsConfiguracao.Tables[0].Rows.Count == 0)
                    {
                        if (!TransmissorCFG.CFG_Iniciar(nCodigoLoja))
                        {
                            Util.Mensagem("Não foi possível iniciar a configuração da Loja", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                        else
                        {
                            TransmissorCFG.dsConfiguracao = TransmissorCFG.CarregarCFG(nCodigoLoja);
                            Util.Mensagem("A configuração da Loja ainda não foi definida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            (new frmConfiguracao()).ShowDialog();
                        }
                    }
                    else
                    {
                        Boolean bPOP_CFG_OK = false;
                        Boolean.TryParse(TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["pop_ok"].ToString(), out bPOP_CFG_OK);

                        Boolean bSMTP_CFG_OK = false;

                        Boolean.TryParse(TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["smtp_ok"].ToString(), out bSMTP_CFG_OK);

                        if (!bPOP_CFG_OK || !bSMTP_CFG_OK)
                        {
                            if (Util.Mensagem("O(s) Servidores ainda não foi(ram) conectado. \n\nDeseja Finalizar a configuração para iniciar o TRANSMISSOR ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                (new frmConfiguracao()).ShowDialog();
                            }
                            else {
                                return false;
                            }
                        }
                        bCFGOK = bPOP_CFG_OK && bSMTP_CFG_OK;                        
                    }
                }
                /*
                if (!TransmissorServico_SMTP.Iniciar())
                {
                    Log.Registrar("ERRO: Não foi possível iniciar Servidor de Envio(SMTP) : \n" + TransmissorServico_SMTP.MensagemErro());
                    Util.Mensagem("Não foi possível iniciar Servidor de Envio(SMTP) : \n" + TransmissorServico_SMTP.MensagemErro(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                if (!TransmissorServicoPOP.Iniciar())
                {
                    Log.Registrar("ERRO: Não foi possível iniciar Servidor de Recepção(POP) : \n" + TransmissorServicoPOP.MensagemErro());
                    Util.Mensagem("Não foi possível iniciar Servidor de Recepção(POP) : \n" + TransmissorServicoPOP.MensagemErro(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                */
                return true;
            }
            catch (Exception ex)
            {

                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

                return false;
            }
        
        }
    }
}
