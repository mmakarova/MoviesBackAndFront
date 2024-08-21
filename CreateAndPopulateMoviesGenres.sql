create table if not exists movies (
id serial primary key,
title TEXT not null,
posterurl TEXT not null,
yearofrelease integer not null);

create table if not exists genres (
id serial primary key,
title TEXT not null);

create table if not exists movies_genres (
movie_id integer not null,
genre_id integer not null,
primary key(movie_id, genre_id)
);

INSERT INTO genres (title) VALUES 
('Фэнтези'),
('Приключения'),
('Боевик'),
('Фантастика'),
('Драма');

--select * from genres
--1	Фэнтези
--2	Приключения
--3	Боевик
--4	Фантастика
--5	Драма

INSERT INTO movies (title,yearofrelease,posterurl) VALUES 
('Меч короля Артура',2017,'https://upload.wikimedia.org/wikipedia/ru/thumb/c/c6/King_Arthur_Legend_of_the_Sword.jpg/202px-King_Arthur_Legend_of_the_Sword.jpg'),
('Бэтмен: Начало',2005,'https://upload.wikimedia.org/wikipedia/ru/thumb/d/d2/Batman_Begins_%28poster%29.jpg/245px-Batman_Begins_%28poster%29.jpg'),
('Легенда о зеленом рыцаре',2021,'https://upload.wikimedia.org/wikipedia/ru/thumb/0/04/The-Green-Knight.jpg/211px-The-Green-Knight.jpg'),
('Темный рыцарь',2008,'https:////upload.wikimedia.org/wikipedia/ru/thumb/f/f4/%D0%A2%D1%91%D0%BC%D0%BD%D1%8B%D0%B9_%D1%80%D1%8B%D1%86%D0%B0%D1%80%D1%8C_%282008%29_%D0%BF%D0%BE%D1%81%D1%82%D0%B5%D1%80.jpg/200px-%D0%A2%D1%91%D0%BC%D0%BD%D1%8B%D0%B9_%D1%80%D1%8B%D1%86%D0%B0%D1%80%D1%8C_%282008%29_%D0%BF%D0%BE%D1%81%D1%82%D0%B5%D1%80.jpg'),
('Крестный отец',1972,'https://upload.wikimedia.org/wikipedia/ru/thumb/c/c4/Godfather_vhs.jpg/270px-Godfather_vhs.jpg'),
('Лабиринт фавна',2006,'https:////upload.wikimedia.org/wikipedia/ru/thumb/c/c7/El_laberinto_del_fauno_%28poster%29.jpg/200px-El_laberinto_del_fauno_%28poster%29.jpg');

--select * from movies;
--1	Меч короля Артура
--2	Бэтмен: Начало
--3	Легенда о зеленом рыцаре
--4	Темный рыцарь
--5	Крестный отец
--6	Лабиринт фавна


INSERT INTO movies_genres (movie_id, genre_id) VALUES 
(1, 1),
(1, 2),
(2, 2),
(2, 3),
(2, 4),
(2, 5),
(3, 1),
(3, 2),
(3, 5),
(4, 3),
(4, 5),
(5, 5),
(6, 1),
(6, 5);

                  