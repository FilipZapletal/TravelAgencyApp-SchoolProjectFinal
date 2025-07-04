using Microsoft.EntityFrameworkCore;
using TravelAgencyApp.Data;
using TravelAgencyApp.Models;

namespace TravelAgencyApp.Services
{
    public class CustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync() => await _context.Customers.ToListAsync();

        public async Task<Customer?> GetByIdAsync(int id) => await _context.Customers.FindAsync(id);

        public async Task CreateAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer updatedCustomer)
        {
            var existing = await _context.Customers.FindAsync(updatedCustomer.CustomerId);
            if (existing == null) return;

            existing.FullName = updatedCustomer.FullName;
            existing.Email = updatedCustomer.Email;

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
