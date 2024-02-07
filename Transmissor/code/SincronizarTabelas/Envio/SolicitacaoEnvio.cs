using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransmissorLog;
using TransmissorDAO;
using System.Data;
using FilaEnvio;
using TransmissorUtil;
using System.Data.SqlClient;

namespace SincronizadorDeTabelas
{
    class SolicitacaoEnvio
    {
        private static DataSet Solicitacoes(Int16 nCodigoLoja/*, SqlTransaction trx*/)
        {
            DataSet ds= null;
            try
            {
                ds = TransmissorDAO.DAO.Pesquisar("select * from Transmissor_SolicitacaoEnvioMovimento where status is null and loja=" + nCodigoLoja);

            }
            catch (Exception ex)
            {

                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                
            }
            return ds;
        }
        public static bool AtualizarStatusSolicitacao(Int32 nIdSolicitacao, String Obs) {
            try
            {
                return DAO.Executar("update Transmissor_SolicitacaoEnvioMovimento set status='OK',DataHoraEnvio=getdate(), obs='" + Obs + "' where id=" + nIdSolicitacao);
                
            }
            catch (Exception ex)
            {
                
                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }
        private static String DadosEmail(Int16 nCodigoLojaAtual, Int16 nCodigoLojaDestino ) {
            String Retorno = "";
            try{
                
                DataSet ds = DAO.Pesquisar("select * from Transmissor_Enviar_Arquivo_CFG_EmailLojas where lojaatual=" + nCodigoLojaAtual + " and loja=" + nCodigoLojaDestino);
                if (ds!=null)
	            {
                    if (ds.Tables[0].Rows.Count>0)
                    {
                        Retorno = ds.Tables[0].Rows[0]["emailprincipal"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
            }
            return Retorno;
        }
        private static bool MontarFilaParaEnvio(Int16 nLojaOrigem, String LojasDestino, String Arquivo, Int32 nIDMov) {
            try
            {  
                string[] Lojas =LojasDestino.Split(',');
                for (int i = 0; i < Lojas.Length; i++)
                {
                    FilaEnvioArquivo _FilaEnvioArquivo = new FilaEnvioArquivo();
                    _FilaEnvioArquivo.Data = DateTime.Now;
                    _FilaEnvioArquivo.Loja = nLojaOrigem;
                    _FilaEnvioArquivo.NomeArquivo = Arquivo;
                    _FilaEnvioArquivo.NomeModulo = "MOVIMENTO";
                    _FilaEnvioArquivo.IDRegistro = nIDMov;
                    FilaEnvioArquivoTRX _FilaEnvioArquivoTRX = new FilaEnvioArquivoTRX();
                    _FilaEnvioArquivoTRX.DataHoraEnviar = _FilaEnvioArquivo.Data;
                    _FilaEnvioArquivoTRX.LojaDestino = Int16.Parse( Lojas[i]);
                    _FilaEnvioArquivoTRX.Email = DadosEmail(nLojaOrigem, _FilaEnvioArquivoTRX.LojaDestino);
                    _FilaEnvioArquivo.clFilaEnvioArquivoTRX.Add(_FilaEnvioArquivoTRX);
                    if (FilaDeEnvio.ColocarNaFila(_FilaEnvioArquivo)<=0)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                
                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                return false;

            }
        }
        public static bool Verificar(Int16 nCodigoLoja) {
            try
            {
                //System.Data.SqlClient.SqlTransaction trxmov = DAO.cnxDinnamuS.BeginTransaction("verificarmov");
                //DAO.Trx = trxmov;
                DataSet ds = Solicitacoes(nCodigoLoja/*trxmov*/);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        //DAO.Trx.Save("VerificacaoMovimento");
                        if (Verificar_Acao(nCodigoLoja,ds))
                        {
                            //trxmov.Commit();
                                                     
                        }
                        else
                        {
                            //trxmov.Rollback("VerificacaoMovimento");
                            //trxmov.Dispose();   
                            return false;
                        }
                    }
                }
                //trxmov.Dispose();  
                return true;
            }
            catch (Exception ex)
            {                
                  Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                  return false;
            }
        }   

        public static bool Verificar_Acao(Int16 nCodigoLoja, DataSet ds) {
            try
            {
                //DataSet ds = Solicitacoes(nCodigoLoja,);
                String LojasDestino="";
                if (ds!=null)
                {
                    if (ds.Tables[0].Rows.Count>0)
                    {
                        String cRetornoVerificacaoAlteracoes = "";
                        cRetornoVerificacaoAlteracoes =
                            EnviarMetodosBasicos.Sincronismo_ProcessarSincronismo(
                            TransmissorUtil.Util.DiretorioAPP(),
                            DateTime.Parse("2001-01-01 00:00:00"),
                            nCodigoLoja,0, false, false
                        );

                        if (cRetornoVerificacaoAlteracoes!="")
                        {
                            Int32 nID = Int32.Parse(ds.Tables[0].Rows[0]["id"].ToString());

                            String[] DadosRetorno = cRetornoVerificacaoAlteracoes.Split("|#|".ToCharArray());
                            String cNomeArquivo = DadosRetorno[0];
                            String nIDRegistros = DadosRetorno[DadosRetorno.Length - 1].Replace("id:", "");

                            if (!AtualizarStatusSolicitacao(nID, cNomeArquivo))
                            {
                                return false;                                    
                            }

                            if (!cRetornoVerificacaoAlteracoes.Contains("Não ocorreram"))
                            {
                                LojasDestino = ds.Tables[0].Rows[0]["LojasDestino"].ToString();
                                if (!MontarFilaParaEnvio(nCodigoLoja, LojasDestino, cNomeArquivo, Int32.Parse(nIDRegistros)))
                                {
                                    return false;
                                }
                                Util.ExcluirArquivo(cNomeArquivo);
                            }
                        }
                    }
                }                
                return true;
            }
            catch (Exception ex)
            {                
                Log.Registrar("ERRO : " + ex.Message + " " + ex.StackTrace);
                return false;
            }
        }
    }
}
