using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Theater_Management_BE.src.Domain.Entities;

namespace Theater_Management_BE.src.Domain.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetTicketsByUserId(Guid userId);
        Task<Ticket> InsertTicket(Ticket ticket);
        Task<IEnumerable<Ticket>> GetAllTickets();
        Task<bool> DeleteTicketById(Guid ticketId);
        Task DeleteAllTickets();
        Task<Ticket?> GetTicketById(Guid ticketId);
    }
}
