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
        
        [Required (ErrorMessage ="El Usuario es obligatorio")]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(10, MinimumLength =4, ErrorMessage = "La contrasaña debe contener entre 4 y 10 caracteres")]
        public string Password { get; set; }
    }
}
