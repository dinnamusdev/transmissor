using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transmissor.code;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using TransmissorUtil;
using TransmissorLog;
using SincronizadorDeTabelas;

namespace Transmissor
{
    class EnvioAutomatico
    {

        public static bool InterromperTemporariamente = false;
        private static bool SolicitacaoDeInterrupcao=false;
        public static bool TarefaEmProcessamento = false;
        private static bool ThreadOK = false;
        private volatile static bool TarefaIniciada = false;
        private static Thread TarefaEnvio = null;

        public static void Terminar() {
            try
            {
                if (TarefaIniciada)
                {
                    TarefaIniciada = false;
                    //Thread.Sleep(1);                 
                    //TarefaEnvio.Join();
                }               
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }                                              
        }
        public static bool Interromper() {
            try
            {

                Log.Registrar("INFO: INTERROMPENDO TAREFA DE ENVIO AUTOMATICO(SMTP)");

                //TarefaIniciada = false;
                //if (TarefaEnvio.IsAlive)
                //{
                    SolicitacaoDeInterrupcao = true;

                    //if (TarefaEmProcessamento)
                    //{
                    //    Util.Mensagem("A tarefa esta em execução. Aguarde um momento...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    
                   // }
                    //else
                   // {
                    //    TarefaIniciada = false;//TarefaEnvio.Suspend();
                    //}
                //}
               
                Log.Registrar("INFO: -->TAREFA DE ENVIO AUTOMATICO(SMTP) INTERROMPIDA COM SUCESSO");

                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                //Util.Mensagem(ex.Message.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        
        }
        public static bool Iniciar() {
            try
            {
          //

                Log.Registrar("INFO: INICIANDO TAREFA DE ENVIO AUTOMATICO(SMTP)");

                SolicitacaoDeInterrupcao = false;
                if (!ThreadOK)
                {
                    
                    TarefaIniciada = true;
                    TarefaEnvio = new Thread(new ThreadStart(VerificarMensagens));
                    //TarefaEnvio.Name = "TAREFA DE ENVIO AUTOMATICO";
                    TarefaEnvio.IsBackground = true;
                    TarefaEnvio.Start();
                    while (!TarefaEnvio.IsAlive) ;
                    ThreadOK = true;

                }
               
                Log.Registrar("INFO:  -->TAREFA DE ENVIO AUTOMATICO(SMTP) INICIADA COM SUCESSO");
                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                 Util.Mensagem(ex.Message.ToString(),MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                 return false;
            }
        }

        private static void VerificarMensagens()
        {
                       
                try
                {
                    int nTentativasFrustradas = 0;
                    DataSet dsArquivos = new DataSet();
                    
                    while (TarefaIniciada)
                    {
                        Thread.Sleep(5000);
                        if (!SolicitacaoDeInterrupcao)
                        {

                            if (Servico.Smtp_OK && !SolicitacaoDeInterrupcao && !InterromperTemporariamente)
                            {
                                try
                                {
                                    TarefaEmProcessamento = true;
                                    if (TransmissorServico_SMTP.Conectado())
                                    {
                                        //SolicitacaoEnvio //Solicitacoes(TransmissorApp.nCodigoLoja);

                                        SolicitacaoEnvio.Verificar(Int16.Parse(TransmissorApp.nCodigoLoja.ToString()));

                                        dsArquivos = Envio.ListarFilaEnvio("", TransmissorApp.nCodigoLoja);
                                        if (dsArquivos.Tables[0].Rows.Count > 0)
                                        {
                                            Int32 nIdArquivo = Int32.Parse(dsArquivos.Tables[0].Rows[0]["id"].ToString());
                                            Log.Registrar("INFO: Enviando Arquivo Id :[" + nIdArquivo + "] ");
                                            if (Envio.EnviarArquivo(nIdArquivo))
                                            {
                                                Log.Registrar("INFO: Arquivo Id :[" + nIdArquivo + "] enviado com sucesso ");
                                                nTentativasFrustradas = 0;
                                            }
                                            else
                                            {
                                                Log.Registrar("ERRO: Falha ao enviar Arquivo Id :[" + nIdArquivo + "] - " + TransmissorServico_SMTP.MensagemErro());
                                                nTentativasFrustradas = nTentativasFrustradas + 1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        nTentativasFrustradas++;
                                    }

                                    if (nTentativasFrustradas >= 2)
                                    {
                                        Log.Registrar("ERRO: Sem conexão com o servidor " + TransmissorServico_SMTP.MensagemErro());
                                        nTentativasFrustradas = 0;
                                        Servico.Smtp_OK = false;

                                    }

                                    TarefaEmProcessamento = false;
                                }
                                catch (Exception ex)
                                {
                                    Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                                    Util.Mensagem(ex.Message.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                    Util.Mensagem(ex.Message.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            
    }
}
