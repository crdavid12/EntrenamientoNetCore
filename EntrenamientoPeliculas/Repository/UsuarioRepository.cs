using EntrenamientoPeliculas.Data;
using EntrenamientoPeliculas.Models;
using EntrenamientoPeliculas.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _bd;

        public UsuarioRepository(ApplicationDbContext bd)
        {
            _bd = bd;
        }
<<<<<<< HEAD
=======

>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
        public Usuario CrearUsuario(Usuario usuario, string password)
        {
            byte[] passwordHash, passwordSalt;

            CrearPasswordHash(password, out passwordHash, out passwordSalt);
<<<<<<< HEAD

=======
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            _bd.Usuario.Add(usuario);
            Guardar();
<<<<<<< HEAD

            return usuario;
=======
            return usuario; 
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
        }

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public bool ExisteUsuario(int usuarioId)
        {
            return _bd.Usuario.Any(x => x.Id == usuarioId);
        }

<<<<<<< HEAD
        public bool ExisteUsuario(string nombre)
        {
            return _bd.Usuario.Any(x => x.NombreUsuario.ToLower().Trim() == nombre.ToLower().Trim());
=======
        public bool ExisteUsuario(string Nombre)
        {
            return _bd.Usuario.Any(x => x.NombreUsuario == Nombre);
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
        }

        public Usuario GetUsuario(int usuarioId)
        {
            return _bd.Usuario.FirstOrDefault(x => x.Id == usuarioId);
        }

<<<<<<< HEAD
        public ICollection<Usuario> GetUsuarios()
=======
        public IEnumerable<Usuario> GetUsuarios()
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
        {
            return _bd.Usuario.OrderBy(x => x.Id).ToList();
        }

        public bool Guardar()
        {
<<<<<<< HEAD
            return _bd.SaveChanges() >= 0 ? true : false;           
=======
            return _bd.SaveChanges() >= 0 ? true : false;
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
        }

        public Usuario Login(string nombre, string password)
        {
            var user = _bd.Usuario.FirstOrDefault(x => x.NombreUsuario == nombre);
<<<<<<< HEAD
            if (user == null)
=======

            if(user == null)
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
            {
                return null;
            }

            if(!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
<<<<<<< HEAD
        }

        private bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
=======

        }
        private bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                //var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
                var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != passwordHash[i])
                        return false;
                }
            }
            return true;

        }
    }
}
