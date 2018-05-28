using System;
using System.Collections.Generic;
using System.Text;

namespace Ferreteria.COMMON.Entidades
{
    public class Categoria: Base
    {
        public string NombreTipoDeMaterial { get; set; }
        //public byte[] FotoGrafia { get; set; }
        public override string ToString()
        {
            return NombreTipoDeMaterial ;
        }
    }
}
