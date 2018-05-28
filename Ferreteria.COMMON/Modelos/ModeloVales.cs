using Ferreteria.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ferreteria.COMMON.Modelos
{
    public class ModeloVales
    {
        public string Nombre { get; set; }
        public string Empleado { get; set; }
        public string Fecha { get; set; }
        public string Folio { get; set; }
        public string Total { get; set; }
        public ModeloVales(Vale vale)
        {
            Nombre = string.Format("{0}", vale.NombreCliente);
            Empleado = string.Format("{0}", vale.NombreEmpleado);
            Fecha = string.Format("{0}", vale.Fecha);
            Folio = string.Format("{0}", vale.Folio);
            Total = string.Format("{0}", vale.Total);
        }
    }
}
