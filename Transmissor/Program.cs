using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Transmissor.code.SincronizarTabelas.Recepcao;

namespace Transmissor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
           Application.Run(new frmPrincipal());
            
            //TransmissorDAO.DAO.IniciarCNX();

            //ReceberMetodosBasicos.Sincronismo_ReceberArquivo(47,

            //TransmissorDAO.DAO.cnxDinnamuS.Close();
        }
    }
}
