using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Theater_Management_BE.src.Domain.Entities;
using Theater_Management_BE.src.Domain.Repositories;
using Theater_Management_BE.src.Infrastructure.Data;
using System.Linq;

namespace Theater_Management_BE.src.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserId(Guid userId)
        {
            return await _context.Tickets
                                 .AsNoTracking()
                                 .Where(t => t.Userid == userId)
                                 .ToListAsync();
        }

        public async Task<Ticket> InsertTicket(Ticket ticket)
        {
            ticket.Id = Guid.NewGuid();
            ticket.CreatedAt = DateTime.UtcNow;
            ticket.UpdatedAt = DateTime.UtcNow;

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetAllTickets()
        {
            return await _context.Tickets
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        // DELETE ticket by ID
        public async Task<bool> DeleteTicketById(Guid ticketId)
        {
            var ticket = await _context.Tickets.FindAsync(ticketId);
            if (ticket == null) return false;

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return true;
        }

        // DELETE all tickets
        public async Task DeleteAllTickets()
        {
            var allTickets = await _context.Tickets.ToListAsync();
            _context.Tickets.RemoveRange(allTickets);
            await _context.SaveChangesAsync();
        }

        // GET ticket by ID
        public async Task<Ticket?> GetTicketById(Guid ticketId)
        {
            // AsNoTracking() nếu chỉ đọc dữ liệu
            return await _context.Tickets
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(t => t.Id == ticketId);
        }

    }
}
