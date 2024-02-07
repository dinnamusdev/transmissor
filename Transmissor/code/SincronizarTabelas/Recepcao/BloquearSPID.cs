using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace BloquearSPIDUtil
{
    class BloquearSPID
    {
        private SqlConnection dDaoBloqSpid = new SqlConnection();
        private SqlConnection CnxApp = new SqlConnection();
        private SqlTransaction transacao = null;
        public bool Iniciar(SqlConnection CnxApp)
        {
            try
            {
                dDaoBloqSpid.ConnectionString = CnxApp.ConnectionString;
                
                dDaoBloqSpid.Open();

                this.CnxApp = CnxApp;

                return true;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        
         public Int16  SpidAtual()
         {
             Int16 SpidAtual = 0;
             try
             {
                 DataSet ds = new DataSet();

                 SqlCommand cmd =new SqlCommand("select @@SPID SpidAtual");
                 cmd.Connection = CnxApp;
                 Object RetornoQuery= cmd.ExecuteScalar();

                 if (RetornoQuery.ToString().Length>0)
                 {
                     SpidAtual = Int16.Parse(RetornoQuery.ToString());
                 }

                 //SpidAtual = cnConexao.Execute("select @@SPID SpidAtual").Fields("SpidAtual").value;
             }
             catch (Exception)
             {
                 
                 throw;
             }

             return SpidAtual;

         }
         public bool BloquearSpid(Int16 nSpid)
         {
             try
             {
                 transacao = dDaoBloqSpid.BeginTransaction();
                 SqlCommand cmd = new SqlCommand("insert into Sincronismo_SpidBloqueados(Processo) values (" + nSpid + ")");
                 cmd.Connection = dDaoBloqSpid;
                 cmd.Transaction = transacao;
                 cmd.ExecuteNonQuery();


                 return true;
             }
             catch (Exception ex)
             {

                 return false;
             }
         }

         public bool LiberarSpid()
         {

            try
            {
                transacao.Rollback();
                

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
              
        }
         public bool Terminar()
         {
            try
            {
                dDaoBloqSpid.Close();
                return true ;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        
    }
}
