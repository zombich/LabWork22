using DatabaseLibrary.Contexts;
using DatabaseLibrary.DTOs;
using DatabaseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLibrary.Services
{
    public class TicketService(CinemaDbContext context)
    {
        private readonly CinemaDbContext _context = context;

        public Task<Ticket?> GetTicketById(int id)
            => _context.Tickets.FirstOrDefaultAsync(t => t.TicketId == id);

        public Task<Film?> GetFilmById(int id)
            => _context.Films.FirstOrDefaultAsync(f => f.FilmId == id);

        public Task<Session?> GetSessionById(int id)
            => _context.Sessions.FirstOrDefaultAsync(s => s.SessionId == id);

        public Task<Hall?> GetHallById(int id)
        => _context.Halls.FirstOrDefaultAsync(h => h.HallId == id);

        public async Task<TicketDTO> GetTicketInfoAsync(Ticket ticket)
        {
            TicketDTO ticketDto = new();
            ticketDto.TicketId = ticket.TicketId;
            ticketDto.Row = ticket.Row;
            ticketDto.Seat = ticket.Seat;

            Session session = await GetSessionById(ticket.SessionId);
            ticketDto.StartSession = session.StartDate.ToString("HH:mm d MMMM");

            Film film = await GetFilmById(session.FilmId);
            ticketDto.FilmName = film.Name;

            Hall hall = await GetHallById(session.HallId);
            ticketDto.CinemaName = hall.Cinema;
            ticketDto.HallId = hall.HallId;

            return ticketDto;
        }
    }
}
