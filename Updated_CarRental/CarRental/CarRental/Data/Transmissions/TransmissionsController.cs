using Microsoft.AspNetCore.Mvc;
using CarRental.Models.AppDBContext;
using MediatR;
using System.Data;
using LazyCache;
using CarRental.Data.Transmissions.Queries;
using CarRental.Data.Transmissions.Commands;

namespace CarRental.Data.Transmissions
{
    public class TransmissionsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public TransmissionsController(AppDBContext context, IMediator mediator, IAppCache cache)
        {
            _context = context;
            _mediator = mediator;
            _cache = cache;
        }


        public async Task<IActionResult> IndexTransmission()
        {
            List<Transmission> transmissions = await _cache.GetOrAddAsync("AllTransmissions", async () => await _mediator.Send(new GetAllTransmissionsQuery()));

            return View(transmissions);
        }


        public async Task<IActionResult> DetailsTransmission(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Transmission transmission = await _cache.GetOrAddAsync($"Transmission_{id}", async () => await _mediator.Send(new GetSingleTransmissionQuery(id.Value)));

            if (transmission == null)
            {
                return NotFound();
            }

            return View(transmission);
        }

        public IActionResult CreateTransmission()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTransmission([Bind("TransmissionId,TransmissionType")] Transmission transmission)
        {
            try
            {
                await _mediator.Send(new AddTransmissionCommand(transmission));
                _cache.Remove("AllTransmissions");
                return RedirectToAction(nameof(IndexTransmission));
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> EditTransmission(byte? id)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTransmission(byte id, [Bind("TransmissionId,TransmissionType")] Transmission transmission)
        {
            try
            {
                await _mediator.Send(new UpdateTransmissionCommand(transmission));
                _cache.Remove("AllTransmissions");
            }
            catch (DBConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(IndexTransmission));

        }


        public async Task<IActionResult> DeleteTransmission(byte? id)
        {
            if (id == null || _context.Transmission == null)
            {
                return NotFound();
            }
            Transmission transmission = await _mediator.Send(new GetSingleTransmissionQuery(id.Value));
            if (transmission == null)
            {
                return NotFound();
            }

            return View(transmission);
        }


        [HttpPost, ActionName("DeleteTransmission")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            try
            {
                Transmission transmission = await _mediator.Send(new GetSingleTransmissionQuery(id));
                if (transmission == null)
                {
                    return NotFound();
                }
                await _mediator.Send(new DeleteTransmissionCommand(transmission));
                _cache.Remove("AllTransmissions");
                return RedirectToAction(nameof(IndexTransmission));
            }
            catch
            {
                return NotFound();
            }
        }

        private bool TransmissionExists(byte id)
        {
            return (_context.Transmission?.Any(e => e.TransmissionId == id)).GetValueOrDefault();
        }
    }
}
