using Microsoft.AspNetCore.Mvc;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("auditorium")]
    public class AuditoriumController : Controller
    {
        private readonly IAuditoriumRepository _auditoriumRepository;

        public AuditoriumController(IAuditoriumRepository auditoriumRepository)
        {
            _auditoriumRepository = auditoriumRepository;
        }

        [HttpGet]
        public ActionResult<List<Auditorium>> GetAllAuditoriums()
        {
            var auditoriums = _auditoriumRepository.GetAll();
            if (auditoriums == null || !auditoriums.Any())
                return NoContent();
            return Ok(auditoriums);
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult InsertAuditorium([FromBody] Auditorium auditorium)
        {
            try
            {
                _auditoriumRepository.Add(auditorium);
                return StatusCode(201, "Thêm phòng chiếu thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể thêm phòng chiếu: " + ex.Message);
            }
        }

        [HttpDelete]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult DeleteAllAuditoriums()
        {
            try
            {
                _auditoriumRepository.DeleteAll();
                return Ok("Đã xóa tất cả phòng chiếu thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể xóa tất cả phòng chiếu: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult DeleteAuditoriumById(Guid id)
        {
            try
            {
                _auditoriumRepository.Delete(id);
                return Ok("Đã xóa phòng chiếu thành công với id: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể xóa phòng chiếu: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Auditorium> GetAuditoriumById(Guid id)
        {
            var auditorium = _auditoriumRepository.GetById(id);
            if (auditorium == null)
                return NoContent();
            return Ok(auditorium);
        }

        [HttpPut("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult UpdateAuditoriumById(Guid id, [FromBody] Auditorium auditorium)
        {
            try
            {
                _auditoriumRepository.UpdateById(id, auditorium);
                return Ok("Cập nhật phòng chiếu thành công với id: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể cập nhật phòng chiếu: " + ex.Message);
            }
        }
    }
}
