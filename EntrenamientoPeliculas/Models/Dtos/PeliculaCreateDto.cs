using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static EntrenamientoPeliculas.Models.Pelicula;

namespace EntrenamientoPeliculas.Models.Dtos
{
    public class PeliculaCreateDto
    {
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La dscripcion es obligatoria")]
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public IFormFile Foto { get; set; }
        public string RutaImagen { get; set; }
        public TipoDescripcion Clasificacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public int CategoriaId { get; set; }
    }
}
