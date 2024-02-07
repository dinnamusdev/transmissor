using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace TransmissorLog
{
    public class Log
    {
        public static bool LogOK = false;
        private static StreamWriter logGravar=null;
        private static StreamReader logLer = null;
        public static DateTime dtAtual = new DateTime();
        private static String Caminho = Directory.GetCurrentDirectory();

        public static DialogResult Mensagem(String cMsg, MessageBoxButtons botoes, MessageBoxIcon icone)
        {

            return MessageBox.Show(cMsg, Application.ProductName + " " + Application.ProductVersion, botoes, icone);
        }
        public static bool Iniciar(DateTime dt) {
            try
            {

                dtAtual = DateTime.Today;
                String cNomeArquivo = Caminho + "\\transmissor_log_" + dtAtual.ToString("ddMMyyyy") + ".txt";
                
                logGravar = new StreamWriter(new FileStream( cNomeArquivo,FileMode.Append,FileAccess.Write,FileShare.ReadWrite ));
                logGravar.AutoFlush = true;
                LogOK = true;
                return true;
                    
            }
            catch (Exception ex)
            {
                //Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);

                Mensagem(ex.Message, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return false;
            }
            
        }


        public static String Ler(DateTime dt)
        {
            String cRetorno = "";
            try
            {
                String cNomeArquivo = Caminho + "\\transmissor_log_" + dt.ToString("ddMMyyyy") + ".txt";
                if (File.Exists(cNomeArquivo))
                {
                    Stream s = new FileStream(cNomeArquivo, FileMode.Open,FileAccess.Read,FileShare.ReadWrite  );

                    logLer = new StreamReader(s);
                    cRetorno =logLer.ReadToEnd();
                 
                }

            }
            catch (Exception ex)
            {

                Mensagem(ex.Message, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                
            }
            return cRetorno;
        }
        public static bool Fechar() { 
        try
             {
                            
                logGravar.Close();
                LogOK = false;

                return true;

            }
            catch (Exception ex)
            {

                Mensagem(ex.Message, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return false;
            }   
        }
        public static bool AVISO(String cTexto)
        {
            return Registrar("AVISO: " + cTexto);
        }
        public static bool INFO(String cTexto) {
            return Registrar("INFO: " + cTexto);
        }
        public static bool ERRO(String cTexto)
        {
            return Registrar("ERRO: " + cTexto);
        }   
        public static bool Registrar(String cTexto)
        {
            try
            {
                if (cTexto=="ERRO")
                {
                    MessageBox.Show("erro");
                }
                logGravar.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + " - " + cTexto  );

                return true;

            }
            catch (Exception ex)
            {

                Mensagem(ex.Message, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return false;
            }

        }
    }
}
