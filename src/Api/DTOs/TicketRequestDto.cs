using System;

namespace Theater_Management_BE.src.Api.Dtos
{
    public class TicketRequestDto
    {
        public Guid Userid { get; set; }
        public Guid Showtimeid { get; set; }
        public int Price { get; set; }
        public String Seatname { get; set; }
    }
}
