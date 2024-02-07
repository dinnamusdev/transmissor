using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.IO.Compression;
 

namespace TransmissorUtil
{
    public class Util
    {
        
        public static bool GravarArquivo(String cNomeArquivo,String Arquivo) { 
            try 
	        {
                StreamWriter arquivo = new StreamWriter(new FileStream(cNomeArquivo, FileMode.Append, FileAccess.Write, FileShare.ReadWrite));
                arquivo.Write(Arquivo);
                arquivo.Flush();
                arquivo.Close();
                
                return true;
	        }
	        catch (Exception ex)
	        {

                return false;
	        }
        
        }
        public static String BlobToFile(String Arquivo ,byte[] data) {
            try
            {
                //byte[] data = 

                File.WriteAllBytes( Arquivo, data);

                

                return Arquivo;

            }
            catch (Exception ex)
            {
                //Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "";
            }
          
            
        }
        public static bool ExcluirArquivo(String cArquivo) {
            try
            {
                File.Delete(cArquivo);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool GerarXML(DataSet ds, String cNomeArquivo)
        {
            try
            {
                ds.WriteXml(cNomeArquivo);
                return true;
            }
            catch (Exception ex)
            {

                //Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                return false;
            }


        }

        public static DataSet LerXML( String cNomeArquivo)
        {
            DataSet ds = null;
            try
            {
                ds = new DataSet();

                ds.ReadXml(cNomeArquivo);
                
                
            }
            catch (Exception ex)
            {

                //Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                ds = null;
                
            }
            return ds;

        }
        public static byte[] FileToBlob(String filePath)
        {
            try
            {
                //string filePath = @"D:\\My Movie.wmv";

                //A stream of bytes that represnts the binary file
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                //The reader reads the binary data from the file stream
                BinaryReader reader = new BinaryReader(fs);

                //Bytes from the binary reader stored in BlobValue array
                byte[] BlobValue = reader.ReadBytes((int)fs.Length);

                fs.Close();
                reader.Close();


                return BlobValue;
            }
            catch (Exception ex)
            {
                //Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;

            }
        }
        public static string Base64Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }
        public static string Base64Decode(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        public static DialogResult Mensagem(String cMsg,MessageBoxButtons botoes, MessageBoxIcon icone) {

            return MessageBox.Show(cMsg, Application.ProductName + " " + Application.ProductVersion, botoes, icone);
        }
        public static String DiretorioAPP() {
            return Directory.GetCurrentDirectory();
        }

        public static bool Bind(Control cl, DataSet dsDados)
        {
            try
            {
            for (int i = 0; i < cl.Controls.Count; i++)
            {
                if (cl.Controls[i].Controls.Count > 0)
                {
                    Bind(cl.Controls[i], dsDados);
                }
                else
                {
                    Type t = cl.Controls[i].GetType();

                    if (t.FullName.ToLower().Contains("textbox"))
                    {
                        TextBox tx = (TextBox)cl.Controls[i];
                        if (tx.Name.ToLower().Contains("txt"))
                        {
                            tx.DataBindings.Add("text", dsDados.Tables[0], tx.Name.Replace("txt", ""));
                        }
                        //Util.Mensagem(tx.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (t.FullName.ToLower().Contains("checkbox"))
                    {
                        CheckBox chk = (CheckBox)cl.Controls[i];
                        if (chk.Name.ToLower().Contains("chk"))
                        {
                            chk.DataBindings.Add("checked", dsDados.Tables[0], chk.Name.Replace("chk", ""),false,DataSourceUpdateMode.OnValidation,new Boolean());
                        }
                    }
                }
              }
            return true;
            }
            catch (Exception ex)
            {
                //Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

           
        }


    }
}
