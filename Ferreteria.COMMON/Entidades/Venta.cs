using System;
using System.Collections.Generic;
using System.Text;

namespace Ferreteria.COMMON.Entidades
{
    public class Venta:Base
    {
        public string Producto { get; set; }
        public string Categoria { get; set; }
        public float Costo { get; set; }
        public float Total { get; set; }
        public int cantidad { get; set; }

    }
}
