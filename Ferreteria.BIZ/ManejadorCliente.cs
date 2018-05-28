using Ferreteria.COMMON.Entidades;
using Ferreteria.COMMON.interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferreteria.BIZ
{
    public class ManejadorCliente : IManejadorClientes
    {
        IRepositorio<Cliente> repositorio;
        public ManejadorCliente(IRepositorio<Cliente> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<Cliente> Listar => repositorio.Leer;

        public bool Agregar(Cliente entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Cliente BuscarIdentificador(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Cliente entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
