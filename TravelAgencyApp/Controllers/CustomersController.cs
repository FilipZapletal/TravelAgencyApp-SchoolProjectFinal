using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyApp.DTOs;
using TravelAgencyApp.Models;
using TravelAgencyApp.Services;

namespace TravelAgencyApp.Controllers
{
    [Authorize(Roles = "Admin,Worker")]
    public class CustomersController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _customerService.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var customer = await _customerService.GetByIdAsync(id.Value);
            if (customer == null) return NotFound();

            return View(customer);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerDTO customerDto)
        {
            if (!ModelState.IsValid) return View(customerDto);

            var customer = new Customer
            {
                FullName = customerDto.FullName,
                Email = customerDto.Email
            };

            await _customerService.CreateAsync(customer);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null) return NotFound();

            var dto = new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Email = customer.Email
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CustomerDTO dto)
        {
            if (id != dto.CustomerId) return NotFound();
            if (!ModelState.IsValid) return View(dto);

            var customer = new Customer
            {
                CustomerId = dto.CustomerId,
                FullName = dto.FullName,
                Email = dto.Email
            };

            await _customerService.UpdateAsync(customer);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var customer = await _customerService.GetByIdAsync(id.Value);
            if (customer == null) return NotFound();

            var dto = new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Email = customer.Email
            };

            return View(dto); // 🔥 View expects CustomerDTO
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
