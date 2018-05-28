using Ferreteria.COMMON.Entidades;
using Ferreteria.COMMON.interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferreteria.BIZ
{
    public class ManejadorCategoria : IManejadorCategoria
    {
        IRepositorio<Categoria> repositorio;
        public ManejadorCategoria(IRepositorio<Categoria> repositorio)
        {
            this.repositorio = repositorio;

        }
        public List<Categoria> Listar => repositorio.Leer;

        public bool Agregar(Categoria entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Categoria BuscarIdentificador(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Categoria entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
