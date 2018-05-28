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
    public class ManejadorUsuario : IManejadorUsuario
    {
        IRepositorio<Usuario> repositorio;
        public ManejadorUsuario(IRepositorio<Usuario> repositorio)
        {
            this.repositorio = repositorio;
        }

        public List<Usuario> Listar => repositorio.Leer;

        public bool Agregar(Usuario entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Usuario BuscarIdentificador(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public IEnumerable BuscarVentasRealizadasPorEmpleado(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Usuario entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
