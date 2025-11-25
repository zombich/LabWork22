using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLibrary.DTOs
{
    public class TicketDTO
    {
        public int TicketId {get;set;}
        public string FilmName {get;set;}
        public string StartSession { get; set; }
        public string CinemaName { get; set; }
        public byte HallId { get; set;}
        public byte Row { get; set; }
        public byte Seat { get; set; }
    }
}
