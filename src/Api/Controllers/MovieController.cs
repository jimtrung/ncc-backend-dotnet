using Microsoft.AspNetCore.Mvc;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public ActionResult<List<Movie>> GetAllMovies()
        {
            var movies = _movieRepository.GetAll();
            if (movies == null || !movies.Any())
                return NoContent();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetMovieById(Guid id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null)
                return NotFound("Movie not found with id: " + id);
            return Ok(movie);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult InsertMovie([FromBody] Movie movie)
        {
            try
            {
                _movieRepository.Add(movie);
                return StatusCode(201, "Movie inserted successfully 🎬");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to insert movie: " + ex.Message);
            }
        }

        [HttpDelete]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult DeleteAllMovies()
        {
            try
            {
                _movieRepository.DeleteAll();
                return Ok("All movies have been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to delete all movies: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult DeleteMovieById(Guid id)
        {
            try
            {
                _movieRepository.Delete(id);
                return Ok("Movie deleted successfully with id: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to delete movie: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult UpdateMovieById(Guid id, [FromBody] Movie movie)
        {
            try
            {
                _movieRepository.Update(movie);
                return Ok("Movie updated successfully with id: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to update movie: " + ex.Message);
            }
        }
    }
}
