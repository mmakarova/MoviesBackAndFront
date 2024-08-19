using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories {
    public interface IGenreRepository {
        Task<Genre?> GetByIdAsync(int id);
        Task<IEnumerable<Genre>> GetAllAsync();

    }
}
