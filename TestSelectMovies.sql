
  select movies.id, movies.title, movies.title, movies.yearofrelease, movies.posterurl, array_agg(genres.id) as genres from movies
  join movies_genres on movies.id = movies_genres.movie_id
  join genres on genres.id = movies_genres.genre_id
  group by movies.id
  having 1 = ANY(array_agg(genres.id)) 
