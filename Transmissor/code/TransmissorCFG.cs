using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using TransmissorDAO;
using TransmissorLog;
using TransmissorUtil;

namespace Transmissor.code
{
    class TransmissorCFG
    {
        public TransmissorCFG() { 
            
        }
        public static SqlDataAdapter daConfiguracao = new SqlDataAdapter();
        public static DataSet dsConfiguracao = new DataSet();
        public  static DataSet CarregarCFG(int nCodigoLoja)
        {
            try
            {

                /*String csql = "SELECT     Loja, Smtp_Servidor, Smtp_Porta, Smtp_Usuario, Smtp_Senha, ISNULL(Smtp_Autenticar, 0) AS Smtp_Autenticar, ISNULL(Smtp_SSL, 0) AS Smtp_SSL, " +
                      "ISNULL(Smtp_OK, 0) AS smtp_ok, Pop_Servidor, Pop_Porta, Pop_Usuario, Pop_Senha, ISNULL(Pop_SSL, 0) AS Pop_SSL, ISNULL(Pop_OK, 0)  " +
                      "AS Pop_OK FROM Transmissor_Enviar_Arquivo_CFG where loja=" + nCodigoLoja, "Transmissor_Enviar_Arquivo_CFG";
                */

                return DAO.DataSetEditavel("SELECT     Loja, Smtp_Servidor, Smtp_Porta, Smtp_Usuario, Smtp_Senha, Smtp_AutenticaR, Smtp_SSL, " +
                      "Smtp_OK, Pop_Servidor, Pop_Porta, Pop_Usuario, Pop_Senha, Pop_SSL, Pop_OK  " +
                      " FROM Transmissor_Enviar_Arquivo_CFG where loja=" + nCodigoLoja, "Transmissor_Enviar_Arquivo_CFG", ref daConfiguracao);
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

        }
        public static DataSet CFG_ListarEmail(int nCodigoLoja) {
            try
            {

                String cSql = "SELECT  email.LojaAtual, l.codigo,  l.Nome, email.EmailPrincipal "+
                    "FROM Transmissor_Enviar_Arquivo_CFG_EmailLojas email INNER JOIN " +
                      "Lojas l ON email.Loja = l.codigo where email.lojaatual=" + nCodigoLoja;


                return DAO.Pesquisar(cSql);
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        
        }
        public static bool CFG_Excluir_Email(int nLojaAtual, int nLoja) {
            try
            {
                return DAO.Executar("delete Transmissor_Enviar_Arquivo_CFG_EmailLojas where loja="+  nLoja +" and lojaatual=" +nLojaAtual);
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        public static bool CFG_Incluir_Email(int nLojaAtual, int nLoja, String EmailPrincipal, String EmailSecundario) {

            try
            {
                if (DAO.Pesquisar("select 1 from Transmissor_Enviar_Arquivo_CFG_EmailLojas where LojaAtual=" + nLojaAtual + " and loja=" + nLoja).Tables[0].Rows.Count == 0)
                {
                    String cSQL = "INSERT INTO Transmissor_Enviar_Arquivo_CFG_EmailLojas" +
                          "(LojaAtual, Loja, EmailPrincipal, EmailSecundario)" +
                           "VALUES(" + nLojaAtual + ", " + nLoja + ", '" + EmailPrincipal + "', '" + EmailSecundario + "')";

                    return DAO.Executar(cSQL);
                }
                else {
                    Util.Mensagem("Já existe um email atribuido a loja informada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                 Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 return false;
            }
        
        }
        public static bool CFG_Atualizar(int nCodigoLoja, SMTP_CFG _SMTP_CFG, POP_CFG _POP_CFG) {
            try
            {

                String cSQL = "update Transmissor_Enviar_Arquivo_CFG set " +                      
                      ", ) Smtp_Servidor='" + _SMTP_CFG.Servidor + "', Smtp_Porta="
                      + _SMTP_CFG.Porta +
                      ", Smtp_Usuario='" + _SMTP_CFG.Usuario + "', Smtp_Senha='" 
                      + _SMTP_CFG.Senha +
                      "', Smtp_Autenticar=" + (_SMTP_CFG.Autenticar ? "1" : "0") +
                      ", Smtp_SSL=" + (_SMTP_CFG.SSL ? "1" : "0") +
                      ", Smtp_OK=" + (_SMTP_CFG.Conectado ? "1" : "0") +
                      ", Pop_Servidor='" + _POP_CFG.Servidor + "', Pop_Porta='" + _POP_CFG.Porta +
                      "', Pop_Usuario='" + _POP_CFG.Usuario + "', Pop_Senha='" + _POP_CFG.Senha + "',Pop_SSL=" +
                      (_POP_CFG.SSL ? "1" : "0") + ", Pop_OK=" + (_POP_CFG.Conectado ? "1" : "0") + 
                      " where loja=" + nCodigoLoja;

                return DAO.Executar(cSQL);

                
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }    
        
        
        }
        public static bool CFG_Iniciar(int nCodigoLoja)
        {
            try
            {

                return DAO.Executar("insert into Transmissor_Enviar_Arquivo_CFG(Loja,Pop_ok,smtp_ok,Smtp_Autenticar,Smtp_SSL,Pop_SSL) values(" + nCodigoLoja + ",0,0,0,0,0)");

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
