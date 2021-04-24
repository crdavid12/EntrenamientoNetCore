using EntrenamientoPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Repository.IRepository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int usuarioId);
        bool ExisteUsuario(int usuarioId);
        bool ExisteUsuario(string nombre);
        Usuario CrearUsuario(Usuario usuario, string password);
        Usuario Login(string nombre, string password);
        bool Guardar();
    }
}
