using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Models {
    public class GetAllMoviesOptions {
        public required string? Title { get; init; }
      
        public int? GenreId { get; init; }
        public string? PosterUrl { get; init; }
        public int? YearOfRelease { get; init; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
