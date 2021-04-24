using AutoMapper;
using EntrenamientoPeliculas.Models;
using EntrenamientoPeliculas.Models.Dtos;
using EntrenamientoPeliculas.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/Usuarios")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "APIPeliculasUsuarios")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _userRepo;
        private IMapper _mapper;
        private readonly IConfiguration _config;


        public UsuariosController(IUsuarioRepository userRepo, IMapper mapper, IConfiguration config)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _config = config;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _userRepo.GetUsuarios();

            var listaUsuariosDto = new List<UsuarioDto>();

            foreach (var item in listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(item));
            }

            return Ok(listaUsuariosDto);

        }

        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int usuarioId)
        {
            if (!_userRepo.ExisteUsuario(usuarioId))
            {
                return NotFound();
            }

            var user = _userRepo.GetUsuario(usuarioId);

            var userDto = _mapper.Map<UsuarioDto>(user);

            return Ok(userDto);
        }
        
        [AllowAnonymous]
        [HttpPost("Registro")]
        public IActionResult CrearUsuario(UsuarioCreateDto usuarioCreateDto)
        {
            if (usuarioCreateDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_userRepo.ExisteUsuario(usuarioCreateDto.NombreUsuario))
            {
                return BadRequest("El usuario ya existe");
            }

            var user = new Usuario()
            {
                NombreUsuario = usuarioCreateDto.NombreUsuario
            };

            var UsuarioCreado = _userRepo.CrearUsuario(user, usuarioCreateDto.Password);

            return Ok(UsuarioCreado);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UsuarioLoginAuthDto usuarioLoginAuthDto)
        {
            var usuarioDesdeRepo = _userRepo.Login(usuarioLoginAuthDto.Usuario, usuarioLoginAuthDto.Password);

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
