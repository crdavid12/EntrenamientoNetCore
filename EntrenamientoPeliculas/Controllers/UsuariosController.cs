using AutoMapper;
using EntrenamientoPeliculas.Models;
using EntrenamientoPeliculas.Models.Dtos;
using EntrenamientoPeliculas.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Controllers
{
    [Route("api/Usuarios")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _usuRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UsuariosController(IUsuarioRepository usuRepo, IMapper mapper, IConfiguration config)
        {
            _usuRepo = usuRepo;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _usuRepo.GetUsuarios();

            var listaUsuariosDto = new List<UsuarioDto>();

            foreach (var item in listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(item));
            }
            return Ok(listaUsuariosDto);
        }

        [HttpGet("{usuarioId}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int usuarioId)
        {
            var usuario = _usuRepo.GetUsuario(usuarioId);

            if(usuario == null)
            {
                return NotFound();
            }

            var UsuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return Ok(UsuarioDto);
        }

        [HttpPost("Registro")]
        public IActionResult CrearUsuario( UsuarioCreateDto usuarioCreateDto)
        {
            usuarioCreateDto.NombreUsuario = usuarioCreateDto.NombreUsuario.ToLower();

            if (_usuRepo.ExisteUsuario(usuarioCreateDto.NombreUsuario))
            {
                return BadRequest("El usuario ya existe");
            }

            var usuarioACrear = new Usuario
            {
                NombreUsuario = usuarioCreateDto.NombreUsuario
            };

            var usuarioCreado = _usuRepo.CrearUsuario(usuarioACrear, usuarioCreateDto.Password);


            return Ok(usuarioCreado);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var usuarioDesdeRepo = _usuRepo.Login(usuarioLoginDto.NombreUsuario, usuarioLoginDto.Password);

            if(usuarioDesdeRepo == null)
            {
                return Unauthorized();
            }
            //Token

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuarioDesdeRepo.Id.ToString()),
            new Claim(ClaimTypes.Name, usuarioDesdeRepo.NombreUsuario.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });

        }
    }
}
