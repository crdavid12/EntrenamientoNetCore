using EntrenamientoPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Repository.IRepository
{
    public interface IPeliculaRepository
    {
        IEnumerable<Pelicula> Getpeliculas();
        Pelicula GetPelicula(int peliculaId);
        IEnumerable<Pelicula> GetPeliculasEnCategoria(int Id);
        IEnumerable<Pelicula> BuscarPelicula(string nombre);
        bool ExistePelicula(string nombre);
        bool ExistePelicula(int peliculaId);
        bool CrearPelicula(Pelicula pelicula);
        bool ActualizarPelicula(Pelicula pelicula);
        bool BorrarPelicula(Pelicula pelicula);
        bool Guardar();
    }
}
