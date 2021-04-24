using AutoMapper;
using EntrenamientoPeliculas.Models;
using EntrenamientoPeliculas.Models.Dtos;
using EntrenamientoPeliculas.Repository.IRepository;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authorization;
=======
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
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
<<<<<<< HEAD
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
=======
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
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
            _mapper = mapper;
            _config = config;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
<<<<<<< HEAD
            var listaUsuarios = _userRepo.GetUsuarios();
=======
            var listaUsuarios = _usuRepo.GetUsuarios();
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b

            var listaUsuariosDto = new List<UsuarioDto>();

            foreach (var item in listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(item));
            }
<<<<<<< HEAD

            return Ok(listaUsuariosDto);

        }

        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int usuarioId)
        {
            if (!_userRepo.ExisteUsuario(usuarioId))
=======
            return Ok(listaUsuariosDto);
        }

        [HttpGet("{usuarioId}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int usuarioId)
        {
            var usuario = _usuRepo.GetUsuario(usuarioId);

            if(usuario == null)
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
            {
                return NotFound();
            }

<<<<<<< HEAD
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
=======
            var UsuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return Ok(UsuarioDto);
        }

        [HttpPost("Registro")]
        public IActionResult CrearUsuario( UsuarioCreateDto usuarioCreateDto)
        {
            usuarioCreateDto.NombreUsuario = usuarioCreateDto.NombreUsuario.ToLower();

            if (_usuRepo.ExisteUsuario(usuarioCreateDto.NombreUsuario))
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
            {
                return BadRequest("El usuario ya existe");
            }

<<<<<<< HEAD
            var user = new Usuario()
=======
            var usuarioACrear = new Usuario
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
            {
                NombreUsuario = usuarioCreateDto.NombreUsuario
            };

<<<<<<< HEAD
            var UsuarioCreado = _userRepo.CrearUsuario(user, usuarioCreateDto.Password);

            return Ok(UsuarioCreado);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UsuarioLoginAuthDto usuarioLoginAuthDto)
        {
            var usuarioDesdeRepo = _userRepo.Login(usuarioLoginAuthDto.Usuario, usuarioLoginAuthDto.Password);
=======
            var usuarioCreado = _usuRepo.CrearUsuario(usuarioACrear, usuarioCreateDto.Password);


            return Ok(usuarioCreado);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UsuarioLoginDto usuarioLoginDto)
        {
            var usuarioDesdeRepo = _usuRepo.Login(usuarioLoginDto.NombreUsuario, usuarioLoginDto.Password);
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b

            if(usuarioDesdeRepo == null)
            {
                return Unauthorized();
            }
<<<<<<< HEAD

=======
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
            //Token

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuarioDesdeRepo.Id.ToString()),
            new Claim(ClaimTypes.Name, usuarioDesdeRepo.NombreUsuario.ToString())
<<<<<<< HEAD
        };
=======
            };
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b

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
<<<<<<< HEAD

=======
>>>>>>> 7848e1daa08bb3835396b6ad01a0a917586a4b3b
    }
}
