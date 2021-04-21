using EntrenamientoPeliculas.Data;
using EntrenamientoPeliculas.Models;
using EntrenamientoPeliculas.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Repository
{
    public class peliculaRepository : IPeliculaRepository
    {
        private readonly ApplicationDbContext _bd;

        public peliculaRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public bool ActualizarPelicula(Pelicula pelicula)
        {
            _bd.Pelicula.Update(pelicula);
            return Guardar();
        }

        public bool BorrarPelicula(Pelicula pelicula)
        {
            _bd.Pelicula.Remove(pelicula);
            return Guardar();
        }

        public IEnumerable<Pelicula> BuscarPelicula(string nombre)
        {
            IQueryable<Pelicula> query = _bd.Pelicula;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(x => x.Nombre.Contains(nombre) || x.Descripcion.Contains(nombre));
            }
            return query.ToList();
        }

        public bool CrearPelicula(Pelicula pelicula)
        {
            _bd.Pelicula.Add(pelicula);
            return Guardar();
        }

        public bool ExistePelicula(string nombre)
        {
            return _bd.Pelicula.Any(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());

        }

        public bool ExistePelicula(int peliculaId)
        {
            return _bd.Pelicula.Any(x => x.Id == peliculaId);
        }

        public IEnumerable<Pelicula> Getpeliculas()
        {
            return _bd.Pelicula.OrderBy(x => x.Id).ToList();
        }

        public Pelicula GetPelicula(int peliculaId)
        {
            return _bd.Pelicula.FirstOrDefault(x => x.Id == peliculaId);
        }

        public IEnumerable<Pelicula> GetPeliculasEnCategoria(int Id)
        {
            return _bd.Pelicula.Include(ca => ca.categoria).Where(ca => ca.CategoriaId == Id).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
