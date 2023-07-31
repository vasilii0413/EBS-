using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models.AppDBContext;
using MediatR;
using LazyCache;
using CarRental.Data.Fuel.Queries;
using CarRental.Data.Fuel.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CarRental.Data.Fuel
{
    public class FuelsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public FuelsController(AppDBContext context,IMediator mediator,IAppCache cache)
        {
            _context = context;
            _mediator = mediator;
            _cache = cache;
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> IndexFuel()
        {
            List<Fuel> fuels = await _cache.GetOrAddAsync("AllFuels", async () => await _mediator.Send(new GetAllFuelsQuery()));

            return View(fuels);
        }

        
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fuel fuel = await _cache.GetOrAddAsync($"Fuel_{id}", async () => await _mediator.Send(new GetSingleFuelQuery(id.Value)));

            if (fuel == null)
            {
                return NotFound();
            }

            return View(fuel);
        }

        
        public IActionResult CreateFuel()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFuel([Bind("FuelId,FuelType")] Fuel fuel)
        {
            try
            {
                await _mediator.Send(new AddFuelCommand(fuel));
                _cache.Remove("AllFuels");
                return RedirectToAction("IndexFuel");
            }
            catch
            {
                return NotFound();
            }
        }

        
        public async Task<IActionResult> EditFuel(byte? id)
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

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditFuel(byte id, [Bind("FuelId,FuelType")] Fuel fuel)
        {
            if (id != fuel.FuelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _mediator.Send(new UpdateFuelCommand(fuel));
                    _cache.Remove("AllFuels");

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
                return RedirectToAction("IndexFuel");
            }
            return View(fuel);
        }

        
        public async Task<IActionResult> DeleteFuel(byte? id)
        {
            if (id == null || _context.Fuel == null)
            {
                return NotFound();
            }

            Fuel fuel = await _mediator.Send(new GetSingleFuelQuery(id.Value));

            if (fuel == null)
            {
                return NotFound();
            }

            return View(fuel);
        }

        
        [HttpPost, ActionName("DeleteFuel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            try
            {
                Fuel fuel = await _mediator.Send(new GetSingleFuelQuery(id));
                if (fuel == null)
                {
                    return NotFound();
                }
                await _mediator.Send(new DeleteFuelCommand(fuel));
                _cache.Remove("AllFuels");

                return RedirectToAction("IndexFuel");
            }
            catch
            {
                return NotFound();
            }
        }

        private bool FuelExists(byte id)
        {
          return (_context.Fuel?.Any(e => e.FuelId == id)).GetValueOrDefault();
        }
    }
}
