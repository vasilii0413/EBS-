using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using CarRental.Models.AppDBContext;

namespace CarRental.Controllers
{
    public class ReservationStatusController : Controller
    {
        private readonly AppDBContext _context;

        public ReservationStatusController(AppDBContext context)
        {
            _context = context;
        }

        // GET: ReservationStatus
        public async Task<IActionResult> Index()
        {
              return _context.ReservationStatus != null ? 
                          View(await _context.ReservationStatus.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.ReservationStatus'  is null.");
        }

        // GET: ReservationStatus/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.ReservationStatus == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatus
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservationStatus == null)
            {
                return NotFound();
            }

            return View(reservationStatus);
        }

        // GET: ReservationStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReservationStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,Status")] ReservationStatus reservationStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationStatus);
        }

        // GET: ReservationStatus/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.ReservationStatus == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatus.FindAsync(id);
            if (reservationStatus == null)
            {
                return NotFound();
            }
            return View(reservationStatus);
        }

        // POST: ReservationStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("ReservationId,Status")] ReservationStatus reservationStatus)
        {
            if (id != reservationStatus.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationStatusExists(reservationStatus.ReservationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservationStatus);
        }

        // GET: ReservationStatus/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.ReservationStatus == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatus
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservationStatus == null)
            {
                return NotFound();
            }

            return View(reservationStatus);
        }

        // POST: ReservationStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.ReservationStatus == null)
            {
                return Problem("Entity set 'AppDBContext.ReservationStatus'  is null.");
            }
            var reservationStatus = await _context.ReservationStatus.FindAsync(id);
            if (reservationStatus != null)
            {
                _context.ReservationStatus.Remove(reservationStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationStatusExists(byte id)
        {
          return (_context.ReservationStatus?.Any(e => e.ReservationId == id)).GetValueOrDefault();
        }
    }
}
