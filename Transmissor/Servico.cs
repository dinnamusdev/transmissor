using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Transmissor.code;
using TransmissorLog;
using TransmissorUtil;

namespace Transmissor
{
    class Servico
    {
        
        public static bool Smtp_OK = false;
        public static bool POP_OK = false;
        private static bool Executar = true;
        private static bool ThreaOK =false;
        private static Thread VerificarServico = new Thread(new ThreadStart(Servico.Verificar));

        public static void IniciarVerificacao()
        {
            try
            {
                if (!ThreaOK)
                {
                    VerificarServico.Start();
                    while (!VerificarServico.IsAlive) ;
                }
                else {
                    VerificarServico.Resume();
                }
            }
            catch (Exception ex)
            {

                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           
            
        }

        private static void InterromperVerificacao()
        {
            try
            {
                VerificarServico.Suspend();
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
          
        }


        private static void TerminarVerificacao()
        {
            try
            {
                Executar = false;
                VerificarServico.Join();
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
          
           
        }


        private static void Verificar(){
            while (Executar)
            {
                Thread.Sleep(1000);
                try
                {
                    POP_OK = TransmissorServico_SMTP.Iniciar();
                    if (POP_OK)
                    {
                        InterromperVerificacao();
                    }
                }
                catch (Exception ex)
                {
                    Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                    Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            
        }

    }
}
