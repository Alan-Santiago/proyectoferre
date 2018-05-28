using Ferreteria.COMMON.Entidades;
using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ferreteria.COMMON.interfaces
{
    public interface IManejadorUsuario : IManejadorGenerico<Usuario>
    {
        IEnumerable BuscarVentasRealizadasPorEmpleado(ObjectId id);
    }
}
