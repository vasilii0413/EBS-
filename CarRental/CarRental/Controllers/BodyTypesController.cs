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
    public class BodyTypesController : Controller
    {
        private readonly AppDBContext _context;

        public BodyTypesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: BodyTypes
        public async Task<IActionResult> Index()
        {
              return _context.BodyType != null ? 
                          View(await _context.BodyType.ToListAsync()) :
                          Problem("Entity set 'AppDBContext.BodyType'  is null.");
        }

        // GET: BodyTypes/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.BodyType == null)
            {
                return NotFound();
            }

            var bodyType = await _context.BodyType
                .FirstOrDefaultAsync(m => m.BodyTypeId == id);
            if (bodyType == null)
            {
                return NotFound();
            }

            return View(bodyType);
        }

        // GET: BodyTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BodyTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BodyTypeId,Body")] BodyType bodyType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bodyType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bodyType);
        }

        // GET: BodyTypes/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.BodyType == null)
            {
                return NotFound();
            }

            var bodyType = await _context.BodyType.FindAsync(id);
            if (bodyType == null)
            {
                return NotFound();
            }
            return View(bodyType);
        }

        // POST: BodyTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("BodyTypeId,Body")] BodyType bodyType)
        {
            if (id != bodyType.BodyTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bodyType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BodyTypeExists(bodyType.BodyTypeId))
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
            return View(bodyType);
        }

        // GET: BodyTypes/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.BodyType == null)
            {
                return NotFound();
            }

            var bodyType = await _context.BodyType
                .FirstOrDefaultAsync(m => m.BodyTypeId == id);
            if (bodyType == null)
            {
                return NotFound();
            }

            return View(bodyType);
        }

        // POST: BodyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.BodyType == null)
            {
                return Problem("Entity set 'AppDBContext.BodyType'  is null.");
            }
            var bodyType = await _context.BodyType.FindAsync(id);
            if (bodyType != null)
            {
                _context.BodyType.Remove(bodyType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BodyTypeExists(byte id)
        {
          return (_context.BodyType?.Any(e => e.BodyTypeId == id)).GetValueOrDefault();
        }
    }
}
