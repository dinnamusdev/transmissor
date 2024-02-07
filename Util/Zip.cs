using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using TransmissorUtil;

namespace TransmissorUtilZip
{
    public class Zip
    {
        
        public static List<String> DesCompactar(String cArquivoZIP, String Destino)
        {
            List<String> clLista = new List<string>();
            try
            {                
                using (ZipFile zip = ZipFile.Read(cArquivoZIP))
                {
                    foreach (ZipEntry e in zip) {

                         
                        if (System.IO.File.Exists(e.FileName))
                        {
                            Util.ExcluirArquivo(e.FileName);
                        }
                        e.Extract(Destino);

                        clLista.Add(e.FileName);
                    }
                }
            }
            catch (Exception ex)
            {                
                return null;
            }
            return clLista;
        }
        public static bool Compactar(String cArquivoZIP, List<String> clLista) {
            try
            {
                ZipFile zip = new ZipFile();
                for (int i = 0; i < clLista.Count;  i++)
                {
                    zip.AddFile(clLista[i]);
                }

                zip.Save(cArquivoZIP);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
