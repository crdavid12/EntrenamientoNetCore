using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Models.Dtos
{
    public class UsuarioCreateDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "La contraseña debe contener entre 4 y 10 carateres")]
        public string Password { get; set; }

    }
}
