using Ferreteria.COMMON.Entidades;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ferreteria.COMMON.interfaces
{
    public interface IManejadorGenerico<T> where T:Base
    {
        bool Agregar(T entidad);
        bool Modificar(T entidad);
        bool Eliminar(ObjectId id);
        List<T> Listar { get; }
        T BuscarIdentificador(ObjectId id);
    }
}
