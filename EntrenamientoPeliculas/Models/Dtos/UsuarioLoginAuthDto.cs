using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Models.Dtos
{
    public class UsuarioLoginAuthDto
    {
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string Password { get; set; }
    }
}
