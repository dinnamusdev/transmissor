using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransmissorDAO;
using TransmissorUtil;

namespace SincronizadorDeTabelas
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            DAO.IniciarCNX();
            String cRetorno= EnviarMetodosBasicos.Sincronismo_ProcessarSincronismo(Util.DiretorioAPP(), DateTime.Today, 1, 0, false, false);            
            DAO.cnxDinnamuS.Dispose();
            DAO.cnxDinnamuS.Close();
            System.Console.Out.Write(cRetorno);
            System.Console.In.ReadLine();
            
        }
    }
}
