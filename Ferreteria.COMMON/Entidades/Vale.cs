using System;
using System.Collections.Generic;
using System.Text;

namespace Ferreteria.COMMON.Entidades
{
    public class Vale:Base
    {
        public Cliente NombreCliente { get; set; }
        public Usuario NombreEmpleado { get; set; }
        public string Fecha { get; set; }
        public string Folio { get; set; }
        public List<Material> MaterialesVendidos { get; set; }
        public string Total { get; set; }
       // public byte[] FotoGrafia { get; set; }

    }
}
