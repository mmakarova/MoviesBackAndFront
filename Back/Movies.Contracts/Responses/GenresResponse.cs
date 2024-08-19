using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses {
    public class GenresResponse {
        public required IEnumerable<GenreResponse> Items { get; init; } = Enumerable.Empty<GenreResponse>();
    }
}
