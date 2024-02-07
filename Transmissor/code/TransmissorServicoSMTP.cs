using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MailDinnamuS;
using System.Windows.Forms;
using System.Net.Mail;
using TransmissorLog;
using TransmissorUtil;

namespace Transmissor.code
{
    class TransmissorServico_SMTP
    {

        
        private static Smtp _smtp = new Smtp();

        public static bool Enviar(String Remetente, String Destinatario, String Assunto,
                           String Mensagem, String Anexo ) {
            bool bRet=false;
            MailMessage m = new MailMessage();
            try
            {
                m = new MailMessage(Remetente, Destinatario, Assunto, Mensagem);
                
                m.Attachments.Add(new Attachment(Anexo));
                
                bRet=_smtp.EnviarEmail(m);
                m.Dispose();
                return bRet;
              
            }
            catch (Exception ex)
            {
                //Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Log.Registrar("ERRO: FALHA NO ENVIO : " + ex.Message);
                m.Dispose();
                return false;
            }
        }
        public static bool Conectado() {

            return _smtp.TestarConexao();
        
        }
        public static String MensagemErro(){
            return _smtp.MSG;
        }
        public static void Parar() {
            _smtp.Fechar();
        }
        public static bool Iniciar() {
            try
            {
                if (Conectado()) { return true; }
                String Servidor = TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["smtp_servidor"].ToString();
                int Porta = int.Parse(TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["smtp_porta"].ToString());
                String Usuario = TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["smtp_usuario"].ToString();
                String Senha = TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["smtp_senha"].ToString();
                Boolean SSL = Boolean.Parse(TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["smtp_ssl"].ToString());
                Boolean Autenticar = Boolean.Parse(TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["smtp_autenticar"].ToString());
                return  _smtp.Configurar(Servidor, Porta, Autenticar, Usuario, Senha, SSL);                 
                
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
                
        }
    }
}
