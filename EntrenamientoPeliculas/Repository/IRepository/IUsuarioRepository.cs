using EntrenamientoPeliculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Repository.IRepository
{
    public interface IUsuarioRepository
    {
<<<<<<< HEAD
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int usuarioId);
        bool ExisteUsuario(int usuarioId);
        bool ExisteUsuario(string nombre);
=======
        IEnumerable<Usuario> GetUsuarios();
        Usuario GetUsuario(int usuarioId);
        bool ExisteUsuario(int usuarioId);
        bool ExisteUsuario(string Nombre);
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
        Usuario CrearUsuario(Usuario usuario, string password);
        Usuario Login(string nombre, string password);
        bool Guardar();
    }
}
