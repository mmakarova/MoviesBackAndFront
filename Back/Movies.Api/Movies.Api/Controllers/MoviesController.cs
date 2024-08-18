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
    public class MoviesController : ControllerBase {

        private readonly IMovieRepository _movieRepository;
        public MoviesController(IMovieRepository movieRepository) {
            _movieRepository = movieRepository;            

        }


        [HttpGet($"/api/movies/{{id:guid}}")]
        public async Task<IActionResult> Get([FromRoute] int id) {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) {
                return NotFound();
            }
            return Ok(movie.MapToResponse());
        }

        [HttpGet($"/api/movies")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMoviesRequest request) {
            var options = request.MapTopOptions();
            var movies = await _movieRepository.GetAllAsync(options);            
            if (movies == null) {
                return NotFound();
            }
            var movieCount = await _movieRepository.GetCountAsync(options.Title, options.YearOfRelease);
            var moviesResponse = movies.MapToResponse(request.Page,request.PageSize, movieCount);
            return Ok(moviesResponse);
        }


    }
}

