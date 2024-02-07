using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Transmissor.code;
using TransmissorLog;
using TransmissorUtil;

namespace Transmissor
{
    public partial class frmEnviar : Form
    {
        public frmEnviar()
        {
            InitializeComponent();
        }
        private bool IniciarUI() {

            dbgEnvio.AutoGenerateColumns = false;
            dbgEnvio_OK.AutoGenerateColumns = false;
            dbgFilaDestino.AutoGenerateColumns = false;
            dbgFilaDestino_OK.AutoGenerateColumns = false;
            if (!AtualizarGrid_Envio()) {
                return false;
            }

            return true;
        
        }
        private bool AtualizarGrid_Envio() {

            try
            {
               dbgEnvio.DataSource = Envio.ListarFilaEnvio("", TransmissorApp.nCodigoLoja).Tables[0];
               dbgEnvio_OK.DataSource = Envio.ListarFilaEnvio("ENVIADO", TransmissorApp.nCodigoLoja).Tables[0];
               return true;
            }
            catch (Exception ex)
            {
                
               Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               return false;
            }
        }
        private void btFechar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarGrid_Envio();
        }

        private void frmEnviar_Load(object sender, EventArgs e)
        {
            if (!IniciarUI()) {
                this.Dispose();
            }
        }

        private void dbgEnvio_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dbgEnvio_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dbgEnvio_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Int16 nLinha = Int16.Parse(dbgEnvio.CurrentRow.Index.ToString());

                if (nLinha>=0)
                {
                    DataTable dt = (DataTable)dbgEnvio.DataSource;
                    Int32 nIdArquivo =  Int32.Parse( dt.Rows[nLinha]["id"].ToString());
                    dbgFilaDestino.DataSource = Envio.ListarFilaEnvio_Destino(nIdArquivo, "").Tables[0];
                }
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dbgEnvio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dbgEnvio_OK_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Int16 nLinha = Int16.Parse(dbgEnvio_OK.CurrentRow.Index.ToString());

                if (nLinha >= 0)
                {
                    DataTable dt = (DataTable)dbgEnvio_OK.DataSource;
                    Int32 nIdArquivo = Int32.Parse(dt.Rows[nLinha]["id"].ToString());
                    dbgFilaDestino_OK.DataSource = Envio.ListarFilaEnvio_Destino(nIdArquivo, "ENVIADO").Tables[0];
                }
            }
            catch (Exception ex)
            {
                Log.Registrar("ERRO : " + ex.Message);
                Util.Mensagem(ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}