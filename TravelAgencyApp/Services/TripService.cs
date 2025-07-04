using Microsoft.EntityFrameworkCore;
using TravelAgencyApp.Data;
using TravelAgencyApp.DTOs;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Services
{
    public class TripService
    {
        private readonly AppDbContext _context;

        public TripService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TripDTO>> GetAllAsync()
        {
            return await _context.Trips
                .Select(t => new TripDTO
                {
                    TripId = t.TripId,
                    Name = t.Name,
                    Destination = t.Destination,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Price = t.Price
                }).ToListAsync();
        }

        public async Task<TripDTO?> GetByIdAsync(int id)
        {
            var t = await _context.Trips.FindAsync(id);
            if (t == null) return null;

            return new TripDTO
            {
                TripId = t.TripId,
                Name = t.Name,
                Destination = t.Destination,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                Price = t.Price
            };
        }

        public async Task CreateAsync(TripDTO dto)
        {
            var trip = new Trip
            {
                Name = dto.Name,
                Destination = dto.Destination,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Price = dto.Price
            };

            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TripDTO dto)
        {
            var existing = await _context.Trips.FindAsync(dto.TripId);
            if (existing == null) return;

            existing.Name = dto.Name;
            existing.Destination = dto.Destination;
            existing.StartDate = dto.StartDate;
            existing.EndDate = dto.EndDate;
            existing.Price = dto.Price;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var t = await _context.Trips.FindAsync(id);
            if (t != null)
            {
                _context.Trips.Remove(t);
                await _context.SaveChangesAsync();
            }
        }
    }
}
