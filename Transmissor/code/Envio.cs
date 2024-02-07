using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using TransmissorDAO;
using TransmissorLog;
using TransmissorUtil;

namespace Transmissor.code
{
    class Envio
    {

        public static bool EnviarArquivo(Int32 nIDArquivo)
        {
            bool bRetorno = false;
            try
            {
                if (DAO.IniciarTRX())
                {
                    if (EnviarArquivo_Acao(nIDArquivo))
                    {
                        bRetorno=DAO.GravarTRX();
                    }
                    else
                    {
                        DAO.DesfazerTRX();
                    }
                }

                return bRetorno;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private static bool EnviarArquivo_Acao(Int32 nIDArquivo )
        {
            bool bRetorno = false;
            DataSet rstTmpArquivo_Email = new DataSet();
            DataSet rstTmpArquivo = new DataSet();
            String cRemetente ="";
            String cDestinatario="";
            String cAssunto ="";
            String cMensagem ="";
            String cNomeArquivoTrx ="";
            String cNomeArquivo="";
            
            try
            {
                cRemetente = TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["smtp_usuario"].ToString();

                rstTmpArquivo = DAO.Pesquisar("select * from Transmissor_Enviar_arquivos where id=" + nIDArquivo);

                rstTmpArquivo_Email = DAO.Pesquisar("select * from Transmissor_Enviar_Arquivos_Transmitir where idarquivo=" + nIDArquivo);

                cAssunto = "TRX" + (rstTmpArquivo.Tables[0].Rows[0]["NomeModulo"].ToString() == "TRANSFERENCIA" ? "C" : "M" ) + "_DTI_1.0" +
                "|#|LJ.DEST:" + rstTmpArquivo_Email.Tables[0].Rows[0]["LOJADESTINO"].ToString();
                   
                cAssunto = cAssunto + "|#|" + Util.Base64Encode(cAssunto);
                
                cNomeArquivo = rstTmpArquivo.Tables[0].Rows[0]["NomeArquivo"].ToString();

                for (int i = 0; i < rstTmpArquivo_Email.Tables[0].Rows.Count; i++)
                {                   
                      
                      cMensagem = "LJ.ORIG:" + TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["LOJA"].ToString() + "\n";
                      cMensagem = cMensagem + " |#|LJ:DEST:" + rstTmpArquivo_Email.Tables[0].Rows[i]["LOJADESTINO"].ToString() + "\n";
                      cMensagem = cMensagem + " |#|ID:" + rstTmpArquivo.Tables[0].Rows[0]["idregistro"].ToString() + "\n";
                      cMensagem = cMensagem + " |#|DT.ENV:" + rstTmpArquivo.Tables[0].Rows[0]["DATA"].ToString() + "\n";
                      cMensagem = cMensagem + " |#|NOME.ARQ:" + cNomeArquivo.Substring(cNomeArquivo.LastIndexOf('\\') + 1) + "\n"; ;
                      cMensagem = cMensagem + " |#| " + Util.Base64Encode(cMensagem);

                      cDestinatario = rstTmpArquivo_Email.Tables[0].Rows[i]["email"].ToString();

                      //rstTmpArquivo_Email.Tables[0].Rows[i].
                      byte[] dados = (byte[])rstTmpArquivo.Tables[0].Rows[0]["arquivo"];
                      
                      cNomeArquivoTrx = Transmissor_GerarArquivoParaTransmissao(nIDArquivo,
                                        rstTmpArquivo.Tables[0].Rows[0]["NomeArquivo"].ToString(),
                                        dados);

                      if (cNomeArquivoTrx.Length>0)
                      {
                          bool bRetornoEnvio = false;
                          bRetornoEnvio =TransmissorServico_SMTP.Enviar(cRemetente, cDestinatario, cAssunto, cMensagem, cNomeArquivoTrx);
                          if (bRetornoEnvio)
                          {
                              bRetorno= DAO.Executar("update Transmissor_Enviar_arquivos set status='ENVIADO' where id=" + nIDArquivo);
                              if (!bRetorno)
                              {
                                  break;
                              }
                          }

                          File.Delete(cNomeArquivoTrx);

                          Transmissor_EnviarArquivo_GravarLog(nIDArquivo,
                                Int32.Parse(rstTmpArquivo_Email.Tables[0].Rows[i]["id"].ToString()),
                                bRetornoEnvio, TransmissorServico_SMTP.MensagemErro());
                      }
                }                
                return bRetorno;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private static bool Transmissor_EnviarArquivo_GravarLog(Int32 nIDArquivo, Int32 nIDEnvio, Boolean bEnviado, String cMensagem) {
            bool bRet = true;
            try
            {


                String cSQL = "INSERT INTO Transmissor_Enviar_Arquivos_Transmitir_Log" +
                                  "(id, id_Arquivo_transmitido, Enviado, DataHoraTransmissao, Mensagem)" +
                                "VALUES     (" + DAO.GerarSequenciaTabela("Transmissor_Enviar_Arquivos_Transmitir_Log") +
                                  "," + nIDEnvio + "," + (bEnviado ? "1" : "0") + "," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','" + cMensagem + "')";
        
                if (DAO.Executar(cSQL)){
                    if(!bEnviado)
                    {
                        Int32 nFimDaFila =0;
        
                        nFimDaFila = Int32.Parse( DAO.Pesquisar("select isnull(max(ordem),0)+1 ordem  from Transmissor_Enviar_Arquivos").Tables[0].Rows[0]["ordem"].ToString());
        
                        cSQL = "update Transmissor_Enviar_arquivos set ordem=" + nFimDaFila + " where status is null and id=" + nIDArquivo;
        
                        if (!DAO.Executar(cSQL))
                        {
                            bRet= false;
                        }
                    }
                }
                else
                {
                    bRet = false;
                }
                return bRet;

            }
            catch (Exception ex) 
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        
        
        }

        /*
         Function Transmissor_EnviarArquivo_GravarLog(nIDArquivo As Long, nIDEnvio As Long, bEnviado As Boolean, cMensagem As String) As Boolean
On Error GoTo sdskdjk

Dim cSQL As String

cSQL = "INSERT INTO Transmissor_Enviar_Arquivos_Transmitir_Log" & _
                      "(id, id_Arquivo_transmitido, Enviado, DataHoraTransmissao, Mensagem)" & _
"VALUES     (" & getDao.GerarNovoCodigoTabela(getDao.GetObjetoConexao.ConnectionString, "Transmissor_Enviar_Arquivos_Transmitir_Log") & _
            "," & nIDEnvio & "," & IIf(bEnviado, "1", "0") & "," & _
              "'" & Format(Date & " " & Time, "mm/dd/yyyy hh:MM:ss") & "','" & cMensagem & "')"
            
            
If getDao.ExecutaSQL(cSQL) Then
    If Not bEnviado Then
        Dim nFimDaFila As Long
        
        nFimDaFila = getDao.GerarResultSet("select isnull(max(ordem),0)+1 ordem  from Transmissor_Enviar_Arquivos").Fields("Ordem").value
        
        cSQL = "update Transmissor_Enviar_arquivos set ordem=" & nFimDaFila & " where id=" & nIDArquivo
        
        If Not getDao.ExecutaSQL(cSQL) Then
           Exit Function
        End If
    End If
    
    Transmissor_EnviarArquivo_GravarLog = True
    
End If

Exit Function
sdskdjk:
MsgBox Err.Description
Resume Next
End Function

         
         */

        private static String Transmissor_GerarArquivoParaTransmissao(Int32 nIDArquivo , String cNomeArquivo , byte[] DadosArquivos){
            String cNomeArquivoFinal = "";
            String cDiretorioAtual = Util.DiretorioAPP();
            try{
                
                if (!Directory.Exists(cDiretorioAtual+ "\\trx_tmp")){
                 
                    Directory.CreateDirectory(cDiretorioAtual+"\\trx_tmp");
                }

                cNomeArquivoFinal = cDiretorioAtual +" \\trx_tmp\\" + cNomeArquivo.Substring(cNomeArquivo.LastIndexOf('\\')+1);
                

                cNomeArquivoFinal=Util.BlobToFile(cNomeArquivoFinal, DadosArquivos);

                if (!File.Exists(cNomeArquivoFinal))
                {
                    cNomeArquivoFinal = "";
                }
                return cNomeArquivoFinal;

            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "";
                
            }
            
            
        }
        /*
         
         Function Transmissor_GerarArquivoParaTransmissao(nIDArquivo As Long, cNomeArquivo As String, fldArquivo As ADODB.Field) As String
On Error GoTo sdkjskjdk
Dim cArquivoTransmissao As String
Dim cNomeArquivoGerado As String
If Dir(".\trx_tmp", vbDirectory) = "" Then
    MkDir ".\trx_tmp"
End If

If InStrRev(cNomeArquivo, "\") > 0 Then
    cNomeArquivoGerado = ".\trx_tmp\" & Mid(cNomeArquivo, InStrRev(cNomeArquivo, "\") + 1)
Else
    cNomeArquivoGerado = ".\trx_tmp\" & cNomeArquivo
End If

If Left(cNomeArquivoGerado, 1) = "." Then
    cNomeArquivoGerado = Replace(CurDir & Mid(cNomeArquivoGerado, 2), "\\", "\")
End If


If BlobToFile(fldArquivo, cNomeArquivoGerado) Then
    Transmissor_GerarArquivoParaTransmissao = cNomeArquivoGerado
End If

Exit Function
sdkjskjdk:
MsgBox Err.Description

End Function
         
         
         */
        public static DataSet ListarFilaEnvio(String cStatus, int nCodigoLoja) {
        try
            {
                String cSQL = "select ea.ordem,ea.id,ea.data,ea.nomemodulo,ea.idregistro,ea.nomearquivo from Transmissor_Enviar_Arquivos ea " + 
                         " inner join Transmissor_Enviar_Arquivos_Transmitir eat " + 
                         "on  ea.id=eat.idarquivo   where ea.loja=" + nCodigoLoja + " and ea.status " + (cStatus == "" ? "is null" : "='" + cStatus + "'") +
                         " group by ea.ordem,ea.id,ea.data,ea.nomemodulo,ea.idregistro,ea.nomearquivo order by " + 
                         (cStatus == "ENVIADO" ? " ea.id desc" : " ea.ordem");

                return DAO.Pesquisar(cSQL);
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            
            }
        }

        public static DataSet ListarFilaEnvio_Destino(Int32 nIDArquivo , String cStatus ) {

            try
            {
                String cSQL = "select eat.id,eat.DataHoraEnviar DataHoraEnviar, l.nome , eat.email,isnull(ea.Status,'TRANSFERIR') status from " +
                    "Transmissor_Enviar_Arquivos ea "+  
                    "inner join Transmissor_Enviar_Arquivos_Transmitir eat on "+ 
                    " ea.id=eat.idarquivo inner join lojas l "  + 
                    "on l.codigo=eat.lojadestino where ea.status " + 
                    (cStatus== "" ? "is null" : "='" + cStatus + "'") +
                    " and ea.id=" + nIDArquivo + " order by ea.Id";

                return DAO.Pesquisar(cSQL);
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        public static DataSet ListarFilaEnvio_Destino_Log(Int32 nIDArquivo_Transmissao) {
            try
            {
                String cSQL = "select case when isnull(eatl.enviado,0)=1 then 'ENVIADO' ELSE ' *> NAO ENVIADO <*' END SITUACAO, eatl.* " +
                    "from Transmissor_Enviar_Arquivos_Transmitir_Log eatl " + 
                    "where eatl.id_Arquivo_transmitido=" + nIDArquivo_Transmissao;
                return DAO.Pesquisar(cSQL);
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO: " + ex.Message + " - " + ex.StackTrace);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        
        }
    
    }
}
