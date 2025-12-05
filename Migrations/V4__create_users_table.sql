CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),

    username TEXT UNIQUE,
    email TEXT NOT NULL UNIQUE,
    password TEXT,

    role role_type NOT NULL,
    provider provider_type NOT NULL,
    token TEXT,
    verified BOOLEAN NOT NULL DEFAULT FALSE,

    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
