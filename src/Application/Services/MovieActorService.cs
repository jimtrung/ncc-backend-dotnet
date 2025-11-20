using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Application.Interfaces;

namespace Theater_Management_BE.src.Application.Services
{
    public class MovieActorService
    {
        private readonly IMovieActorRepository _movieActorRepository;

        public MovieActorService(IMovieActorRepository movieActorRepository)
        {
            _movieActorRepository = movieActorRepository;
        }

        public void InsertMovieActors(MovieActor ma)
        {
            _movieActorRepository.Add(ma);
        }
    }
}
