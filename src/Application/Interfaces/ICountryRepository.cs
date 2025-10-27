using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface ICountryRepository
    {
        Task<Country> AddAsync(Country country);
        Task<Country?> GetByCodeAsync(string code);
        Task<Country?> GetByFieldAsync(string fieldName, object value);
        Task<bool> UpdateByFieldAsync(string code, string fieldName, object value);
        Task<bool> DeleteAsync(string code);
    }
}
