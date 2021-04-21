using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public string RutaImagen { get; set; }
        public enum TipoDescripcion  { Siete, Trece, Dieciseis, Dieciocho }
        public TipoDescripcion Clasificacion { get; set; }
        public DateTime FechaCreacion { get; set; }

        //ForeinKey
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria categoria { get; set; }

    }
}
