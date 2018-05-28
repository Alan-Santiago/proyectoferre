using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ferreteria.COMMON.Entidades
{
   public  class Tiquet
    {
        public string Documento { get; set; }
        public Tiquet(string documento)
        {
            Documento = documento;

        }
        public bool Guardar(string elementos)
        {
            try
            {
                StreamWriter fila = new StreamWriter(Documento);
                fila.Write(elementos);
                fila.Close();
                return true;

            }
            catch (Exception)
            {
                return false;

            }
        }
        public string Leer()
        {
            try
            {
                StreamReader fila = new StreamReader(Documento);
                string elementos = fila.ReadToEnd();
                fila.Close();
                return elementos;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

