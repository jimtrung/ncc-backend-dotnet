using System.Collections.Generic;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Services
{
    public class DirectorService
    {
        private readonly IDirectorRepository _directorRepository;

        public DirectorService(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        public List<Director> GetAllDirectors()
        {
            return _directorRepository.GetAll();
        }

        public Director? GetDirectorById(Guid id)
        {
            return _directorRepository.GetById(id);
        }

    }
}
