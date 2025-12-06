using Microsoft.AspNetCore.Mvc;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowtimeController : Controller
    {
        private readonly IShowtimeRepository _showtimeRepository;

        public ShowtimeController(IShowtimeRepository showtimeRepository)
        {
            _showtimeRepository = showtimeRepository;
        }

        // GET: /Showtime
        [HttpGet]
        public ActionResult<List<Showtime>> GetAllShowtimes()
        {
            var showtimes = _showtimeRepository.GetAll();
            if (showtimes == null || !showtimes.Any())
                return NoContent();

            return Ok(showtimes);
        }

        // GET: /Showtime/{id}
        [HttpGet("{id}")]
        public ActionResult<Showtime> GetShowtimeById(Guid id)
        {
            var showtime = _showtimeRepository.GetById(id);
            if (showtime == null)
                return NoContent();

            return Ok(showtime);
        }

        // POST: /Showtime
        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult InsertShowtime([FromBody] Showtime showtime)
        {
            if (showtime == null)
            {
                return BadRequest("Suất chiếu bị null. Kiểm tra định dạng JSON.");
            }
            try
            {
                _showtimeRepository.Add(showtime);
                return StatusCode(201, "Thêm suất chiếu thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể thêm suất chiếu: " + ex.Message);
            }
        }

        // PUT: /Showtime/{id}
        // [HttpPut("{id}")]
        // public ActionResult UpdateShowtimeById(Guid id, [FromBody] Showtime showtime)
        // {
        //     try
        //     {
        //         _showtimeRepository.UpdateById(id, showtime);
        //         return Ok("Showtime updated successfully with id: " + id);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, "Failed to update showtime: " + ex.Message);
        //     }
        // }

        // DELETE: /Showtime/{id}
        [HttpDelete("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult DeleteShowtimeById(Guid id)
        {
            try
            {
                _showtimeRepository.Delete(id);
                return Ok("Đã xóa suất chiếu thành công với id: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể xóa suất chiếu: " + ex.Message);
            }
        }

        // DELETE: /Showtime
        [HttpDelete]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult DeleteAllShowtimes()
        {
            try
            {
                _showtimeRepository.DeleteAll();
                return Ok("Đã xóa tất cả suất chiếu thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể xóa tất cả suất chiếu: " + ex.Message);
            }
        }
    }
}
