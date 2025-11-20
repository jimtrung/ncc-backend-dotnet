using Microsoft.AspNetCore.Mvc;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Api.Controllers
{
    [ApiController]
    [Route("actor")]
    public class ActorController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;

        public ActorController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        [HttpGet("all")]
        public ActionResult<List<Actor>> GetAllActor()
        {
            var actors = _actorRepository.GetAll();
            return Ok(actors);
        }
    }
}
