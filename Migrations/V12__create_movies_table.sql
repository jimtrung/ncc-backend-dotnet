CREATE TABLE movies (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name TEXT NOT NULL,
    description TEXT,
    director_id UUID REFERENCES directors(id),
    genres movie_genre[] NOT NULL,
    premiere TIMESTAMPTZ,
    duration INT,
    language TEXT,
    rated INT,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
