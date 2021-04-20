using AutoMapper;
using EntrenamientoPeliculas.Models;
using EntrenamientoPeliculas.Models.Dtos;
using EntrenamientoPeliculas.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Controllers
{
    [Route("api/Categorias")]
    [ApiController]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepsitory _catRepo;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepsitory catRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            var listaCategorias = _catRepo.GetCategorias();

            var listaCategoriasDto = new List<CategoriaDto>();

            foreach (var item in listaCategorias)
            {
                listaCategoriasDto.Add(_mapper.Map<CategoriaDto>(item));
            }

            return Ok(listaCategoriasDto);
        }

        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        public IActionResult GetCategoria(int categoriaId)
        {
            var itemCategoria = _catRepo.GetCategoria(categoriaId);

            if (itemCategoria == null)
            {
                return NotFound();
            }

            var itemCategoriaDto = _mapper.Map<CategoriaDto>(itemCategoria);

            return Ok(itemCategoriaDto);
        }

        [HttpPost]
        public IActionResult CrearCategoria([FromBody]CategoriaDto categoriaDto)
        {
            if(categoriaDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_catRepo.ExisteCategoria(categoriaDto.Nombre))
            {
                ModelState.AddModelError("", $"La categoria ya existe");
                return StatusCode(404, ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            if (!_catRepo.CrearCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió masl al crear la categria {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategoria", new { categoriaId = categoria.Id }, categoria);
        }

        [HttpPatch("{categoriaId}", Name = "ActualizarCategoria")]
        public IActionResult ActualizarCategoria(int categoriaId, [FromBody] CategoriaDto categoriaDto)
        {
            if (categoriaDto == null || categoriaDto.Id != categoriaId)
            {
                return BadRequest(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            if (!_catRepo.ActualizarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió masl al actualizar la categoria {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        public IActionResult BorrarCategoria(int categoriaId)
        {
            if (!_catRepo.ExisteCategoria(categoriaId))
            {
                return NotFound();
            }

            var categoria = _catRepo.GetCategoria(categoriaId);

            if (!_catRepo.BorrarCategoria(categoria))
            {
                ModelState.AddModelError("", $"Algo salió masl al borrar la categoria {categoria.Nombre}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
