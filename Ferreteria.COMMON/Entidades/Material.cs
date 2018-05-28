using System;
using System.Collections.Generic;
using System.Text;

namespace Ferreteria.COMMON.Entidades
{
    public class Material : Base
    {
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public float PrecioDeAdquisicion { get; set; }
        public float PrecioDeVenta { get; set; }
        public string DescripcionDeMaterial { get; set; }
        public byte[] FotoGrafia { get; set; }
    }
}
