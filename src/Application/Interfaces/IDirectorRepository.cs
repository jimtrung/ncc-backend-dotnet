using Theater_Management_BE.src.Domain.Entities;
using System.Collections.Generic;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface IDirectorRepository
    {
        Director Add(Director director);
        Director? GetById(Guid id);
        Director? GetByField(string fieldName, object value);
        bool UpdateByField(Guid id, string fieldName, object value);
        bool Delete(Guid id);
        List<Director> GetAll();
    }
}
