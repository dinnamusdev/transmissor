using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transmissor.code
{
    class SMTP_CFG
    {
        public String Servidor="";
        public int Porta=0;
        public String Usuario = "";
        public String Senha = "";
        public Boolean SSL = false;
        public Boolean Autenticar = false;
        public bool Conectado = false;
    }
}
