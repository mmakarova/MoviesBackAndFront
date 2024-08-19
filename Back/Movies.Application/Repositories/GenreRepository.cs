using Dapper;
using Movies.Application.Database;
using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories {
    public class GenreRepository : IGenreRepository {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public GenreRepository(IDbConnectionFactory dbConnectionFactory) {
            _dbConnectionFactory = dbConnectionFactory;
        }


        public async Task<IEnumerable<Genre>> GetAllAsync() {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.QueryAsync(new CommandDefinition("""
                select * from genres
                """));
            return result.Select(x => new Genre {
                Id = x.id,
                Title = x.title
            });
        }

        public async Task<Genre?> GetByIdAsync(int id) {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var genre = await connection.QuerySingleOrDefaultAsync<Genre>(
                new CommandDefinition("""
                    select * from genres where id = @id
                    """, new { id }));
            if (genre is null) {
                return null;
            }

            return genre;
        }
    }
}
