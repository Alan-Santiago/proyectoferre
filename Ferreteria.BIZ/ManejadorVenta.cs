using Ferreteria.COMMON.Entidades;
using Ferreteria.COMMON.interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferreteria.BIZ
{
    public class ManejadorVenta : IManejadorVenta
    {
        IRepositorio<Venta> repositorio;

        public ManejadorVenta(IRepositorio<Venta> repositorio)
        {
            this.repositorio = repositorio;
        }

        public List<Venta> Listar => repositorio.Leer;

        public bool Agregar(Venta entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Venta BuscarIdentificador(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();

        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Venta entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
