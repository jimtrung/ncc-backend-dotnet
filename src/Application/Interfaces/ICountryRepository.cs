using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Application.Interfaces
{
    public interface ICountryRepository
    {
        Country Add(Country country);
        Country? GetByCode(string code);
        Country? GetByField(string fieldName, object value);
        bool UpdateByField(string code, string fieldName, object value);
        bool Delete(string code);
    }
}
