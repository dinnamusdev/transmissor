using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TransmissorLog;
using TransmissorUtil;
using TransmissorDAO;
using TransmissorUtilZip;
using System.Data.SqlClient;

namespace SincronizadorDeTabelas
{
    class EnviarMetodosBasicos
    {

        public static Int32 nIDSincEnvioAtual=0;
        private static DataSet RegistrosTabelaSincronismo(String cNomeArquivo, String cNomeTabela, DateTime dDataUltimoSincronismo, Boolean bCargaTotal) {
            DataSet dsTmp = new DataSet();
            try
            {
                dsTmp = DAO.Pesquisar("select * from " + cNomeTabela + (!bCargaTotal ? " where ultimamodificacao >'" + dDataUltimoSincronismo.ToString("yyyy-MM-dd hh:mm:ss") + "'" : ""));

            }
            catch (Exception ex)
            {
                
                 Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                 return null;
            }
            return dsTmp;
        
        }

        public static int GerarArquivoTabela(String cNomeArquivo , String cNomeTabela , DateTime dDataUltimoSincronismo , Boolean bCargaTotal ) {
            /*
                '0 - error
                '1 - com reg
                '2 - sem reg
             */
            try
            {
                DataSet rstTmp = RegistrosTabelaSincronismo(cNomeArquivo, cNomeTabela, dDataUltimoSincronismo, bCargaTotal);

                if (rstTmp!=null)
                {
                    if (rstTmp.Tables[0].Rows.Count>0)
                    {
                        if (Util.GerarXML(rstTmp, cNomeArquivo)) {                             
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }                       
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                
                 Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                 return 0;
            }
        }

        private static DateTime Sincronismo_DataUltimoSinc() {
            DateTime dDataUltimoSinc = new DateTime();
            try
            {

                DataSet rstTmp = DAO.Pesquisar("select top 1  CAST(data + ' ' + isnull(hora,'') as datetime) DataHora  from Sincronismo_Envio order by data desc");
                if (rstTmp!=null)
                {
                    if (rstTmp.Tables[0].Rows.Count>0)
                    {
                        dDataUltimoSinc = (DateTime)
                        rstTmp.Tables[0].Rows[0]["DataHora"]; //+ " " +
                        //rstTmp.Tables[0].Rows[0]["hora"].ToString();
                        //DateTime.Parse(

                    }
                    else
                    {
                        dDataUltimoSinc = DateTime.Parse("01/01/00 00:00:00");
                    }   
                }
            }
            catch (Exception ex)
            {
                
                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
            }
            return dDataUltimoSinc;
        }

        public static String Sincronismo_ProcessarSincronismo(String cLocal, DateTime dDataHoraReenvio, Int16 nCodigoLoja, Int32 nIdEnvio, bool bEnvioAutomatico, bool bCargaTotal)
        {
            String cNomeArquivoZip = "", cNomeArquivoLista="";
            String cRetorno = "";
            try
            {
                List<String> clArquivos=new List<string>();
                DateTime clRetornoDataUltimoSinc = Sincronismo_DataUltimoSinc();
                DateTime dDataArquivo = DateTime.Now;

                
                nIDSincEnvioAtual = 0;

                cNomeArquivoZip = "SINCDINNAMUS_" + dDataArquivo.ToString("ddMMyyHHmmss") + ".ZIP";
                
                cNomeArquivoLista = cNomeArquivoZip.ToUpper().Replace(".ZIP", ".LST");

                clArquivos = GerarArquivos(cLocal,clRetornoDataUltimoSinc, nCodigoLoja, nIdEnvio, dDataArquivo, bCargaTotal);
                if (clArquivos.Count > 0)
                {
                    if (!Sincronismo_ArquivoLista(cNomeArquivoLista, clArquivos))
                    {
                        return "";
                    }
                    clArquivos.Add(cNomeArquivoLista);

                    if (!Zip.Compactar(cNomeArquivoZip, clArquivos))
                    {
                        return "";
                    }

                    if (nIdEnvio == 0)
                    {
                        if (!Sincronismo_GravarPacote(nIDSincEnvioAtual, cNomeArquivoZip))
                        {
                            return "";
                        }
                    }

                    for (int i = 0; i < clArquivos.Count; i++)
                    {
                        Util.ExcluirArquivo(clArquivos[i]);
                    }

                    cRetorno = cNomeArquivoZip + "|#|id:"+ nIDSincEnvioAtual ;

                }
                else {
                    cRetorno = "Não ocorreram alterações na base";
                }
    
            }
            catch (Exception ex)
            {

                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
            }
            return cRetorno;
        }
        private static String Sincronismo_GerarArquivoRegistroTransferencia(Int32 nIdEnvio , DateTime DataHora ) {
            String Retorno = "";
            try
            {
                String cNomeArquivo = "Sincronismo_Envio" + "_" + DataHora.ToString("ddMMyy") + "_" + DataHora.ToString("hhmmss") + ".din";

                DataSet rstTmp = DAO.Pesquisar("select top 1 * from Sincronismo_Envio with (nolock) " + (nIdEnvio > 0 ? " where id=" + nIdEnvio : "") + " order by id desc");
                if (rstTmp!=null)
	            {   
    	          if (rstTmp.Tables[0].Rows.Count>0){
                     nIDSincEnvioAtual = Int32.Parse( rstTmp.Tables[0].Rows[0]["id"].ToString());
                     if (Util.GerarXML(rstTmp,cNomeArquivo))
	                 {
                         Retorno = cNomeArquivo;
	                 }
                  }	        
	            }
            }
            catch (Exception ex)
            {
                
               Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
             
            }
            return Retorno;
        }

        private static bool Sincronismo_RegistrarEnvio(DateTime DataHora, int nCodigoLojaOrigem)
        {
            try
            {

                return DAO.Executar("insert into Sincronismo_Envio(Data,LojaOrigem) values ('" + DataHora.ToString("yyyy-MM-dd hh:mm:ss") + "'," + nCodigoLojaOrigem + ")");

                 
            }
            catch (Exception ex)
            {
                
               Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
               return false;
             
            }
        }


        private static bool Sincronismo_GravarPacote(Int32 nIdEnvio, String cNomeArquivoTRX )
        {
            try
            {
                SqlCommand cmd = new SqlCommand("update sincronismo_envio set pacote=@pacote where id=" + nIdEnvio);
                cmd.Parameters.Add(new SqlParameter("@pacote",SqlDbType.Image));
                cmd.Parameters["@pacote"].Value = Util.FileToBlob(cNomeArquivoTRX);
                cmd.Connection = DAO.cnxDinnamuS;
                cmd.Transaction = DAO.Trx;
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                
                 Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                 return false;
            }
        }
        
        private static bool Sincronismo_ArquivoLista(String cNomeArquivoLista, List<String> clListaArquivos) {
            try
            {
                String cConteudoArquivo ="";
                for (int i = 0; i < clListaArquivos.Count ; i++)
			    {
                    cConteudoArquivo = cConteudoArquivo + clListaArquivos[i] + "\n";

			    }
                if (!Util.GravarArquivo(cNomeArquivoLista, cConteudoArquivo))
                {
                    return false;
                }
                    
                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }        
        private static List<String> GerarArquivos(String cLocal , DateTime cDataUltimoSincronismo , Int16 nCodigoLoja, Int32 nIdEnvio , DateTime dDataHora, Boolean bCargaTotal ) {

            List<String> lsRetorno = new List<string>();
            try
            {
                nIDSincEnvioAtual = 0;
                int nRetorno=0;
                String cNomeTabela="",cNomeArquivo="";
                DataSet rstTmp = Sincronismo_ListaTabelaSincronismo(nCodigoLoja);
                for (int i = 0; i < rstTmp.Tables[0].Rows.Count; i++)
                {
                   
                    
                    cNomeTabela = rstTmp.Tables[0].Rows[i]["NomeTabela"].ToString();

                    
                    cNomeArquivo = rstTmp.Tables[0].Rows[i]["ordem"].ToString().PadLeft(4,'0') + "-" + 
                    cNomeTabela + "#" +                     
                    rstTmp.Tables[0].Rows[i]["ChavePrimaria"].ToString() + "#" + 
                    dDataHora.ToString("ddMMyy") + "#" + 
                    dDataHora.ToString("hhmmss") + ".din";

                    nRetorno = GerarArquivoTabela(Util.DiretorioAPP() + "\\" + cNomeArquivo, cNomeTabela, cDataUltimoSincronismo, bCargaTotal);
                    if (nRetorno == 1 ){
                        lsRetorno.Add(cNomeArquivo);
                    }                    
                }

                
                 if( lsRetorno.Count > 0 ){
                    if (nIdEnvio == 0 ){
                        if(!Sincronismo_RegistrarEnvio(dDataHora,nCodigoLoja)){
                            return null;
                        }

                    }
                    String cNomeArquivoRegistroTransf ="";

                    cNomeArquivoRegistroTransf = Sincronismo_GerarArquivoRegistroTransferencia(nIdEnvio, dDataHora);
             
                    if( cNomeArquivoRegistroTransf.Trim().Length > 0){
                       lsRetorno.Add (cNomeArquivoRegistroTransf);
                    }
                 }       
            }
            catch (Exception ex)
            {

                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                return null;
            }
            return lsRetorno;
        }
       public static DataSet Sincronismo_ListaTabelaSincronismo(Int16 nCodigoLoja) {
            DataSet ds=null;
            Boolean bMatriz = false;
            try
            {
                String sql = "select isnull(codigolojamatriz,0) codigolojamatriz from lojas where codigo=" + nCodigoLoja;
                
                //SqlCommand cmd = new SqlCommand(sql,DAO.cnxDinnamuS,

                DataSet dsTmp = DAO.Pesquisar(sql);

                if (dsTmp!=null)
                {
                    if (dsTmp.Tables[0].Rows[0]["codigolojamatriz"].ToString()=="0")
                    {
                        bMatriz = true;
                    }
                }

                ds = DAO.Pesquisar("select * from Sincronismo_Tabelas where " + (bMatriz ? "EnviarDaMatriz=1" : "EnviarDaFilial=1") + "order by ordem");
            }
            catch (Exception ex)
            {
                
                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace); 
            }
            return ds;
        }
        
  
    }
}
