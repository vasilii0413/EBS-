using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models.AppDBContext;
using MediatR;
using LazyCache;
using CarRental.Data.TypeOfDrivings.Queries;
using CarRental.Data.TypeOfDrivings.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CarRental.Data.TypeOfDrivings
{
    public class TypeOfDrivingsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public TypeOfDrivingsController(AppDBContext context, IMediator mediator, IAppCache cache)
        {
            _context = context;
            _mediator = mediator;
            _cache = cache;
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> IndexTypeOfDriving()
        {
            List<TypeOfDriving> typesOfDriving = await _cache.GetOrAddAsync("AllTypesOfDriving", async () => await _mediator.Send(new GetAllTypesOfDrivingQuery()));

            return View(typesOfDriving);
        }


        public async Task<IActionResult> DetailsTypeOfDriving(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TypeOfDriving typeD = await _cache.GetOrAddAsync($"TypeOfDriving_{id}", async () => await _mediator.Send(new GetSingleTypeOfDrivingQuery(id.Value)));

            if (typeD == null)
            {
                return NotFound();
            }

            return View(typeD);
        }


        public IActionResult CreateTypeOfDriving()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTypeOfDriving([Bind("DriveTypeId,TypeDrive")] TypeOfDriving typeOfDriving)
        {
            try
            {
                await _mediator.Send(new AddDriveTypeCommand(typeOfDriving));
                _cache.Remove("AllTypesOfDriving");
                return RedirectToAction(nameof(IndexTypeOfDriving));
            }
            catch
            {
                return NotFound();
            }
        }


        public async Task<IActionResult> EditTypeOfDriving(byte? id)
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTypeOfDriving(byte id, [Bind("DriveTypeId,TypeDrive")] TypeOfDriving typeOfDriving)
        {
            try
            {
                await _mediator.Send(new UpdateDriveTypeCommand(typeOfDriving));
                _cache.Remove("AllTypesOfDriving");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(IndexTypeOfDriving));

        }


        public async Task<IActionResult> DeleteTypeOfDriving(byte? id)
        {
            if (id == null || _context.TypeOfDriving == null)
            {
                return NotFound();
            }

            TypeOfDriving typeD = await _mediator.Send(new GetSingleTypeOfDrivingQuery(id.Value));

            if (typeD == null)
            {
                return NotFound();
            }
            return View(typeD);
        }


        [HttpPost, ActionName("DeleteTypeOfDriving")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            try
            {
                TypeOfDriving typeD = await _mediator.Send(new GetSingleTypeOfDrivingQuery(id));
                if (typeD == null)
                {
                    return NotFound();
                }
                await _mediator.Send(new DeleteDriveTypeCommand(typeD));
                _cache.Remove("AllTypesOfDriving");
                return RedirectToAction(nameof(IndexTypeOfDriving));
            }
            catch
            {
                return NotFound();
            }
        }

        private bool TypeOfDrivingExists(byte id)
        {
            return (_context.TypeOfDriving?.Any(e => e.DriveTypeId == id)).GetValueOrDefault();
        }
    }
}
