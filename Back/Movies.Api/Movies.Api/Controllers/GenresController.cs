using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Movies.Api.Mapping;
using Movies.Application.Models;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Controllers {
    [ApiController]
    public class GenresController : ControllerBase {
        private readonly IGenreRepository _genreRepository;
        public GenresController(IGenreRepository genreRepository) {
            _genreRepository = genreRepository;

        }

        [HttpGet($"/api/genres/{{id:int}}")]
        [ProducesResponseType(typeof(GenreResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id) {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null) {
                return NotFound();
            }
            return Ok(genre.MapToResponse());
        }

        [HttpGet($"/api/genres")]
        [ProducesResponseType(typeof(GenresResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() {
            var genres = await _genreRepository.GetAllAsync();
            if (genres == null) {
                return NotFound();
            }
            return Ok(genres.MapToResponse());
        }
    }
}
