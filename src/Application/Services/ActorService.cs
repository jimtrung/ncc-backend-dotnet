using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Services
{
    public class ActorService
    {
        private readonly IActorRepository _actorRepository;

        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public List<Actor> GetAllActors()
        {
            return _actorRepository.GetAll();
        }
    }
}
