using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EntrenamientoPeliculas.Models.Pelicula;

namespace EntrenamientoPeliculas.Models.Dtos
{
    public class PeliculaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public string RutaImagen { get; set; }
        public TipoDescripcion Clasificacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        public int CategoriaId { get; set; }
        public Categoria categoria { get; set; }
    }
}
