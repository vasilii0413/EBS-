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
    public class TransmissionsController : Controller
    {
        private readonly AppDBContext _context;

        public TransmissionsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Transmissions
        public async Task<IActionResult> Index()
        {
              return _context.Transmission != null ? 
                          View(await _context.Transmission.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Transmission'  is null.");
        }

        // GET: Transmissions/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Transmission == null)
            {
                return NotFound();
            }

            var transmission = await _context.Transmission
                .FirstOrDefaultAsync(m => m.TransmissionId == id);
            if (transmission == null)
            {
                return NotFound();
            }

            return View(transmission);
        }

        // GET: Transmissions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transmissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransmissionId,TransmissionType")] Transmission transmission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transmission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transmission);
        }

        // GET: Transmissions/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Transmission == null)
            {
                return NotFound();
            }

            var transmission = await _context.Transmission.FindAsync(id);
            if (transmission == null)
            {
                return NotFound();
            }
            return View(transmission);
        }

        // POST: Transmissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("TransmissionId,TransmissionType")] Transmission transmission)
        {
            if (id != transmission.TransmissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transmission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransmissionExists(transmission.TransmissionId))
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
            return View(transmission);
        }

        // GET: Transmissions/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Transmission == null)
            {
                return NotFound();
            }

            var transmission = await _context.Transmission
                .FirstOrDefaultAsync(m => m.TransmissionId == id);
            if (transmission == null)
            {
                return NotFound();
            }

            return View(transmission);
        }

        // POST: Transmissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Transmission == null)
            {
                return Problem("Entity set 'AppDBContext.Transmission'  is null.");
            }
            var transmission = await _context.Transmission.FindAsync(id);
            if (transmission != null)
            {
                _context.Transmission.Remove(transmission);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransmissionExists(byte id)
        {
          return (_context.Transmission?.Any(e => e.TransmissionId == id)).GetValueOrDefault();
        }
    }
}
