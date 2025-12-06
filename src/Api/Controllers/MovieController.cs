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
                return NotFound("Không tìm thấy phim với id: " + id);
            return Ok(movie);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult InsertMovie([FromBody] Movie movie)
        {
            try
            {
                _movieRepository.Add(movie);
                return StatusCode(201, "Thêm phim thành công 🎬");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể thêm phim: " + ex.Message);
            }
        }

        [HttpDelete]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult DeleteAllMovies()
        {
            try
            {
                _movieRepository.DeleteAll();
                return Ok("Đã xóa tất cả phim thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể xóa tất cả phim: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult DeleteMovieById(Guid id)
        {
            try
            {
                _movieRepository.Delete(id);
                return Ok("Đã xóa phim thành công với id: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể xóa phim: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult UpdateMovieById(Guid id, [FromBody] Movie movie)
        {
            try
            {
                _movieRepository.Update(movie);
                return Ok("Cập nhật phim thành công với id: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể cập nhật phim: " + ex.Message);
            }
        }
    }
}
