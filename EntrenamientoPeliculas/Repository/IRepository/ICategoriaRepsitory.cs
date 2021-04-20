using EntrenamientoPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Repository.IRepository
{
    public interface ICategoriaRepsitory
    {
        ICollection<Categoria> GetCategorias();
        Categoria GetCategoria(int id);
        bool ExisteCategoria(string nombre);
        bool ExisteCategoria(int id);
        bool CrearCategoria(Categoria categoria);
        bool ActualizarCategoria(Categoria categoria);
        bool BorrarCategoria(Categoria categoria);
        bool Guardar();
    }
}
