CREATE TABLE movie_actors (
    movie_id UUID REFERENCES movies(id) ON DELETE CASCADE,
    actor_id UUID REFERENCES actors(id) ON DELETE CASCADE,
    PRIMARY KEY (movie_id, actor_id)
);