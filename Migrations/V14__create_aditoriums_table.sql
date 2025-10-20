CREATE TABLE auditoriums (
    -- Very likely to use integer for id
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    capacity INT NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);
