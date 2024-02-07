using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using TransmissorUtil;
using TransmissorLog;


namespace TransmissorDAO
{
    public class DAO
    {
         
        public static String cCaminhoApp = (Util.DiretorioAPP().ToLower().Contains("debug") ? "e:\\dinnamus" : Util.DiretorioAPP()) ; 
        
        private static SqlTransaction trx;

        public static SqlTransaction Trx
        {
            get { return DAO.trx; }
            set { DAO.trx = value; }
        }

        
        public static SqlConnection cnxDinnamuS = new SqlConnection();
        private static DataSet dsTemp = new DataSet();

        public static SqlDataAdapter getDataAdapter
        {
            get { return daTemp; }
            set { DAO.daTemp = value; }

        }

        private static SqlDataAdapter daTemp = null;

        private static Exception eMsg;

        public static Exception EMsg
        {
            get { return DAO.eMsg; }
            set { DAO.eMsg = value; }
        }

        public static bool Executar(String cSQL) {
            return Executar(cSQL, null,null);
        }
        public static bool Executar(SqlCommand cmd) {
            return Executar(null, cmd,null);
        }
        public static bool GravarTRX()
        {
            try
            {

                trx.Commit();


                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                return false;
            }

        }
        public static bool DesfazerTRX()
        {
            try
            {

                trx.Rollback();


                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                return false;
            }

        }
        public static bool IniciarTRX() {
            try
            {

                trx = cnxDinnamuS.BeginTransaction();


                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                return false;
            }
        
        }
        public static bool Executar(String cSQL, SqlCommand cmd, SqlTransaction trx) {
            try
            {
                lock (cnxDinnamuS)
                {
                    SqlCommand Comando = null;
                    if (cmd == null)
                    {
                        Comando = new SqlCommand(cSQL);
                        
                        Comando.Connection = cnxDinnamuS;
                    }
                    else
                    {
                        Comando = cmd;
                        Comando.Connection = cnxDinnamuS;
                    }
                    if (trx!=null)
                    {
                       Comando.Transaction = trx;
                    }
                    else
                    {
                       Comando.Transaction = DAO.Trx;
                    }
                    Comando.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                EMsg = ex;
                return false;
            }
        }
        public static DataSet Pesquisar(String cSQL) {
            return Pesquisar(cSQL, null,null);
        }
        public static DataSet Pesquisar(SqlCommand comandosql) {
            return Pesquisar(null,comandosql,null);
        }
        public static DataSet Pesquisar(String cSQL, SqlCommand comando, SqlTransaction _trx) 
        {
            try
            {
                lock (dsTemp)
                {
                    dsTemp = new DataSet();

                    if (cSQL != null)
                    {
                        SqlCommand cmd = new SqlCommand(cSQL, cnxDinnamuS);
                        if (_trx != null)
                        {
                            cmd.Transaction = _trx;
                        }
                        else
                        {
                            cmd.Transaction = DAO.Trx;
                        }
                        daTemp = new SqlDataAdapter(cmd);

                    }
                    else
                    {
                        comando.Connection = cnxDinnamuS;
                        
                        daTemp = new SqlDataAdapter(comando);
                    }



                    daTemp.Fill(dsTemp);
                }
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                EMsg = ex;
                return null;
                
            }
            return dsTemp;
        }
        public static bool DataSetEditavel_Gravar(DataSet ds, SqlDataAdapter da) {
            return DataSetEditavel_Gravar(ds, da, "", 0);

        }

        public static bool DataSetEditavel_Gravar(DataSet ds, SqlDataAdapter da, String cNomePK, Int32 nValorPK) {

            try
            {
                SqlCommandBuilder objCommandBuilder = new SqlCommandBuilder(da);
                objCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
                
                SqlCommand cmd = objCommandBuilder.GetInsertCommand();
                if (cNomePK!="")
                {
                   cmd.CommandText= cmd.CommandText.Replace(") VALUES",  ",[" + cNomePK + "]) VALUES");
                   cmd.CommandText = cmd.CommandText.Substring(0, cmd.CommandText.Length - 1) + ",@" +cNomePK + ")";
                   cmd.Parameters.Add(new SqlParameter("@" +cNomePK,nValorPK));
                   da.InsertCommand = cmd;
                   objCommandBuilder=new SqlCommandBuilder(da);
                    
                }                 
                da.Update(ds.Tables[0]);                 
                return true;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                EMsg = ex;
                return false;
            }
        }
        public static DataSet DataSetEditavel(String cSQL, String Tabela, ref SqlDataAdapter da) {
            return DataSetEditavel(cSQL, Tabela, ref da, null);
        }

      
        public static DataSet DataSetEditavel(String cSQL , String Tabela, ref SqlDataAdapter da,SqlTransaction trx)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(cSQL, cnxDinnamuS);
                if (trx != null)
                {
                    cmd.Transaction = trx;
                }
                daTemp = new SqlDataAdapter(cmd);
                dsTemp = new DataSet(Tabela);
                daTemp.FillSchema(dsTemp, SchemaType.Source, Tabela);                
                daTemp.Fill(dsTemp,Tabela);
                da = daTemp;
                return dsTemp;
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                EMsg = ex;
                return null;

            }
            
        }

        public static bool IniciarCNX()
        {
            bool bRet = false;
            try
            {


                String cCNX = LerUDLDinnamuS((cCaminhoApp + "\\cnxdinnamus.cnx").Replace("\\\\",""));
                cnxDinnamuS.ConnectionString = cCNX;
                cnxDinnamuS.Open();
                bRet = true;

            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                eMsg = ex;

            }
            return bRet;
        }
        public static int GerarSequenciaTabela(String cNomeTabela) {
            Int32 nRet=0;
            try
            {
                SqlCommand comando= new SqlCommand("NovoCodigoTabela",cnxDinnamuS);
                comando.Parameters.Add(new SqlParameter("@cNomeTabela", cNomeTabela));
                comando.Parameters.Add(new SqlParameter("@nValor", 0));
                comando.Parameters["@nValor"].Direction = ParameterDirection.Output;
                comando.CommandType = CommandType.StoredProcedure;
                comando.Transaction = Trx;
                comando.ExecuteNonQuery();
                nRet = int.Parse( comando.Parameters["@nValor"].Value.ToString());
                    
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                EMsg = ex;
                
            }
            return nRet;
        }
        public static String LerUDLDinnamuS(String cCaminho)
        {
            String cRetorno = "";
            try
            {
                String cDadosArquivo = "";
                StreamReader f = new StreamReader(cCaminho);
                while (cDadosArquivo != null)
                {
                    cDadosArquivo = f.ReadLine();
                    if (cDadosArquivo != null)
                    {
                        cRetorno += cDadosArquivo;
                    }
                }

                if (cRetorno.Length > 0)
                {
                    cRetorno = cRetorno.Replace("[oledb]", "").Replace("; Everything after this line is an OLE DB initstring", "").Replace("Provider=SQLOLEDB.1", "");
                }

            }
            catch (Exception ex) { Log.Registrar("ERRO : " + ex.Message); }
            return cRetorno;
        }
    }
}
