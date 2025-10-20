-- Directors
CREATE TRIGGER set_updated_at_directors
BEFORE UPDATE ON directors
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

-- Movies
CREATE TRIGGER set_updated_at_movies
BEFORE UPDATE ON movies
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

-- Movie actors
CREATE TRIGGER set_updated_at_movie_actors
BEFORE UPDATE ON movie_actors
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

-- Auditoriums
CREATE TRIGGER set_updated_at_auditoriums
BEFORE UPDATE ON auditoriums
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

-- Seats
CREATE TRIGGER set_updated_at_seats
BEFORE UPDATE ON seats
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

-- Showtimes
CREATE TRIGGER set_updated_at_showtimes
BEFORE UPDATE ON showtimes
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();

-- Tickets
CREATE TRIGGER set_updated_at_tickets
BEFORE UPDATE ON tickets
FOR EACH ROW
EXECUTE FUNCTION update_updated_at_column();
