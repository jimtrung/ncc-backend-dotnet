using Microsoft.AspNetCore.Mvc;
using Theater_Management_BE.src.Application.Services;
using Theater_Management_BE.src.Api.DTOs;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieActorController : Controller
    {
        private readonly MovieActorService _movieActorService;

        public MovieActorController(MovieActorService movieActorService)
        {
            _movieActorService = movieActorService;
        }

        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "administrator")]
        public ActionResult AddMovieActors([FromBody] MovieActorsRequest request)
        {
            foreach (var actorId in request.ActorsId)
            {
                _movieActorService.InsertMovieActors(new MovieActor
                {
                    MovieId = request.MovieId,
                    ActorId = actorId
                });
            }
            return Ok("Thêm diễn viên vào phim thành công");
        }
    }
}
