using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWebAPIApp.Dto;
using MyWebAPIApp.Interfaces;
using MyWebAPIApp.Models;

namespace MyWebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(categories);
        }

        [HttpGet("{cateId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int cateId)
        {
            if (!_categoryRepository.CategoryExists(cateId)) return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(cateId));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategory(int categoryId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_categoryRepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid) return BadRequest();
            return Ok(pokemons);
        }
    }
}
