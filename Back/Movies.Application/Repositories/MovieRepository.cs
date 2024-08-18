using Dapper;
using Movies.Application.Database;
using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application.Repositories {
    public class MovieRepository : IMovieRepository {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public MovieRepository(IDbConnectionFactory dbConnectionFactory) {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<Movie?> GetByIdAsync(int id) {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var movie = await connection.QuerySingleOrDefaultAsync<Movie>(
                new CommandDefinition("""
                    select * from movies where id = @id
                    """, new { id }));
            if (movie is null) {
                return null;
            }           
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync(GetAllMoviesOptions options) {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.QueryAsync(
              new CommandDefinition("""
                  select id,title,yearofrelease,posterurl from movies 
                  where (@title is null or title like ('%' || @title || '%')) and (@yearofrelease is null or yearofrelease =@yearofrelease)
                  limit @pageSize
                  offset @pageOffset
                  """, new {
                  title = options.Title,
                  yearofrelease = options.YearOfRelease,
                  pageSize = options.PageSize,
                  pageOffset = (options.Page - 1)*options.PageSize
              }));
            return result.Select(x => new Movie {
                Id = x.id,
                Title = x.title,
                YearOfRelease = x.yearofrelease,
                PosterUrl = x.posterurl
            });
        }

     
        public async Task<int> GetCountAsync(string? title, int? yearOfRelease) {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            return await connection.QuerySingleAsync<int>(new CommandDefinition("""
                select count(id) from movies
                where (@title is null or title like ('%' || @title || '%'))
                and (@yearOfRelease is null or yearofrelease = @yearOfRelease)
                """, new { title = title, yearOfRelease = yearOfRelease }));
        }
    }

}
