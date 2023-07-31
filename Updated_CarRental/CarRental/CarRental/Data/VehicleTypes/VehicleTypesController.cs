using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models.AppDBContext;
using MediatR;
using LazyCache;
using CarRental.Data.VehicleTypes.Queries;
using CarRental.Data.VehicleTypes.Commands;
using Microsoft.AspNetCore.Authorization;

namespace CarRental.Data.VehicleTypes
{
    public class VehicleTypesController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public VehicleTypesController(AppDBContext context, IMediator mediator, IAppCache cache)
        {
            _context = context;
            _mediator = mediator;
            _cache = cache;
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> IndexVehicleType()
        {
            List<VehicleType> vehicleTypes = await _cache.GetOrAddAsync("AllVehicleTypes", async () => await _mediator.Send(new GetAllVehicleTypesQuery()));

            return View(vehicleTypes);
        }

        public async Task<IActionResult> DetailsVehicleType(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VehicleType vehicleType = await _cache.GetOrAddAsync($"VehicleType_{id}", async () => await _mediator.Send(new GetSingleVehicleTypeQuery(id.Value)));


            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }

        public IActionResult CreateVehicleType()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVehicleType([Bind("VehicleTypeId,VehicleTypeName")] VehicleType vehicleType)
        {
            try
            {
                await _mediator.Send(new AddVehicleTypeCommand(vehicleType));
                _cache.Remove("AllVehicleTypes");
                return RedirectToAction(nameof(IndexVehicleType));
            }
            catch
            {
                return NotFound();
            }
        }


        public async Task<IActionResult> EditVehicleType(byte? id)
        {
            if (id == null || _context.VehicleType == null)
            {
                return NotFound();
            }

            var vehicleType = await _context.VehicleType.FindAsync(id);
            if (vehicleType == null)
            {
                return NotFound();
            }
            return View(vehicleType);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVehicleType(byte id, [Bind("VehicleTypeId,VehicleTypeName")] VehicleType vehicleType)
        {
            try
            {
                await _mediator.Send(new UpdateVehicleTypeCommand(vehicleType));
                _cache.Remove("AllVehicleTypes");

            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(IndexVehicleType));

        }


        public async Task<IActionResult> DeleteVehicleType(byte? id)
        {
            if (id == null || _context.VehicleType == null)
            {
                return NotFound();
            }

            VehicleType vehicleType = await _mediator.Send(new GetSingleVehicleTypeQuery(id.Value));

            if (vehicleType == null)
            {
                return NotFound();
            }

            return View(vehicleType);
        }


        [HttpPost, ActionName("DeleteVehicleType")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            try
            {
                VehicleType vehicleType = await _mediator.Send(new GetSingleVehicleTypeQuery(id));
                if (vehicleType == null)
                {
                    return NotFound();
                }
                await _mediator.Send(new DeleteVehicleTypeCommand(vehicleType));
                _cache.Remove("AllVehicleTypes");

                return RedirectToAction(nameof(IndexVehicleType));
            }
            catch
            {
                return NotFound();
            }
        }

        private bool VehicleTypeExists(byte id)
        {
            return (_context.VehicleType?.Any(e => e.VehicleTypeId == id)).GetValueOrDefault();
        }
    }
}
