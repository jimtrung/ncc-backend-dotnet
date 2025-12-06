using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Domain.Repositories;
using Theater_Management_BE.src.Api.Dtos;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTicketsByUserId(Guid userId)
        {
            var tickets = await _ticketRepository.GetTicketsByUserId(userId);
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> InsertTicket([FromBody] TicketRequestDto dto)
        {
            Console.WriteLine($"UserId = {dto.Userid}");
            Console.WriteLine($"ShowtimeId = {dto.Showtimeid}");
            Console.WriteLine($"SeatName: {dto.Seatname}");
            Console.WriteLine($"Price: {dto.Price}");

            var ticket = new Ticket
            {
                Userid = dto.Userid,
                Showtimeid = dto.Showtimeid,
                Price = dto.Price,
                Seatname = dto.Seatname,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var inserted = await _ticketRepository.InsertTicket(ticket);
            return Ok(inserted);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllTickets();
            return Ok(tickets);
        }
        // DELETE /ticket/{id} - xóa vé theo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketById(Guid id)
        {
            var deleted = await _ticketRepository.DeleteTicketById(id);
            if (!deleted)
                return NotFound($"Không tìm thấy vé với ID {id}.");

            return Ok($"Đã xóa vé {id} thành công.");
        }

        // DELETE /ticket - xóa tất cả vé
        [HttpDelete]
        public async Task<IActionResult> DeleteAllTickets()
        {
            await _ticketRepository.DeleteAllTickets();
            return Ok("Đã xóa tất cả vé thành công.");
        }

        // GET /ticket/{id} - lấy vé theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicketById(Guid id)
        {
            var ticket = await _ticketRepository.GetTicketById(id);
            if (ticket == null)
                return NotFound($"Không tìm thấy vé với ID {id}.");

            return Ok(ticket);
        }


    }
}
