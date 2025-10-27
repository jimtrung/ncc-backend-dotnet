using Microsoft.EntityFrameworkCore;
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

        public async Task<Country> AddAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<Country?> GetByCodeAsync(string code)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<Country?> GetByFieldAsync(string fieldName, object value)
        {
            return fieldName.ToLower() switch
            {
                "code" => await _context.Countries.FirstOrDefaultAsync(c => c.Code == (string)value),
                "name" => await _context.Countries.FirstOrDefaultAsync(c => c.Name == (string)value),
                "iso3" => await _context.Countries.FirstOrDefaultAsync(c => c.Iso3 == (string)value),
                "phonecode" => await _context.Countries.FirstOrDefaultAsync(c => c.PhoneCode == (string)value),
                _ => null
            };
        }

        public async Task<bool> UpdateByFieldAsync(string code, string fieldName, object value)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Code == code);
            if (country == null)
                return false;

            var prop = typeof(Country).GetProperty(fieldName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (prop == null)
                throw new ArgumentException($"Field '{fieldName}' does not exist on Country entity.");

            prop.SetValue(country, value);
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string code)
        {
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Code == code);
            if (country == null)
                return false;

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
