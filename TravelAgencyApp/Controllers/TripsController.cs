using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelAgencyApp.DTOs;
using TravelAgencyApp.Services;

namespace TravelAgencyApp.Controllers
{
    [Authorize]
    public class TripsController : Controller
    {
        private readonly TripService _tripService;

        public TripsController(TripService tripService)
        {
            _tripService = tripService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _tripService.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var trip = await _tripService.GetByIdAsync(id.Value);
            if (trip == null) return NotFound();

            return View(trip);
        }

        [Authorize(Roles = "Admin,Worker")]

        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Worker")]

        public async Task<IActionResult> Create(TripDTO tripDto)
        {
            if (!ModelState.IsValid) return View(tripDto);

            await _tripService.CreateAsync(tripDto);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var trip = await _tripService.GetByIdAsync(id.Value);
            if (trip == null) return NotFound();

            // Map to DTO
            var dto = new TripDTO
            {
                TripId = trip.TripId,
                Name = trip.Name,
                Destination = trip.Destination,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Price = trip.Price
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Edit(int id, TripDTO tripDto)
        {
            if (id != tripDto.TripId) return NotFound();
            if (!ModelState.IsValid) return View(tripDto);

            try
            {
                await _tripService.UpdateAsync(tripDto);
            }
            catch
            {
                if (await _tripService.GetByIdAsync(id) == null)
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var trip = await _tripService.GetByIdAsync(id.Value);
            if (trip == null) return NotFound();

            return View(trip);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tripService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
