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
            var result = await connection.QuerySingleOrDefaultAsync<dynamic>(
                new CommandDefinition("""
                    select movies.id, movies.title, movies.title, movies.yearofrelease, movies.posterurl, array_agg(genres.id) as genres from movies
                    join movies_genres on movies.id = movies_genres.movie_id
                    join genres on genres.id = movies_genres.genre_id
                    where (movies.id = @id)
                    group by movies.id 
                    """, new { id }));
            if (result is null) {
                return null;
            }
            return new Movie {
                Id = result.id,
                Title = result.title,
                YearOfRelease = result.yearofrelease,
                PosterUrl = result.posterurl,
                Genres = new List<int>(result.genres)
            };            
        }
        public async Task<IEnumerable<Movie>> GetAllAsync(GetAllMoviesOptions options) {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.QueryAsync(
              new CommandDefinition("""
                  select movies.id, movies.title, movies.title, movies.yearofrelease, movies.posterurl, array_agg(genres.id) as genres from movies
                  join movies_genres on movies.id = movies_genres.movie_id
                  join genres on genres.id = movies_genres.genre_id
                  where (@title is null or movies.title ilike ('%' || @title || '%'))
                  group by movies.id                  
                  having @genreid = ANY(array_agg(genres.id)) or @genreid is null
                  limit @pageSize
                  offset @pageOffset
                  """, new {
                  title = options.Title,
                  genreid = options.GenreId,
                  yearofrelease = options.YearOfRelease,
                  pageSize = options.PageSize,
                  pageOffset = (options.Page - 1)*options.PageSize
              }));
            return result.Select(x => new Movie {
                Id = x.id,
                Title = x.title,
                YearOfRelease = x.yearofrelease,
                PosterUrl = x.posterurl,
                Genres = new List<int>(x.genres)
            });
        }

        public async Task<int> GetCountAsync(string? title, int? yearOfRelease, int? genreid) {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            return await connection.QuerySingleAsync<int>(new CommandDefinition("""
                select count(*) from
                (select movies.id from movies
                join movies_genres on movies.id = movies_genres.movie_id
                join genres on genres.id = movies_genres.genre_id
                where (@title is null or movies.title ilike ('%' || @title || '%'))
                group by movies.id                  
                having @genreid = ANY(array_agg(genres.id)) or @genreid is null)
                """, new { title = title, yearOfRelease = yearOfRelease, genreid = genreid }));
        }


    }

}
