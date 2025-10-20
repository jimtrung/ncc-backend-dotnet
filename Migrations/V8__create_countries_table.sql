CREATE TABLE countries (
    code CHAR(2) PRIMARY KEY,
    name TEXT NOT NULL,
    iso3 CHAR(3) UNIQUE,
    phone_code TEXT,
    created_at TIMESTAMPTZ DEFAULT NOW(),
    updated_at TIMESTAMPTZ DEFAULT NOW()
);