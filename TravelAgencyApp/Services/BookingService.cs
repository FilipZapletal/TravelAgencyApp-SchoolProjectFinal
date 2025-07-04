using Microsoft.EntityFrameworkCore;
using TravelAgencyApp.Data;
using TravelAgencyApp.DTOs;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Services
{
    public class BookingService
    {
        private readonly AppDbContext _context;

        public BookingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookingDTO>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Trip)
                .Select(b => new BookingDTO
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    CustomerId = b.CustomerId,
                    TripId = b.TripId,
                    TripName = b.Trip != null ? b.Trip.Name : "Unknown Trip",
                    CustomerName = b.Customer != null ? b.Customer.FullName : "Unknown Customer"
                })
                .ToListAsync();
        }

        public async Task<BookingDTO?> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Trip)
                .Where(b => b.BookingId == id)
                .Select(b => new BookingDTO
                {
                    BookingId = b.BookingId,
                    BookingDate = b.BookingDate,
                    CustomerId = b.CustomerId,
                    TripId = b.TripId,
                    TripName = b.Trip != null ? b.Trip.Name : "Unknown Trip",
                    CustomerName = b.Customer != null ? b.Customer.FullName : "Unknown Customer"
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(BookingDTO dto)
        {
            var booking = new Models.Booking
            {
                BookingDate = dto.BookingDate,
                TripId = dto.TripId,
                CustomerId = dto.CustomerId
            };
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BookingDTO dto)
        {
            var existing = await _context.Bookings.FindAsync(dto.BookingId);
            if (existing == null) return;

            existing.BookingDate = dto.BookingDate;
            existing.TripId = dto.TripId;
            existing.CustomerId = dto.CustomerId;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Trip>> GetAllTripsAsync() =>
    await _context.Trips.ToListAsync();

        public async Task<List<Customer>> GetAllCustomersAsync() =>
            await _context.Customers.ToListAsync();

    }
}
