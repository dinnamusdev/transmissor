using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data;
using Transmissor.code;
using TransmissorLog;
using TransmissorUtil;

namespace Transmissor.code
{
    class RecepcaoAutomatica
    {
        public static bool InterromperTemporariamente = false;
        private static bool SolicitacaoDeInterrupcao=false;
        public static bool TarefaEmProcessamento = false;
        private static bool ThreadOK = false;
        private volatile static bool TarefaIniciada = false;
        public static Thread TarefaRecepcao = null;

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
                /*
                if (TarefaRecepcao.IsAlive)
                {
                    if (TarefaEmProcessamento)
                    {
                        Util.Mensagem("A tarefa esta em execução. Aguarde um momento...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        SolicitacaoDeInterrupcao = true;
                    }
                    else
                    {
                        TarefaRecepcao.Suspend();
                    }
                }*/
                SolicitacaoDeInterrupcao = true;
                
                //while (TarefaEnvio.IsAlive)

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
                Log.Registrar("INFO: INICIANDO TAREFA DE RECEPÇAO AUTOMATICA(POP)");

                SolicitacaoDeInterrupcao = false;
                if (!ThreadOK)
                {
                    TarefaIniciada = true;
                    TarefaRecepcao=new Thread(new ThreadStart(VerificarMensagens));                    
                    //TarefaEnvio.Name = "TAREFA DE ENVIO AUTOMATICO";
                    TarefaRecepcao.IsBackground = true;
                    TarefaRecepcao.Start();
                    while (!TarefaRecepcao.IsAlive) ;
                    ThreadOK = true;

                }
                
                Log.Registrar("INFO:  -->TAREFA DE RECEPÇÃO AUTOMATICA(POP) INICIADA COM SUCESSO");
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

                            if (InterromperTemporariamente)
                            {
                                TransmissorServicoPOP._Pop.NOOPConexao();
                            }

                            if (Servico.POP_OK && !SolicitacaoDeInterrupcao && !InterromperTemporariamente)
                            {

                                try
                                {
                                    TarefaEmProcessamento = true;

                                    //TransmissorServicoPOP._Pop.NOOPConexao();

                                    if (TransmissorServicoPOP._Pop.EstaConectado())
                                    {

                                        Receber.Receptor_VerificarMensagens(TransmissorApp.nCodigoLoja);

                                        String UltimaMensagemServidor = TransmissorServicoPOP._Pop.UltimaMensagemServidor();

                                        if (!UltimaMensagemServidor.Contains("OK"))
                                        {
                                            if (UltimaMensagemServidor != "")
                                            {
                                                nTentativasFrustradas++;
                                            }
                                            else
                                            {
                                                //TransmissorServicoPOP._Pop.NOOPConexao();     
                                                if (UltimaMensagemServidor != "")
                                                {
                                                    TransmissorServicoPOP.Iniciar(true);
                                                }

                                            }

                                        }
                                        else
                                        {
                                            if (TransmissorServicoPOP._Pop.MensagensExcluidas)
                                            {
                                                //TransmissorServicoPOP._Pop.Dispose();
                                                TransmissorServicoPOP.Iniciar(true);
                                                TransmissorServicoPOP._Pop.MensagensExcluidas = false;
                                                nTentativasFrustradas = 0;
                                            }
                                        }
                                        //TransmissorServicoPOP._Pop.ResetarConexao();
                                    }
                                    else
                                    {
                                        nTentativasFrustradas++;

                                    }
                                    if (nTentativasFrustradas > 2)
                                    {

                                        TransmissorServicoPOP._Pop.Dispose();
                                        Servico.POP_OK = false;
                                        nTentativasFrustradas = 0;
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
