using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models.AppDBContext;
using MediatR;
using CarRental.Data.Vehicles.Queries;
using CarRental.Data.Vehicles.Commands;

namespace CarRental.Data.Vehicles
{
    public class VehiclesController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;

        public VehiclesController(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<IActionResult> IndexVehicle()
        {
            List<Vehicle> vehicles = await _mediator.Send(new GetAllVehiclesQuery());
            return View(vehicles);
        }



        public async Task<IActionResult> DetailsVehicle(short? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _mediator.Send(new GetSingleVehicleQuery(id.Value));

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        public IActionResult CreateVehicle()
        {
            ViewData["VehicleTypes"] = new SelectList(_context.VehicleType, "VehicleTypeId", "VehicleTypeName");
            ViewData["BodyTypes"] = new SelectList(_context.BodyType, "BodyTypeId", "Body");
            ViewData["Transmissions"] = new SelectList(_context.Transmission, "TransmissionId", "TransmissionType");
            ViewData["Fuels"] = new SelectList(_context.Fuel, "FuelId", "FuelType");
            ViewData["DriveTypes"] = new SelectList(_context.TypeOfDriving, "DriveTypeId", "TypeDrive");

            return View();
        }

        public async Task<IActionResult> ManageCars()
        {
            List<Vehicle> vehicles = await _mediator.Send(new GetAllVehiclesQuery());
            return View(vehicles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVehicle([Bind("VehicleId,Name,VehicleTypeId,BodyTypeId,TransmissionId,Seats,FuelId,DriveTypeId,Year,DailyPrice,Image")] Vehicle vehicle)
        {
            try
            {
                await _mediator.Send(new AddVehicleCommand(vehicle));
                return RedirectToAction(nameof(ManageCars));
            }
            catch
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> EditVehicle(short? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["VehicleTypes"] = new SelectList(_context.VehicleType, "VehicleTypeId", "VehicleTypeName");
            ViewData["BodyTypes"] = new SelectList(_context.BodyType, "BodyTypeId", "Body");
            ViewData["Transmissions"] = new SelectList(_context.Transmission, "TransmissionId", "TransmissionType");
            ViewData["Fuels"] = new SelectList(_context.Fuel, "FuelId", "FuelType");
            ViewData["DriveTypes"] = new SelectList(_context.TypeOfDriving, "DriveTypeId", "TypeDrive");
            return View(vehicle);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditVehicle(short id, [Bind("VehicleId,Name,VehicleTypeId,BodyTypeId,TransmissionId,Seats,FuelId,DriveTypeId,Year,DailyPrice,Image")] Vehicle vehicle)
        {

            try
            {
                await _mediator.Send(new UpdateVehicleCommand(vehicle));
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(ManageCars));
        }


        public async Task<IActionResult> DeleteVehicle(short? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _mediator.Send(new GetSingleVehicleQuery(id.Value));

            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        [HttpPost, ActionName("DeleteVehicle")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            try
            {
                Vehicle vehicle = await _mediator.Send(new GetSingleVehicleQuery(id));
                if (vehicle == null)
                {
                    return NotFound();
                }
                await _mediator.Send(new DeleteVehicleCommand(vehicle));
                return RedirectToAction(nameof(IndexVehicle));
            }
            catch
            {
                return NotFound();
            }
        }

        private bool VehicleExists(short id)
        {
            return (_context.Vehicle?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> FilteredVehicles(string[] filterBodyType, string[] filterTransmission)
        {

            var allVehicles = await _mediator.Send(new GetAllVehiclesQuery());

            var filteredVehicles = allVehicles.AsQueryable();

            if (filterBodyType != null && filterBodyType.Length > 0)
            {
                filteredVehicles = filteredVehicles.Where(v => filterBodyType.Contains(v.BodyType.Body));
            }

            if (filterTransmission != null && filterTransmission.Length > 0)
            {
                filteredVehicles = filteredVehicles.Where(v => filterTransmission.Contains(v.Transmission.TransmissionType));
            }

            return View("FilteredVehicles", filteredVehicles.ToList());
        }

    }
}
