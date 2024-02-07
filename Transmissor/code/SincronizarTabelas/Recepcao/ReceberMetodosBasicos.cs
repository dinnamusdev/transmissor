using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransmissorLog;
using TransmissorUtil;
using TransmissorUtilZip;
using TransmissorDAO;
using BloquearSPIDUtil;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Transmissor.code.SincronizarTabelas.Recepcao
{
    class ReceberMetodosBasicos
    {
        private static SqlTransaction trxmov = null;
        public static String MSG = "";
        private static DataSet Sincronismo_RecebimentosRealizados(bool bSomenteOUltimo , Int16 nLojaOrigem ) {
            DataSet ds = null;
            try
            {
              ds = DAO.Pesquisar("select " + (bSomenteOUltimo ? "Top 1" : "") + " * from Sincronismo_Receber " +
                            (nLojaOrigem > 0 ? " where LojaOrigem=" + nLojaOrigem : "") + " order by idEnvio desc");

            }
            catch (Exception ex)
            {
                
               Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        private static String Sincronismo_Receber_ExtrairNomeArquivoEnvio(List<String>clLista) {
            String Retorno = "";
            try
            {
                String cNomeArquivo = "";
                
                for (int i = 0; i < clLista.Count; i++)
                {
                    if( clLista[i].Contains("Sincronismo_Envio" )){
                        cNomeArquivo = clLista[i];
                    }
                }
                Retorno = cNomeArquivo;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
            }
            return Retorno;
        }

        
        private static bool Sincronismo_ProcessarArquivosRecebidos(Int32 nIDEnvio, List<String>clLista , Int16 nCodigoLojaAtual) {
            bool Retorno = false;
            try
            {
                
                String  cNomeArquivoEnvio = Sincronismo_Receber_ExtrairNomeArquivoEnvio(clLista);
                DataSet rstUltimoPacoteRecebido = new DataSet();
                DataSet rstDadosEnvio = Util.LerXML(cNomeArquivoEnvio);
                Int32 nIdUltimoPacoteRecebido=0;
                if (rstDadosEnvio!=null)
                {
                    if (rstDadosEnvio.Tables[0].Rows.Count>0)
                    {
                        Int16 nCodigoLojaOrigem = Int16.Parse( rstDadosEnvio.Tables[0].Rows[0]["LojaOrigem"].ToString());

                        rstUltimoPacoteRecebido = Sincronismo_RecebimentosRealizados(true, nCodigoLojaOrigem);
                        if (rstUltimoPacoteRecebido!=null)
	                    {
		                    if (rstUltimoPacoteRecebido.Tables[0].Rows.Count>=0)
	                        {
                                if (rstUltimoPacoteRecebido.Tables[0].Rows.Count == 0)
                                {
                                    nIdUltimoPacoteRecebido = 0;
                                }
                                else
                                {
                                    nIdUltimoPacoteRecebido = Int32.Parse(rstUltimoPacoteRecebido.Tables[0].Rows[0]["idEnvio"].ToString());
                                }
                                if (nIdUltimoPacoteRecebido + 1 == nIDEnvio || nIdUltimoPacoteRecebido==0)
                                {
                                    clLista.Sort();
                                    Int16 nSpidAtual=0;
                                    BloquearSPID fBloqSpid = new BloquearSPID();
                                    if (!fBloqSpid.Iniciar(DAO.cnxDinnamuS))
                                    {
                                          Log.AVISO( "Não foi possível iniciar o bloqueador de spid.");
                                    }
                                    else
                                    {
                                        nSpidAtual = fBloqSpid.SpidAtual();

                                        if (!fBloqSpid.BloquearSpid(nSpidAtual))
                                        {
                                            Log.AVISO("Não foi possível bloquear o spid." + nSpidAtual);

                                        }
                                        else
                                        {
                                            // Inicia recepção aqui
                                            trxmov = DAO.cnxDinnamuS.BeginTransaction("movimento");
                                            if (Sincronismo_ProcessarArquivosRecebidos_Acao(clLista,cNomeArquivoEnvio,nCodigoLojaAtual)) {
                                                trxmov.Commit();
                                            }
                                            else
                                            {
                                                trxmov.Rollback("movimento");
                                            }
                                        }
                                        fBloqSpid.Terminar();
                                    }    
                                }
                                else
                                {
                                    MSG = "INFO: Pacote fora da sequência.O ultimo pacote recebido da loja: ["
                                        + nCodigoLojaOrigem + "]  foi a sequencia : " +
                                    nIdUltimoPacoteRecebido + ". Você esta tentando receber o pacote de numero : "
                                    + nIDEnvio + ". O recebimento foi CANCELADO";
                                    Log.Registrar(MSG);
                                }
	 
	                        }
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

        private static bool Sincronismo_ProcessarArquivosRecebidos_Acao(List<String> clLista, String cNomeArquivoEnvio, Int16 nCodigoLojaAtual) {
            bool Retorno = false;
            String cStringSQL="";
            String cArquivo="";
            DataSet rstTmp=new DataSet();
            String[] clDadosArquivo = null;
            String cNomeTabela = "", cNomeChavePrimaria = "";
            try
            {
                for (int i = 0; i < clLista.Count-2; i++)
                {
                     cArquivo = clLista[i];
                     rstTmp =  Util.LerXML(cArquivo); //XML_Para_Recordset(LerArquivo2(cArquivo, frmForm))
                     //Sincronismo_MensagemAoOperador_Receber "Processando Arquivo " & cArquivo
                     clDadosArquivo = cArquivo.Split('#'); //Sincronismo_NomeTabelaNomeChavePrimaria(clLista[i]);
                     cNomeTabela = clDadosArquivo[0].Substring(5);
                     cNomeChavePrimaria = clDadosArquivo[1];
                     DataSet rstRegistroExistente = new DataSet();
                     if (rstTmp!=null)
                     {
                         cStringSQL = "";
                         for (int ij = 0; ij < rstTmp.Tables[0].Columns.Count; ij++)
                         {
                            //if (!rstTmp.Tables[0].Columns[ij].ColumnName.ToLower().Contains("ultimamodificacao"))
                           // {
                                cStringSQL += (cStringSQL !="" ? "," :"") + rstTmp.Tables[0].Columns[ij].ColumnName;
                           // }                 
                         }
                         
                         for (int j = 0; j < rstTmp.Tables[0].Rows.Count; j++)
                         {

                           rstRegistroExistente = DAO.Pesquisar("select ultimamodificacao from " + cNomeTabela + " where " + cNomeChavePrimaria + " = " + rstTmp.Tables[0].Rows[j][cNomeChavePrimaria].ToString(),null,trxmov);                             
                           
                            if( rstRegistroExistente.Tables[0].Rows.Count > 0 ){                           
                            
                             DateTime dDataUltimaModificacao = DateTime.Parse( rstRegistroExistente.Tables[0].Rows[0]["ultimamodificacao"].ToString());//IIf(IsNull(!ultimamodificacao), date, !ultimamodificacao)
                             DataSet rstTmpPesquisa=new DataSet();
                             bool bPulaAtualizacao = false;
                              
                             if (cNomeTabela.ToUpper() == "ESTOQUEFILIAL"){
                                 
                                rstTmpPesquisa = DAO.Pesquisar("select codigoloja from " + cNomeTabela + " where " + cNomeChavePrimaria + " = " + rstTmp.Tables[0].Rows[j][cNomeChavePrimaria].ToString(),null,trxmov);
                                if(nCodigoLojaAtual == Int16.Parse(rstTmpPesquisa.Tables[0].Rows[0]["codigoloja"].ToString())){
                                   bPulaAtualizacao = true;
                                }                                
                             }
                             DateTime ultimamodificacao= DateTime.Parse( rstTmp.Tables[0].Rows[j]["ultimamodificacao"].ToString() );

                             if (!bPulaAtualizacao /*&& ultimamodificacao > DateTime.Parse(rstRegistroExistente.Tables[0].Rows[0]["ultimamodificacao"].ToString())*/)
                             {
                                 String SelectQuery = "select "+ cStringSQL + " from " + cNomeTabela + " where " + cNomeChavePrimaria + " = " + rstTmp.Tables[0].Rows[j][cNomeChavePrimaria].ToString();
                                 SqlDataAdapter ad = new SqlDataAdapter();
                                 
                                 rstRegistroExistente = DAO.DataSetEditavel(SelectQuery, cNomeTabela, ref ad,trxmov);
                                 rstRegistroExistente.Tables[0].Rows[0].BeginEdit();
                                 for (int col = 0; col < rstTmp.Tables[0].Columns.Count; col++)
                                 {                                     
                                    String Coluna = rstTmp.Tables[0].Columns[col].ColumnName;                                     
                                    if (!rstRegistroExistente.Tables[0].Columns[Coluna].ReadOnly)
                                    {
                                        String NomeCampo = rstRegistroExistente.Tables[0].Columns[Coluna].DataType.FullName;

                                        if (NomeCampo.ToLower()!="System.Byte[]".ToLower())
                                        {
                                            rstRegistroExistente.Tables[0].Rows[0][Coluna] =
                                            rstTmp.Tables[0].Rows[j][Coluna];
                                        }  
                                    }                                      
                                 }
                                 rstRegistroExistente.Tables[0].Rows[0].EndEdit();
                                 if (!DAO.DataSetEditavel_Gravar(rstRegistroExistente, ad)) {
                                     return false;
                                 }                                     
                                }                            
                           }else{
                               bool bInsert = false;
                               if (DAO.Pesquisar("select so.name,sc.name from sysobjects so inner join syscolumns sc on so.id=sc.id where sc.status=0x80 and so.name='" + cNomeTabela + "'",null,trxmov).Tables[0].Rows.Count> 0 ){
                                  bInsert = true;
                               }
                               String SelectQuery = "select " + cStringSQL + " from " + cNomeTabela + " where 1=2";//+ cNomeChavePrimaria + " = " + rstTmp.Tables[0].Rows[j][cNomeChavePrimaria].ToString();  
                               if (bInsert ){
                                  cStringSQL = "SET IDENTITY_INSERT " + cNomeTabela + " ON";
                                  if (!DAO.Executar(cStringSQL,null,trxmov) ){return false;}
                               }

                              
                               SqlDataAdapter ad = new SqlDataAdapter();

                               rstRegistroExistente = DAO.DataSetEditavel(SelectQuery, cNomeTabela, ref ad, trxmov);
                               
                               rstRegistroExistente.Tables[0].Rows.Add(
                                   rstTmp.Tables[0].Rows[j].ItemArray
                                   );

                               
                               Int32 nValorPK = Int32.Parse(rstTmp.Tables[0].Rows[j][cNomeChavePrimaria].ToString());
                               bool bRetornoGravacao = false;

                               bRetornoGravacao =DAO.DataSetEditavel_Gravar(rstRegistroExistente, ad, cNomeChavePrimaria, nValorPK);

                               if (bInsert)
                               {
                                   cStringSQL = "SET IDENTITY_INSERT " + cNomeTabela + " Off";
                                   if (!DAO.Executar(cStringSQL, null, trxmov)) { return false; }
                               }
                               if (!bRetornoGravacao)
                               {   
                                   return false;
                               }   
                                                            
                              
                           }                      
                         }
                     }
                     else
                     {
                         break;
                     }

                }
                if( Sincronismo_Receber_RegistrarRecepcao(Util.LerXML(cNomeArquivoEnvio))){
                    Retorno=true;
                }


            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
            }
            return Retorno;
        }


private static bool Sincronismo_Receber_RegistrarRecepcao(DataSet rstDadosEnvio ) 
{


    try
    {
        String cStringSQL = "insert into sincronismo_receber(DataEnvio,HoraEnvio,DataRecebido,HoraRecebido,LojaOrigem,idEnvio) values " +
        "('" + DateTime.Parse(rstDadosEnvio.Tables[0].Rows[0]["Data"].ToString()).ToString("yyyy-MM-dd hh:mm:ss") + "','" +
        DateTime.Parse(rstDadosEnvio.Tables[0].Rows[0]["Data"].ToString()).ToString("hh:mm:ss") +
        "',getdate(),getdate()," /*
        "cast(month(getdate()) as varchar(4))   + " + "'/'" + "  +    cast(day(getdate()) as varchar(4)) +" + "'/'" + "+   cast(year(getdate()) as varchar(4)) " +
        "," + "cast(datepart(hh,getdate()) as varchar(2)) + ':' + cast(datepart(mi,getdate()) as varchar(2)) + ':' + cast(datepart(ss,getdate()) as varchar(2))" +
        " ,"*/ + rstDadosEnvio.Tables[0].Rows[0]["LojaOrigem"].ToString() + "," +
        rstDadosEnvio.Tables[0].Rows[0]["ID"].ToString() + ")";
        return DAO.Executar(cStringSQL,null,trxmov);
    }
    catch (Exception ex)
    {

        Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
        return false;
    }

}


public static bool Sincronismo_ReceberArquivo(List<Int32> lsMovimento, Int16 nCodigoLojaAtual) {
    try
    {

        String cNomeArquivo = "";
        Int32 nId = 0;
        DataSet dsMov = new DataSet();
        for (int i = 0; i < lsMovimento.Count; i++)
        {
            nId = lsMovimento[i];
            dsMov = Receber.Receptor_ArquivosMovimento(nCodigoLojaAtual, nId, "");
            cNomeArquivo = dsMov.Tables[0].Rows[0]["nomearquivo"].ToString();
            byte[] Arquivo = (byte[])dsMov.Tables[0].Rows[0]["arquivo"];
            if (Util.BlobToFile(cNomeArquivo,Arquivo)!="")
            {
                if (!Sincronismo_ReceberArquivo_Acao( nId, cNomeArquivo, nCodigoLojaAtual)) {
                    return false;
                }
            }
            Util.ExcluirArquivo(cNomeArquivo); 

        }

        return true;
    }
    catch (Exception ex)
    {
        
        Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
        return false;
    }
}


public static bool Sincronismo_ReceberArquivo_Acao(Int32 nIdArquivo, String cNomeArquivoZip, Int16 nCodigoLojaAtual) {

    bool Retorno = false;
    try
    {
        if (DAO.Pesquisar("select 1 from sincronismo_receber where  LojaOrigem=" + nCodigoLojaAtual + " and  IdEnvio=" + nIdArquivo).Tables[0].Rows.Count == 0)
        {
            List<String> clLista = new List<string>();
            String cNomeArquivoLista = "";

            cNomeArquivoLista = cNomeArquivoZip.Replace(".ZIP", ".LST");

            clLista = Zip.DesCompactar(cNomeArquivoZip, Util.DiretorioAPP());

            if (clLista.Count > 0)
            {
                if (Sincronismo_ProcessarArquivosRecebidos(nIdArquivo,clLista, nCodigoLojaAtual))
                {
                    Retorno = true;
                }
            }                    
        }
    }
    catch (Exception ex)
    {
        Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
        return false;
    }
    return Retorno;
}
        /*
         Function Sincronismo_ReceberArquivo(cNomeArquivoZip As String, frmForm As Form, Optional bReceberAutomatica As Boolean, Optional nCodigoLojaAtual As Integer) As Boolean
On Error GoTo fkjdkfjk
Dim clLista As New Collection
Dim cNomeArquivoLista As String

cNomeArquivoLista = Replace(Dir(cNomeArquivoZip), ".ZIP", ".LST")

Sincronismo_MensagemAoOperador_Receber "Extraindo Arquivos..."

If ExtractZipArquivo(cNomeArquivoZip, CurDir) Then
   Sincronismo_MensagemAoOperador_Receber "Arquivos Extraidos com sucesso "
Else
   Sincronismo_MensagemAoOperador_Receber "Não foi possível extrair os Arquivos"
   Exit Function
End If



Set clLista = Sincronismo_ListaArquivos_Recebido(CurDir & "\" & Dir(cNomeArquivoLista))

If Sincronismo_Receber_VerificarArquivo(clLista) Then
   Sincronismo_MensagemAoOperador_Receber UCase("O arquivo já foi recebido anteriormente")
   If Not bReceberAutomatica Then
       MsgBox "O arquivo já foi recebido anteriormente", vbExclamation
   End If
   Copia_de_Arquivo CurDir & "\trx-recebidas\" & Dir(cNomeArquivoZip), cNomeArquivoZip, frmPrincipal.hwnd, True, True
   Kill cNomeArquivoZip
   Exit Function
End If

If clLista.Count > 0 Then
    Sincronismo_MensagemAoOperador_Receber "Processando arquivos de dados..."
    If Sincronismo_ProcessarArquivosRecebidos(clLista, frmForm, nCodigoLojaAtual) Then
       Sincronismo_MensagemAoOperador_Receber "Arquivos de dados Processados com sucesso"
       Sincronismo_ReceberArquivo = True
    Else
       Sincronismo_MensagemAoOperador_Receber "Não foi possível processar os Arquivos de dados"
    End If
End If



Exit Function
fkjdkfjk:
MsgBox Err.Description
End Function

         
         */
    }
}
