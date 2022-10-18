﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWebAPIApp.Dto;
using MyWebAPIApp.Interfaces;
using MyWebAPIApp.Models;
using MyWebAPIApp.Repository;

namespace MyWebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;

        public PokemonController(IPokemonRepository pokemonRepository
            , IReviewRepository reviewRepository
            , ICategoryRepository categoryRepository
            , IMapper mapper)
        {
            _mapper = mapper;
            _pokemonRepository = pokemonRepository;
            _reviewRepository = reviewRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        public IActionResult GetPokemons()
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons());

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId)) return NotFound();

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId));

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId)) return NotFound();

            var raitng = _pokemonRepository.GetPokemonRating(pokeId);

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(raitng);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int ownerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null) return BadRequest(ModelState);

            var pokemon = _pokemonRepository.GetPokemons()
                .Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (pokemon != null)
            {
                ModelState.AddModelError("", "pokemon already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

            if (!_pokemonRepository.CreatePokemon(ownerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something wrong went saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCountry([FromQuery] int ownerId, [FromQuery] int categpryId, int pokeId, [FromBody] PokemonDto updatePokemon)
        {
            if (updatePokemon == null) return BadRequest(ModelState);

            if (pokeId != updatePokemon.Id) return BadRequest(ModelState);

            if (!_pokemonRepository.PokemonExists(pokeId)) return NotFound();

            if (!ModelState.IsValid) return BadRequest();

            var pokemonMap = _mapper.Map<Pokemon>(updatePokemon);

            if (!_pokemonRepository.UpdatePokemon(ownerId, categpryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something wrong when updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{pokemonId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePokemon(int pokemonId)
        {
            if (!_pokemonRepository.PokemonExists(pokemonId)) return NotFound();

            var reviewDelete = _reviewRepository.GetReviewsOfAPokemon(pokemonId);
            var pokemonDelete = _pokemonRepository.GetPokemon(pokemonId);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewDelete.ToList()))
                ModelState.AddModelError("", "something wrong when deleting");

            if (!_pokemonRepository.DeletePokemon(pokemonDelete))
                ModelState.AddModelError("", "something wrong when deleting");

            return NoContent();
        }
    }
}
