using AutoMapper;
using EntrenamientoPeliculas.Models;
using EntrenamientoPeliculas.Models.Dtos;
using EntrenamientoPeliculas.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntrenamientoPeliculas.Controllers
{
    [Authorize]
    [Route("api/Categorias")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "APIPeliculasCategorias")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CategoriasController : Controller
    {
        private readonly ICategoriaRepsitory _catRepo;
        private readonly IMapper _mapper;

        public CategoriasController(ICategoriaRepsitory catRepo, IMapper mapper)
        {
            _catRepo = catRepo;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Obtener todas las Categorias
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CategoriaDto>))]
        [ProducesResponseType(400)]
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

        /// <summary>
        /// Obtener categoria por Id
        /// </summary>
        /// <param name="categoriaId">Este es el Id de la categoria</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{categoriaId:int}", Name = "GetCategoria")]
        [ProducesResponseType(200, Type =typeof(CategoriaDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
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

        /// <summary>
        /// Crear una Categoria
        /// </summary>
        /// <param name="categoriaDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type= typeof(CategoriaDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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


        /// <summary>
        /// Actualizar una categoria
        /// </summary>
        /// <param name="categoriaId">Este es el Id de la categoria </param>
        /// <param name="categoriaDto"></param>
        /// <returns></returns>
        [HttpPatch("{categoriaId}", Name = "ActualizarCategoria")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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


        /// <summary>
        /// Borrar una Categoria
        /// </summary>
        /// <param name="categoriaId">Este es el id de la Categoria</param>
        /// <returns></returns>
        [HttpDelete("{categoriaId:int}", Name = "BorrarCategoria")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
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
