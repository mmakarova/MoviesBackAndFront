﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Contracts.Responses {
    public class MoviesResponse {
        public required IEnumerable<MovieResponse> Items { get; init; } = Enumerable.Empty<MovieResponse>();
        public required int PageSize { get; init; }
        public required int Page { get; init; }
        public required int Total { get; init; }
    }
}
