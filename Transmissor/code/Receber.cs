using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using TransmissorLog;
using TransmissorUtil;
using TransmissorDAO;
using Transmissor.code.SincronizarTabelas.Recepcao;

namespace Transmissor.code
{
    class Receber
    {

        //private static bool bExcluiu = false;
        public static bool RecebeuMsg_Movimento = false;
        public static bool RecebendoTransferenciaRetaguarda = false;
        private static bool RecebeuMsg = false;
        public static String PastaRecepcao=".\\TRXE_RECEBIDAS";
        public static bool MensagensRecebida = false;
        public static bool Receptor_ReceberMovimentoAutomaticamento(Int16 nCodigoLoja) {
            try
            {
                DataSet dsMovRec = Receptor_ArquivosMovimento(nCodigoLoja, 0, "");
                if (dsMovRec.Tables[0].Rows.Count>0)
                {
                    List<Int32> lsMovimento = new List<Int32>();
                    for (int i = 0; i < dsMovRec.Tables[0].Rows.Count; i++)
                    {
                        lsMovimento.Add(Int32.Parse( dsMovRec.Tables[0].Rows[0]["idregistro"].ToString()));
                        
                    }
                    ReceberMetodosBasicos.Sincronismo_ReceberArquivo(lsMovimento, TransmissorApp.nCodigoLoja);
                }
                else
                {
                    RecebeuMsg_Movimento = false;
                }                   
                return true;
            }
            catch (Exception ex)
            {
                
               Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace); 
               //Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               return false;
            }
        }
        public static bool Receptor_GravarTransferencia(Int32 nCodigoLoja, List<Int32> lTransf) {
            try
            {
                DataSet ds = new DataSet();
                
                for (int i = 0; i < lTransf.Count; i++)
                {
                    ds = Receptor_ArquivosRecebidos(nCodigoLoja,lTransf[i],"");
                    
                    if (ds.Tables[0].Rows.Count >0)
                    {
                        byte[] Arquivo = null; ;
                        String cNomeArquivo = "";
                        if (!Directory.Exists(PastaRecepcao)) {
                            Directory.CreateDirectory(PastaRecepcao);
                        }
                        else
                        {
                            LimparDiretorio(PastaRecepcao);
                        }

                        cNomeArquivo = ds.Tables[0].Rows[0]["nomearquivo"].ToString();

                        Arquivo = (byte[])ds.Tables[0].Rows[0]["arquivo"];
                        if(File.Exists(PastaRecepcao + "\\" + cNomeArquivo)){
                            File.Delete(PastaRecepcao + "\\" + cNomeArquivo);
                        }
                        if (Util.BlobToFile(PastaRecepcao + "\\" + cNomeArquivo,Arquivo)=="")
                        {
                            break;
                        }
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
        public static bool LimparDiretorio(String cCaminho) {
            try
            {

                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@cCaminho);

                if (di.GetFiles().Length > 0)
                {
                    FileInfo[] fi = di.GetFiles();

                    for (int i = 0; i < fi.Length; i++)
                    {
                        fi[i].Delete();
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

        public static bool Receptor_VerificarMensagens(Int32 nLojaAtual) {
            try
            {

                String cInfoMsgCompleta ="";
                String cInfoMsgDecodificada =""; 
                String cCorpoMsg ="";
                String cNomeArquivo ="";
                bool IdMsgTransmissor=false;

                String[] vrDadosInfo =null;
                int nTotalMSG =0;

                Int32 nIDregistro =0;
                String cModulo ="";
                Int16 nLojaOrigem  =0;
                Int16 nLojaDestino =0;
                String cDataEnvio ="";
                String cNomeArquivoSalvo = "";
                RecebeuMsg = false;
                RecebeuMsg_Movimento = false;
                TransmissorServicoPOP._Pop.MensagensExcluidas = false;
                nTotalMSG = TransmissorServicoPOP._Pop.MensagensTotal();
                if (nTotalMSG > 0)
                {
                    
                    for (int i = 1; i <= nTotalMSG; i++)
                    {

                        if (!TransmissorServicoPOP._Pop.UltimaMensagemServidor().Contains("OK")) {
                            break;
                        }
                        cInfoMsgCompleta = TransmissorServicoPOP._Pop.MensagemInformacao(i);
       
                        if(cInfoMsgCompleta.Contains("*DinnamuS*Teste SMTP*")){
                           TransmissorServicoPOP._Pop.MensagemDeletar (i);
                           TransmissorServicoPOP._Pop.MensagensExcluidas = true;
                        }
                        else
                        {
                            IdMsgTransmissor =  
                                cInfoMsgCompleta.Contains("TRXC_DTI_1.0") ||
                                cInfoMsgCompleta.Contains("TRXM_DTI_1.0");

                            if (IdMsgTransmissor)
                            {
                                vrDadosInfo = cInfoMsgCompleta.Split("|#|".ToCharArray());
                                if (vrDadosInfo.Length > 0)
                                {
                                    cInfoMsgDecodificada = Util.Base64Decode(vrDadosInfo[8]);
                                    vrDadosInfo = cInfoMsgDecodificada.Split("|#|".ToCharArray());
                                    if (vrDadosInfo.Length > 1)
                                    {
                                        if (vrDadosInfo[0].Contains("TRX"))
                                        {
                                            cCorpoMsg = TransmissorServicoPOP._Pop.MensagemCorpo(i);
                                            // Debug.Print cCorpoMsg
                                            string[] vrCorpoMSG = null;
                                            vrCorpoMSG = cCorpoMsg.Split("|#|".ToCharArray());
                                            if (vrCorpoMSG.Length > 0)
                                            {

                                                //string[] vrCorpoMSGTratada = vrCorpoMSG;
                                                List<String> lista= vrCorpoMSG.ToList();
                                                for (int j = 0; j < vrCorpoMSG.Length; j++)
                                                {
                                                    if (vrCorpoMSG[j] == "") {
                                                        lista.Remove("");                                                                                                           
                                                    }
                                                }
                                                

                                                vrCorpoMSG = lista.ToArray();

                                                vrCorpoMSG = (Util.Base64Decode(vrCorpoMSG[vrCorpoMSG.Length - 1]).Split("|#|".ToCharArray()));
                                                lista = vrCorpoMSG.ToList();
                                                for (int j = 0; j < vrCorpoMSG.Length; j++)
                                                {
                                                    if (vrCorpoMSG[j] == "")
                                                    {
                                                        lista.Remove("");
                                                    }
                                                }

                                                vrCorpoMSG = lista.ToArray();

                                                nLojaDestino = Int16.Parse(vrCorpoMSG[1].Replace("LJ:DEST:", "").Trim());
                                                nIDregistro = Int16.Parse(vrCorpoMSG[2].Replace("ID:", "").Trim());
                                                nLojaOrigem = Int16.Parse(vrCorpoMSG[0].Replace("LJ.ORIG:", "").Trim());
                                                cDataEnvio = vrCorpoMSG[3].Replace("DT.ENV:", "").Trim().Replace("\n", "");

                                                
                                                cNomeArquivo = TransmissorServicoPOP._Pop.MensagemAnexos(i);
                                                if (nLojaDestino == TransmissorApp.nCodigoLoja)
                                                {
                                                    if (cNomeArquivo.Trim().Length > 0)
                                                    {
                                                        if (TransmissorServicoPOP._Pop.MensagemAnexosSalvar(i, cNomeArquivo, Util.DiretorioAPP()))
                                                        {
                                                            cNomeArquivoSalvo = (Util.DiretorioAPP() + "\\" + cNomeArquivo).Replace("\\\\", "\\");
                                                        }
                                                    }
                                                    DataSet dsConsultarPacote = null;
                                                    String TipoArquivo = (cInfoMsgCompleta.Contains("TRXC_DTI_1.0") ? "TRANSFERENCIA" : "MOVIMENTO");

                                                    dsConsultarPacote = Receptor_PacoteJaRecebido(nIDregistro, nLojaOrigem, TipoArquivo);
                                                    if (dsConsultarPacote != null)
                                                    {
                                                        if (dsConsultarPacote.Tables[0].Rows.Count == 0)
                                                        {
                                                            Log.Registrar("INFO: RECEBENDO MENSAGEM " + TipoArquivo + " " + nIDregistro + " - LOJA ORIGEM :" + nLojaOrigem);
                                                            if (Receptor_ReceberArquivo(nIDregistro, TipoArquivo, DateTime.Parse(cDataEnvio), DateTime.Now, nLojaOrigem, nLojaDestino, cNomeArquivo))
                                                            {
                                                                Log.Registrar("INFO: --> MENSAGEM RECEBIDA COM SUCESSO");
                                                                if (TipoArquivo=="TRANSFERENCIA")
                                                                {
                                                                    RecebeuMsg = true;
                                                                }
                                                                else
                                                                {
                                                                    RecebeuMsg_Movimento = true;
                                                                }
                                                                
                                                                TransmissorServicoPOP._Pop.MensagemDeletar(i);

                                                                if (!TransmissorServicoPOP._Pop.UltimaMensagemServidor().Contains("OK"))
                                                                {
                                                                    break;
                                                                }
                                                                TransmissorServicoPOP._Pop.MensagensExcluidas = true;
                                                            }
                                                            else {
                                                                Log.Registrar("ERRO: --> FALHA NO RECEBIMENTO DA MENSAGEM");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //Util.Mensagem("Deletando msg " + i, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                            TransmissorServicoPOP._Pop.MensagemDeletar(i);
                                                            if (!TransmissorServicoPOP._Pop.UltimaMensagemServidor().Contains("OK"))
                                                            {
                                                                break;
                                                            }
                                                            TransmissorServicoPOP._Pop.MensagensExcluidas = true;
                                                        }
                                                    }

                                                    
                                                    File.Delete(cNomeArquivoSalvo);
                                                }
                                                else {
                                                    TransmissorServicoPOP._Pop.MensagemDeletar(i);
                                                    if (!TransmissorServicoPOP._Pop.UltimaMensagemServidor().Contains("OK"))
                                                    {
                                                        break;
                                                    }
                                                    TransmissorServicoPOP._Pop.MensagensExcluidas = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else {
                                TransmissorServicoPOP._Pop.MensagemDeletar(i);
                                if (!TransmissorServicoPOP._Pop.UltimaMensagemServidor().Contains("OK"))
                                {
                                    break;
                                }
                                TransmissorServicoPOP._Pop.MensagensExcluidas = true;
                            }
                        }
                    }
                  
                }
                if (RecebeuMsg)
                {
                    MensagensRecebida = true;
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
        private static DataSet Receptor_PacoteJaRecebido(Int32 nIDregistro, Int32 nLojaOrigem, String Modulo)
        { 
            try 
	        {	        

               String cSQL ="";               
               cSQL = "select 1 from Transmissor_Receber_Arquivos where nomemodulo='"+ Modulo  +"' and idregistro='" + nIDregistro + "' and lojaorigem='" + nLojaOrigem + "'";
               DataSet ds = DAO.Pesquisar(cSQL);
               return ds;		

	        }
	        catch (Exception ex)
	        {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
	        }
        }
        public static DataSet Receptor_ArquivosRecebidos(Int32 nCodigoLoja, Int32 ID, String Status) 
        {
            DataSet ds = null;
            try
            {
                String cSQL = "SELECT tra.*,l.nome NomeLojaOrigem FROM Transmissor_Receber_Arquivos tra inner join lojas l on l.codigo=tra.lojaorigem where "+ 
                    (Status=="Recebido" ? "" : " not ")+  "  tra.IdRegistro in (select codigo from dadosrecebidos where loja="+
                    nCodigoLoja + " and feito='S' )and" +
                    " tra.nomemodulo='TRANSFERENCIA' AND   tra.lojadestino=" + nCodigoLoja + (ID > 0 ? "and tra.idregistro="
                    + ID : "") + (Status=="Recebido" ? " order by tra.id desc" :"");


                ds = DAO.Pesquisar(cSQL);

            }
            catch (Exception ex)
            {

                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
            }
            return ds;
        }

        public static DataSet Receptor_ArquivosRecebidos_OK( Int32 nCodigoLoja, Int32 ID, String Status)
        {
            DataSet ds = null;
            try
            {
                String cSQL = "SELECT tra.*,l.nome NomeLojaOrigem,dr.data DataRec_Retaguarda FROM Transmissor_Receber_Arquivos tra " +
                    "inner join dadosrecebidos dr on dr.codigo=tra.idregistro inner join lojas l "+
                    " on l.codigo=tra.lojaorigem where " +
                    "tra.nomemodulo='TRANSFERENCIA' and tra.lojadestino=" + nCodigoLoja + (ID > 0 ? "and tra.id="
                    + ID : "") + (Status == "Recebido" ? " order by tra.id desc" : "");


                ds = DAO.Pesquisar(cSQL);

            }
            catch (Exception ex)
            {

                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return ds;
        }

        public static DataSet Receptor_ArquivosMovimento_ERP_OK(Int32 nCodigoLoja, Int32 ID, String Status)
        {
            DataSet ds = null;
            try
            {
                String cSQL = "SELECT tra.*,l.nome NomeLojaOrigem,sc.datarecebido datahorarec_erp "+
                    "FROM Transmissor_Receber_Arquivos tra inner join lojas l on l.codigo=tra.lojaorigem   " +
                    " inner join Sincronismo_Receber sc  on sc.idenvio=tra.idregistro "+
                    "where tra.idregistro in (select idenvio from Sincronismo_Receber where lojaorigem=tra.lojaorigem) and  tra.nomemodulo='MOVIMENTO' and tra.lojadestino=" + nCodigoLoja + (ID > 0 ? " and tra.id="
                    + ID : "") +  " order by tra.idregistro desc" ;


                ds = DAO.Pesquisar(cSQL);

            }
            catch (Exception ex)
            {

                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return ds;
        }

        public static DataSet Receptor_ArquivosMovimento(Int32 nCodigoLoja, Int32 ID, String Status)
        {
            DataSet ds = null;
            try
            {
                String cSQL = "SELECT tra.*,l.nome NomeLojaOrigem FROM Transmissor_Receber_Arquivos tra "+
                    "inner join lojas l on l.codigo=tra.lojaorigem where  " +
                    "not tra.idregistro in (select idenvio from Sincronismo_Receber where lojaorigem=tra.lojaorigem) and " +
                    "tra.nomemodulo='MOVIMENTO' and tra.lojadestino=" + nCodigoLoja + (ID > 0 ? " and tra.idRegistro="
                    + ID : "") + " order by tra.idregistro" ;
                
                ds = DAO.Pesquisar(cSQL);

            }
            catch (Exception ex)
            {

                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return ds;
        }


        private static bool Receptor_ReceberArquivo(Int32 nIDregistro, String cNomeModulo, DateTime dDataHoraEnvio, DateTime dDataHoraChegada, Int16 nLojaOrigem, Int16 nLojaDestino, String cNomeArquivo)
        {
            try
            {
                String cSQL ="";
                Int32 nId =0;
  
                nId = DAO.GerarSequenciaTabela("Transmissor_Receber_Arquivos");
                cSQL = "INSERT INTO Transmissor_Receber_Arquivos" +
                "(ID, NomeModulo, IDRegistro, DataHoraChegada, DataHoraEnvio, LojaOrigem, LojaDestino,Arquivo,nomearquivo) values" +
                "(" + nId + ", '" + cNomeModulo + "', " + nIDregistro + ", '" +
                dDataHoraChegada.ToString("yyyy/MM/yy hh:mm:ss") +
                "','" + dDataHoraEnvio.ToString("yyyy/MM/yy hh:mm:ss") +
                "', " + nLojaOrigem + ", " + nLojaDestino + ",@arquivo,'"+cNomeArquivo  +"')";
                SqlCommand comando = new SqlCommand(cSQL);
                comando.Connection = DAO.cnxDinnamuS;
                SqlParameter p= new SqlParameter("@arquivo",System.Data.SqlDbType.Image);
                p.Value = Util.FileToBlob(cNomeArquivo);
                comando.Parameters.Add(p);
                comando.ExecuteNonQuery();
                comando.Dispose();               

                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        

    }
}
