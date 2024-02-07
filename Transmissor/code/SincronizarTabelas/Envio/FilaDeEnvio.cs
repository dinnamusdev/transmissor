using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransmissorLog;
using TransmissorDAO;
using System.Data;
using System.Data.SqlClient;
using TransmissorUtil;
namespace FilaEnvio
{
    class FilaDeEnvio
    {
        public static String Msg = "";
        public static Int32 ColocarNaFila(FilaEnvioArquivo _FilaEnvioArquivo) {
            Int32 nRetorno = 0;
            try
            {
                
                Int32 nID =0;
                Int32 nOrdem  =0;
                DataSet rstTmp = new DataSet();
                Int32 nIdArquivo = 0;
                /*
                if (DAO.Pesquisar("select 1 from Transmissor_Enviar_Arquivos where idregistro=" + _FilaEnvioArquivo.IDRegistro + " and nomemodulo='" + _FilaEnvioArquivo.NomeModulo + "'").Tables[0].Rows.Count > 0)
                {
                    Msg = "A " + _FilaEnvioArquivo.NomeModulo + " id: " + _FilaEnvioArquivo.IDRegistro + " já foi colocado na fila. Envio não autorizado";
                    return 0;
                }
                */
                nIdArquivo = DAO.GerarSequenciaTabela( "Transmissor_Enviar_Arquivos");

                nOrdem = int.Parse( DAO.Pesquisar("select isnull(max(ordem),0) +1 ordem from Transmissor_Enviar_arquivos").Tables[0].Rows[0]["ordem"].ToString()) ;


                String cSQL =
                 "INSERT INTO Transmissor_Enviar_Arquivos " +
                "(id, Data, NomeModulo, IDRegistro, NomeArquivo,ordem,loja,arquivo) values (" + 
                nIdArquivo + ", '" + _FilaEnvioArquivo.Data.ToString("yyyy-MM-dd hh:mm:ss") + "', '" +
                _FilaEnvioArquivo.NomeModulo + "', " + _FilaEnvioArquivo.IDRegistro + ", '" 
                + Util.DiretorioAPP() +"\\"+ _FilaEnvioArquivo.NomeArquivo + "'," +
                nOrdem + "," + _FilaEnvioArquivo.Loja + ",@arquivo)";

                SqlCommand cmd = new SqlCommand(cSQL);
                cmd.Transaction = DAO.Trx;
                cmd.Parameters.Add(new SqlParameter("@arquivo",SqlDbType.Image));
                cmd.Parameters["@arquivo"].Value = Util.FileToBlob(_FilaEnvioArquivo.NomeArquivo);
      
                if( DAO.Executar(cmd) ){    
                    Int32 nIdArqTransmitir =0;
                    for (int i = 0; i < _FilaEnvioArquivo.clFilaEnvioArquivoTRX.Count; i++)
	                {

                        nIdArqTransmitir = DAO.GerarSequenciaTabela("Transmissor_Enviar_Arquivos_Transmitir");
        
                        cSQL = "INSERT INTO Transmissor_Enviar_Arquivos_Transmitir " +
                                    "(id, IdArquivo, DataHoraEnviar, LojaDestino, Email)" +
                                      "VALUES (" + nIdArqTransmitir + ", " + nIdArquivo + "," +
                                      (_FilaEnvioArquivo.clFilaEnvioArquivoTRX[i].DataHoraEnviar == DateTime.Parse("2001-01-01 00:00:00") ? "null" : "'") + 
                                      (_FilaEnvioArquivo.clFilaEnvioArquivoTRX[i].DataHoraEnviar.ToString("yyyy-MM-dd hh:mm:ss") + "'") + ", " +
                                      _FilaEnvioArquivo.clFilaEnvioArquivoTRX[i].LojaDestino + ", '" +
                                      _FilaEnvioArquivo.clFilaEnvioArquivoTRX[i].Email + "') ";
               
                        if (!DAO.Executar(cSQL)) {
                            return 0;
                        }
                    }
                }
                nRetorno = nIdArquivo;

            }
            catch (Exception ex)
            {
                
                 Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
            }
            return nRetorno;
        }

       
    }
}
