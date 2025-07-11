﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgencyApp.DTOs;
using TravelAgencyApp.Models;
using TravelAgencyApp.Services;

namespace TravelAgencyApp.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly BookingService _bookingService;
        private readonly TripService _tripService;
        private readonly CustomerService _customerService;

        public BookingsController(BookingService bookingService, TripService tripService, CustomerService customerService)
        {
            _bookingService = bookingService;
            _tripService = tripService;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _bookingService.GetAllAsync(); 
            return View(bookings);
        }

        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        [Authorize(Roles = "Admin,Worker,User")]
        public async Task<IActionResult> Create()
        {
            var trips = await _tripService.GetAllAsync();
            ViewBag.Trips = new SelectList(trips, "TripId", "Name");

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Worker,User")]
        public async Task<IActionResult> Create(BookingDTO dto)
        {
            // 🔍 Get current user's email
            var userEmail = User.Identity?.Name;

            // 🧠 Find corresponding customer from email
            var customers = await _customerService.GetAllAsync();
            var currentCustomer = customers.FirstOrDefault(c => c.Email == userEmail);

            if (currentCustomer == null)
            {
                // ❌ Something went wrong, no customer matched
                ModelState.AddModelError("", "No associated customer found.");
                var trips = await _tripService.GetAllAsync();
                ViewBag.Trips = new SelectList(trips, "TripId", "Name");
                return View(dto);
            }

            // 💥 Inject the real customer ID into the DTO
            dto.CustomerId = currentCustomer.CustomerId;

            if (!ModelState.IsValid)
            {
                var trips = await _tripService.GetAllAsync();
                ViewBag.Trips = new SelectList(trips, "TripId", "Name");
                return View(dto);
            }

            await _bookingService.CreateAsync(dto);
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return NotFound();

            var trips = await _tripService.GetAllAsync();
            var customers = await _customerService.GetAllAsync();

            ViewBag.Trips = new SelectList(trips, "TripId", "Name", booking.TripId);
            ViewBag.Customers = new SelectList(customers, "CustomerId", "FullName", booking.CustomerId);

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Edit(int id, BookingDTO dto)
        {
            if (id != dto.BookingId) return NotFound();

            if (!ModelState.IsValid)
            {
                var trips = await _tripService.GetAllAsync();
                var customers = await _customerService.GetAllAsync();

                ViewBag.Trips = new SelectList(trips, "TripId", "Name", dto.TripId);
                ViewBag.Customers = new SelectList(customers, "CustomerId", "FullName", dto.CustomerId);

                return View(dto);
            }

            await _bookingService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return NotFound();

            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Worker")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookingService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Worker")]
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            await _bookingService.ChangeStatusAsync(id, BookingStatus.Approved);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin,Worker")]
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            await _bookingService.ChangeStatusAsync(id, BookingStatus.Rejected);
            return RedirectToAction(nameof(Index));
        }

    }
}
