using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Models.Dtos
{
    public class UsuarioDto
    {
        public string NombreUsuario { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
