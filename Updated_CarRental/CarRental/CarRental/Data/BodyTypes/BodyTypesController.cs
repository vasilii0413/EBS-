using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models.AppDBContext;
using MediatR;
using LazyCache;
using CarRental.Data.BodyType.Queries;
using CarRental.Data.BodyType.Commands;

namespace CarRental.Data.BodyType
{
    public class BodyTypesController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public BodyTypesController(AppDBContext context, IMediator mediator, IAppCache cache)
        {
            _context = context;
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<IActionResult> IndexBody()
        {
            List<BodyType> bodyTypes = await _cache.GetOrAddAsync("AllBodyTypes", async () => await _mediator.Send(new GetAllBodyTypesQuery()));

            return View(bodyTypes);
        }


        public async Task<IActionResult> DetailsBody(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BodyType bodyType = await _cache.GetOrAddAsync($"BodyType_{id}", async () => await _mediator.Send(new GetSingleBodyTypeQuery(id.Value)));

            if (bodyType == null)
            {
                return NotFound();
            }

            return View(bodyType);
        }


        public IActionResult CreateBody()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBody([Bind("BodyTypeId,Body")] BodyType bodyType)
        {
            try
            {
                await _mediator.Send(new AddBodyTypeCommand(bodyType));

                _cache.Remove("AllBodyTypes");

                return RedirectToAction(nameof(IndexBody));

            }
            catch
            {
                return NotFound();
            }

        }


        public async Task<IActionResult> EditBody(byte? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBody([Bind("BodyTypeId,Body")] BodyType bodyType)
        {
            try
            {
                await _mediator.Send(new UpdateBodyTypeCommand(bodyType));
                _cache.Remove("AllBodyTypes");

            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(IndexBody));
        }

        public async Task<IActionResult> DeleteBody(byte? id)
        {
            if (id == null || _context.BodyType == null)
            {
                return NotFound();
            }

            BodyType bodyType = await _mediator.Send(new GetSingleBodyTypeQuery(id.Value));

            if (bodyType == null)
            {
                return NotFound();
            }

            return View(bodyType);
        }


        [HttpPost, ActionName("DeleteBody")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            try
            {
                BodyType bodyType = await _mediator.Send(new GetSingleBodyTypeQuery(id));

                if (bodyType == null)
                {
                    return NotFound();
                }
                await _mediator.Send(new DeleteBodyTypeCommand(bodyType));
                _cache.Remove("AllBodyTypes");

                return RedirectToAction(nameof(IndexBody));
            }
            catch
            {
                return NotFound();
            }
        }

        private bool BodyTypeExists(byte id)
        {
            return (_context.BodyType?.Any(e => e.BodyTypeId == id)).GetValueOrDefault();
        }
    }
}
