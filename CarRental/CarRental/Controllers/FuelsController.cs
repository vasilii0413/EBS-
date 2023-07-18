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
    public class FuelsController : Controller
    {
        private readonly AppDBContext _context;

        public FuelsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Fuels
        public async Task<IActionResult> Index()
        {
              return _context.Fuel != null ? 
                          View(await _context.Fuel.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.Fuel'  is null.");
        }

        // GET: Fuels/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Fuel == null)
            {
                return NotFound();
            }

            var fuel = await _context.Fuel
                .FirstOrDefaultAsync(m => m.FuelId == id);
            if (fuel == null)
            {
                return NotFound();
            }

            return View(fuel);
        }

        // GET: Fuels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fuels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FuelId,FuelType")] Fuel fuel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fuel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fuel);
        }

        // GET: Fuels/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Fuel == null)
            {
                return NotFound();
            }

            var fuel = await _context.Fuel.FindAsync(id);
            if (fuel == null)
            {
                return NotFound();
            }
            return View(fuel);
        }

        // POST: Fuels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("FuelId,FuelType")] Fuel fuel)
        {
            if (id != fuel.FuelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fuel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuelExists(fuel.FuelId))
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
            return View(fuel);
        }

        // GET: Fuels/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Fuel == null)
            {
                return NotFound();
            }

            var fuel = await _context.Fuel
                .FirstOrDefaultAsync(m => m.FuelId == id);
            if (fuel == null)
            {
                return NotFound();
            }

            return View(fuel);
        }

        // POST: Fuels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Fuel == null)
            {
                return Problem("Entity set 'AppDBContext.Fuel'  is null.");
            }
            var fuel = await _context.Fuel.FindAsync(id);
            if (fuel != null)
            {
                _context.Fuel.Remove(fuel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuelExists(byte id)
        {
          return (_context.Fuel?.Any(e => e.FuelId == id)).GetValueOrDefault();
        }
    }
}
