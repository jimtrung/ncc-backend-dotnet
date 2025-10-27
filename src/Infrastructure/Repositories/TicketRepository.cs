using Microsoft.EntityFrameworkCore;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Domain.Repositories;
using Theater_Management_BE.src.Infrastructure.Data;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Ticket ticket)
        {
            try
            {
                await _context.Tickets.AddAsync(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert new ticket", ex);
            }
        }

        public async Task<Ticket?> GetByFieldAsync(string fieldName, object fieldValue)
        {
            try
            {
                var property = typeof(Ticket).GetProperty(fieldName);
                if (property == null)
                    throw new Exception($"Invalid field name: {fieldName}");

                return await _context.Tickets
                    .FirstOrDefaultAsync(t => EF.Property<object>(t, fieldName).Equals(fieldValue));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get ticket by field: {fieldName}", ex);
            }
        }

        public async Task UpdateByFieldAsync(Guid id, string fieldName, object fieldValue)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(id);
                if (ticket == null)
                    throw new Exception($"Ticket with ID {id} not found");

                var property = typeof(Ticket).GetProperty(fieldName);
                if (property == null)
                    throw new Exception($"Invalid field name: {fieldName}");

                property.SetValue(ticket, fieldValue);
                _context.Tickets.Update(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update ticket field: {fieldName}", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(id);
                if (ticket == null)
                    throw new Exception($"Ticket with ID {id} not found");

                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete ticket with ID: {id}", ex);
            }
        }
    }
}
