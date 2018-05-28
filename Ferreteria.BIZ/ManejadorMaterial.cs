using Ferreteria.COMMON.Entidades;
using Ferreteria.COMMON.interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferreteria.BIZ
{
    public class ManejadorMaterial : IManejadorMaterial
    {
        IRepositorio<Material> repositorio;
        public ManejadorMaterial(IRepositorio<Material> repositorio)
        {
            this.repositorio = repositorio;
        }

        public List<Material> Listar => repositorio.Leer;

        public bool Agregar(Material entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Material BuscarIdentificador(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Material entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
