using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories {
    public interface IMovieRepository {
        Task<Movie?> GetByIdAsync(int id);
        Task<IEnumerable<Movie>> GetAllAsync(GetAllMoviesOptions options);

        Task<int> GetCountAsync(string? title, int? yearOfRelease, int? genreid);

    }
}
