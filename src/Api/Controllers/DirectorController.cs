using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Theater_Management_BE.src.Application.Services;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("director")]
    public class DirectorController : ControllerBase
    {
        private readonly DirectorService _directorService;

        public DirectorController(DirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpGet("all")]
        public ActionResult<List<Director>> GetAllDirectors()
        {
            var directors = _directorService.GetAllDirectors();
            return Ok(directors);
        }

        [HttpGet("{id}")]
        public ActionResult<Director?> GetDirectorById(Guid id)
        {
            var director = _directorService.GetDirectorById(id);
            if (director == null)
                return NotFound();
            return Ok(director);
        }

    }
}
