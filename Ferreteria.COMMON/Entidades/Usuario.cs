using System;
using System.Collections.Generic;
using System.Text;

namespace Ferreteria.COMMON.Entidades
{
    public class Usuario : Base
    {
        public string UsuarioNombre { get; set; }
        public string Direccion { get; set; }
        public string Apellidos { get; set; }
        public string Contraseña { get; set; }
        public byte[] FotoGrafia { get; set; }
        public override string ToString()
        {
            return UsuarioNombre;

        }
    }
}
