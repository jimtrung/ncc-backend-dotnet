using System.Reflection;
using Theater_Management_BE.src.Application.Interfaces;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Infrastructure.Data;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _context;

        public CountryRepository(AppDbContext context)
        {
            _context = context;
        }

        public Country Add(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
            return country;
        }

        public Country? GetByCode(string code)
        {
            return _context.Countries.FirstOrDefault(c => c.Code == code);
        }

        public Country? GetByField(string fieldName, object value)
        {
            return fieldName.ToLower() switch
            {
                "code" => _context.Countries.FirstOrDefault(c => c.Code == (string)value),
                "name" => _context.Countries.FirstOrDefault(c => c.Name == (string)value),
                "iso3" => _context.Countries.FirstOrDefault(c => c.Iso3 == (string)value),
                "phonecode" => _context.Countries.FirstOrDefault(c => c.PhoneCode == (string)value),
                _ => null
            };
        }

        public bool UpdateByField(string code, string fieldName, object value)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Code == code);
            if (country == null) return false;

            var prop = typeof(Country).GetProperty(fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null)
                throw new ArgumentException($"Field '{fieldName}' does not exist on Country entity.");

            prop.SetValue(country, value);
            _context.Countries.Update(country);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(string code)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Code == code);
            if (country == null) return false;

            _context.Countries.Remove(country);
            _context.SaveChanges();
            return true;
        }
    }
}
