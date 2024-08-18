using Movies.Application.Models;

using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.Api.Mapping {
    public static class ContractMappingMovies {

        public static MovieResponse MapToResponse(this Movie movie) {
            return new MovieResponse {
                Id = movie.Id,
                Title = movie.Title,
                YearOfRelease = movie.YearOfRelease,
                PosterUrl = movie.PosterUrl
            };
        }

        public static MoviesResponse MapToResponse(this IEnumerable<Movie> movies, int page, int pageSize, int totalCount) {
            return new MoviesResponse {
                Items = movies.Select(MapToResponse),
                Page = page,
                PageSize = pageSize,
                Total = totalCount
            };
        }

        public static GetAllMoviesOptions MapTopOptions(this GetAllMoviesRequest request) {

            return new GetAllMoviesOptions {
                Title = request.Title,
                Page = request.Page,
                PageSize  = request.PageSize,
            };
        }



    } 
}
