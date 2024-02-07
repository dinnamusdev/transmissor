using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilaEnvio
{
    class FilaEnvioArquivo
    {
        public DateTime Data =new DateTime();
        public String NomeModulo="" ;
        public Int32 IDRegistro=0 ;
        public String NomeArquivo="" ;
        public List<FilaEnvioArquivoTRX> clFilaEnvioArquivoTRX = new List<FilaEnvioArquivoTRX>();
        public Int16 Loja=0;
    }
}
