using System;
using System.Linq;
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

        public void Insert(Ticket ticket)
        {
            try
            {
                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to insert new ticket", ex);
            }
        }

        public Ticket? GetByField(string fieldName, object fieldValue)
        {
            try
            {
                var property = typeof(Ticket).GetProperty(fieldName);
                if (property == null)
                    throw new Exception($"Invalid field name: {fieldName}");

                return _context.Tickets
                    .AsEnumerable()
                    .FirstOrDefault(t => property.GetValue(t)?.Equals(fieldValue) ?? false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get ticket by field: {fieldName}", ex);
            }
        }

        public void UpdateByField(Guid id, string fieldName, object fieldValue)
        {
            try
            {
                var ticket = _context.Tickets.Find(id);
                if (ticket == null)
                    throw new Exception($"Ticket with ID {id} not found");

                var property = typeof(Ticket).GetProperty(fieldName);
                if (property == null)
                    throw new Exception($"Invalid field name: {fieldName}");

                property.SetValue(ticket, fieldValue);
                _context.Tickets.Update(ticket);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to update ticket field: {fieldName}", ex);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var ticket = _context.Tickets.Find(id);
                if (ticket == null)
                    throw new Exception($"Ticket with ID {id} not found");

                _context.Tickets.Remove(ticket);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to delete ticket with ID: {id}", ex);
            }
        }
    }
}
