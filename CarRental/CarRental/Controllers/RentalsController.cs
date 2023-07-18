using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using CarRental.Models.AppDBContext;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CarRental.Controllers
{
    public class RentalsController : Controller
    {
        private readonly AppDBContext _context;

        public RentalsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Rental
                .Include(r => r.ReservationStatus)
                .Include(r => r.Vehicle)
                .Include(r => r.User);
            return View(await appDBContext.ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.ReservationStatus)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        [Authorize]
        public IActionResult Create(int vehicleId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email");
            ViewData["ReservationId"] = new SelectList(_context.Set<ReservationStatus>(), "ReservationId", "ReservationId");
            ViewData["VehicleId"] = new SelectList(_context.Set<Vehicle>(), "VehicleId", "VehicleId");
            ViewData["VehicleId"] = vehicleId;
            return View();
        }

        // POST: Rentals/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalId,RentalDate,ReturnDate,VehicleId,ReservationId,UserId")]Rental rental)
        {
            try
            {
                rental.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                rental.VehicleId = Convert.ToInt16(Request.Form["VehicleId"]);


                _context.Add(rental); 
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Vehicles");
            }
            catch 
            { 
                return NotFound();
            }
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            
            ViewData["Reservations"] = new SelectList(_context.ReservationStatus, "ReservationId", "Status");
            ViewData["Vehicles"] = new SelectList(_context.Vehicle, "VehicleId", "Name");
            ViewData["Users"] = new SelectList(_context.Vehicle, "Id", "Email");


            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("RentalId,UserId,RentalDate,ReturnDate,VehicleId,ReservationId")] Rental rental)
        {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
            }
                return RedirectToAction("Index", "Rentals");
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.ReservationStatus)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'AppDBContext.Rental'  is null.");
            }
            var rental = await _context.Rental.FindAsync(id);
            if (rental != null)
            {
                _context.Rental.Remove(rental);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(short id)
        {
          return (_context.Rental?.Any(e => e.RentalId == id)).GetValueOrDefault();
        }
    }
}
