using Ferreteria.COMMON.Entidades;
using Ferreteria.COMMON.interfaces;
using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ferreteria.BIZ
{
    public class ManejadorVales : IManejadorVales
    {
        IRepositorio<Vale> repositorio;
        public ManejadorVales(IRepositorio<Vale> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<Vale> Listar => repositorio.Leer;

        public bool Agregar(Vale entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Vale BuscarIdentificador(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

       

        public bool Eliminar(ObjectId id)
        {
           
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Vale entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
