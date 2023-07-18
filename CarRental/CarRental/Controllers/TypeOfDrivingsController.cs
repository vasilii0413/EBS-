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
    public class TypeOfDrivingsController : Controller
    {
        private readonly AppDBContext _context;

        public TypeOfDrivingsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: TypeOfDrivings
        public async Task<IActionResult> Index()
        {
              return _context.TypeOfDriving != null ? 
                          View(await _context.TypeOfDriving.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.TypeOfDriving'  is null.");
        }

        // GET: TypeOfDrivings/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.TypeOfDriving == null)
            {
                return NotFound();
            }

            var typeOfDriving = await _context.TypeOfDriving
                .FirstOrDefaultAsync(m => m.DriveTypeId == id);
            if (typeOfDriving == null)
            {
                return NotFound();
            }

            return View(typeOfDriving);
        }

        // GET: TypeOfDrivings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfDrivings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriveTypeId,TypeDrive")] TypeOfDriving typeOfDriving)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfDriving);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfDriving);
        }

        // GET: TypeOfDrivings/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.TypeOfDriving == null)
            {
                return NotFound();
            }

            var typeOfDriving = await _context.TypeOfDriving.FindAsync(id);
            if (typeOfDriving == null)
            {
                return NotFound();
            }
            return View(typeOfDriving);
        }

        // POST: TypeOfDrivings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("DriveTypeId,TypeDrive")] TypeOfDriving typeOfDriving)
        {
            if (id != typeOfDriving.DriveTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfDriving);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfDrivingExists(typeOfDriving.DriveTypeId))
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
            return View(typeOfDriving);
        }

        // GET: TypeOfDrivings/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.TypeOfDriving == null)
            {
                return NotFound();
            }

            var typeOfDriving = await _context.TypeOfDriving
                .FirstOrDefaultAsync(m => m.DriveTypeId == id);
            if (typeOfDriving == null)
            {
                return NotFound();
            }

            return View(typeOfDriving);
        }

        // POST: TypeOfDrivings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.TypeOfDriving == null)
            {
                return Problem("Entity set 'AppDBContext.TypeOfDriving'  is null.");
            }
            var typeOfDriving = await _context.TypeOfDriving.FindAsync(id);
            if (typeOfDriving != null)
            {
                _context.TypeOfDriving.Remove(typeOfDriving);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfDrivingExists(byte id)
        {
          return (_context.TypeOfDriving?.Any(e => e.DriveTypeId == id)).GetValueOrDefault();
        }
    }
}
