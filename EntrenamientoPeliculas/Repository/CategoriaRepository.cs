using EntrenamientoPeliculas.Data;
using EntrenamientoPeliculas.Models;
using EntrenamientoPeliculas.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Repository
{
    public class CategoriaRepository : ICategoriaRepsitory
    {
        private readonly ApplicationDbContext _bd;

        public CategoriaRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }


        public bool ActualizarCategoria(Categoria categoria)
        {
            _bd.Categoria.Update(categoria);
            return Guardar();
        }

        public bool BorrarCategoria(Categoria categoria)
        {
            _bd.Categoria.Remove(categoria);
            return Guardar();
        }

        public bool CrearCategoria(Categoria categoria)
        {
            _bd.Categoria.Add(categoria);
            return Guardar();
        }

        public bool ExisteCategoria(string nombre)
        {
            var item = _bd.Categoria.Any(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            return item;
        }

        public bool ExisteCategoria(int id)
        {
            var item = _bd.Categoria.Any(x => x.Id == id);
            return item;
        }

        public Categoria GetCategoria(int id)
        {
            return _bd.Categoria.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Categoria> GetCategorias()
        {
            return _bd.Categoria.OrderBy(x => x.Id).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }
    }
}
