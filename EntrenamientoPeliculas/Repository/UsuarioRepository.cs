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

        public Usuario CrearUsuario(Usuario usuario, string password)
        {
            byte[] passwordHash, passwordSalt;

            CrearPasswordHash(password, out passwordHash, out passwordSalt);
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            _bd.Usuario.Add(usuario);
            Guardar();
            return usuario; 
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

        public bool ExisteUsuario(string Nombre)
        {
            return _bd.Usuario.Any(x => x.NombreUsuario == Nombre);
        }

        public Usuario GetUsuario(int usuarioId)
        {
            return _bd.Usuario.FirstOrDefault(x => x.Id == usuarioId);
        }

        public IEnumerable<Usuario> GetUsuarios()
        {
            return _bd.Usuario.OrderBy(x => x.Id).ToList();
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

        public Usuario Login(string nombre, string password)
        {
            var user = _bd.Usuario.FirstOrDefault(x => x.NombreUsuario == nombre);

            if(user == null)
            {
                return null;
            }

            if(!VerificarPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;

        }
        private bool VerificarPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {

            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                //var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
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
