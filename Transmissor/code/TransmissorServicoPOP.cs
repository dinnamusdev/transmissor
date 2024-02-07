using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailDinnamuS;

namespace Transmissor.code
{
    class TransmissorServicoPOP
    {
        public static Pop _Pop = new Pop();

        public static bool Conectado() {
            
            return _Pop.Conectado;

            
        }
        public static void Fechar() {
            _Pop.Disconectar();
        }

        public static String MensagemErro()
        {
           return _Pop.MensagemErro();
        }
        public static bool Iniciar()
        {
            return Iniciar(false);
        }
        public static bool Iniciar(bool ForcaFechamento) {
            try
            {
                if (_Pop.EstaConectado() )
                {
                    if (!ForcaFechamento)
                    {
                        return true;
                    }
                    else {
                        _Pop.Dispose();
                    }
                    
                }
                else {

                    _Pop.Dispose();
                    //_Pop.Disconectar();
                    
                }
                String Servidor = TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["pop_servidor"].ToString();
                int Porta = int.Parse(TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["pop_porta"].ToString());
                String Usuario = TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["pop_usuario"].ToString();
                String Senha = TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["pop_senha"].ToString();
                Boolean SSL = Boolean.Parse(TransmissorCFG.dsConfiguracao.Tables[0].Rows[0]["pop_ssl"].ToString());
                _Pop = new Pop();
                 _Pop.Conectar(Servidor,Porta,SSL, Usuario, Senha);
                 return _Pop.EstaConectado();
            }
            catch (Exception ex)
            {

                return false;
            }
        
        }
    }
}
