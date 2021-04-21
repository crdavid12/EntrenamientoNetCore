using AutoMapper;
using EntrenamientoPeliculas.Models;
using EntrenamientoPeliculas.Models.Dtos;
using EntrenamientoPeliculas.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Controllers
{
    [Route("api/Peliculas")]
    [ApiController]
    public class PeliculaController : Controller
    {
        private readonly IPeliculaRepository _pelRepo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PeliculaController(IPeliculaRepository pelRepo, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _pelRepo = pelRepo;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Getpeliculas()
        {
            var listaPeliculas = _pelRepo.Getpeliculas();
            var listaPeliculasDto = new List<PeliculaDto>();

            foreach (var item in listaPeliculas)
            {
                listaPeliculasDto.Add(_mapper.Map<PeliculaDto>(item));
            }

            return Ok(listaPeliculasDto);
        }
        [HttpGet("{peliculaId:int}", Name = "GetPelicula")]
        public IActionResult GetPelicula(int peliculaId)
        {
            var itemPelicula = _pelRepo.GetPelicula(peliculaId);

            if (itemPelicula == null)
            {
                return NotFound();
            }

            var itemPeliculaDto = _mapper.Map<PeliculaDto>(itemPelicula);

            return Ok(itemPeliculaDto);
        }

        [HttpGet("GetPeliculasEnCategoria/{categoriaId:int}")]
        public IActionResult GetPeliculasEnCategoria(int categoriaId)
        {
            var itempelicula = _pelRepo.GetPeliculasEnCategoria(categoriaId);
            if (itempelicula.Count() == 0)
            {
                return NotFound();
            }

            var itemPeliculaDto = new List<PeliculaDto>();

            foreach (var item in itempelicula)
            {
                itemPeliculaDto.Add(_mapper.Map<PeliculaDto>(item));
            }

            return Ok(itemPeliculaDto);
        }

        [HttpGet("Buscar")]
        public IActionResult BuscarPelicula(string nombre)
        {
            try
            {
                var resultado = _pelRepo.BuscarPelicula(nombre);

                if (resultado.Any())
                {
                    return Ok(resultado);
                }
                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error, recuperando la aplicacion");
            }
        }

        [HttpPost]
        public IActionResult CrearPelicula([FromForm] PeliculaCreateDto peliculaDTO)
        {
            if (peliculaDTO == null)
            {
                return NotFound();
            }
            if (_pelRepo.ExistePelicula(peliculaDTO.Nombre))
            {
                ModelState.AddModelError("", $"La pelicula ya existe {peliculaDTO.Nombre}");
                return StatusCode(404, ModelState);
            }

            //Subir Archivo

            var archivo = peliculaDTO.Foto;
            string rutaPrincipal = _hostingEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;

            if (archivo.Length > 0)
            {
                //Nueva Imagen
                var nombreFoto = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"fotos");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreFoto + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }
                peliculaDTO.RutaImagen = @"\fotos\" + nombreFoto + extension;
            }

            var pelicula = _mapper.Map<Pelicula>(peliculaDTO);

            if (!_pelRepo.CrearPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo salió mal creando la Pelicula {pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetPelicula", new { peliculaId = pelicula.Id }, pelicula);
        }

        [HttpPatch("{peliculaId:int}", Name = "ActualizarPelicula")]
        public IActionResult ActualizarPelicula(int peliculaId, [FromBody] PeliculaUpdateDto peliculaUpdateDto)
        {
            if (peliculaUpdateDto == null || peliculaUpdateDto.Id != peliculaId)
            {
                return NotFound();
            }

            var pelicula = _mapper.Map<Pelicula>(peliculaUpdateDto);

            if (!_pelRepo.ActualizarPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Salió algo mal al actualizar la pelicula {pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{peliculaId:int}", Name = "BorrarPelicula")]
        public IActionResult BorrarPelicula(int peliculaId)
        {
            var itempeliculaDto = _pelRepo.GetPelicula(peliculaId);

            if(itempeliculaDto == null)
            {
                return NotFound();
            }

            var pelicula = _mapper.Map<Pelicula>(itempeliculaDto);

            if (!_pelRepo.BorrarPelicula(pelicula))
            {
                ModelState.AddModelError("", $"Algo pasó al elimininar la pelicula {pelicula.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
    }
}
