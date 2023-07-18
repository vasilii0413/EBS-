using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using CarRental.Models.AppDBContext;
using CarRental.ViewModels;

namespace CarRental.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly AppDBContext _context;

        public VehiclesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var appDBContext = _context.Vehicle
                .Include(v => v.BodyType)
                .Include(v => v.Fuel)
                .Include(v => v.Transmission)
                .Include(v => v.TypeOfDriving)
                .Include(v => v.VehicleType);
            return View(await appDBContext.ToListAsync());
        }

        

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.BodyType)
                .Include(v => v.Fuel)
                .Include(v => v.Transmission)
                .Include(v => v.TypeOfDriving)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["VehicleTypes"] = new SelectList(_context.VehicleType, "VehicleTypeId", "VehicleTypeName");
            ViewData["BodyTypes"] = new SelectList(_context.BodyType, "BodyTypeId", "Body");
            ViewData["Transmissions"] = new SelectList(_context.Transmission, "TransmissionId", "TransmissionType");
            ViewData["Fuels"] = new SelectList(_context.Fuel, "FuelId", "FuelType");
            ViewData["DriveTypes"] = new SelectList(_context.TypeOfDriving, "DriveTypeId", "TypeDrive");

            return View();
        }

        public IActionResult ManageCars()
        {
            var vehicles = _context.Vehicle
                .Include(v => v.VehicleType)
                .Include(v => v.BodyType)
                .Include(v => v.Transmission)
                .Include(v => v.Fuel)
                .Include(v => v.TypeOfDriving)
                .ToList();

            return View(vehicles);
        }
        // POST: Vehicles/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,Name,VehicleTypeId,BodyTypeId,TransmissionId,Seats,FuelId,DriveTypeId,Year,DailyPrice,Image")] Vehicle vehicle)
        {
            try
            {
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(short? id)
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

        // POST: Vehicles/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("VehicleId,Name,VehicleTypeId,BodyTypeId,TransmissionId,Seats,FuelId,DriveTypeId,Year,DailyPrice,Image")] Vehicle vehicle)
        {

            try
            {
                _context.Update(vehicle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(ManageCars));
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Vehicle == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.BodyType)
                .Include(v => v.Fuel)
                .Include(v => v.Transmission)
                .Include(v => v.TypeOfDriving)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }
        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Vehicle == null)
            {
                return Problem("Entity set 'AppDBContext.Vehicle'  is null.");
            }
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(short id)
        {
            return (_context.Vehicle?.Any(e => e.VehicleId == id)).GetValueOrDefault();
        }

        public IActionResult FilteredVehicles(string[] filterBodyType, string[] filterTransmission)
        {
            var query = _context.Vehicle.AsQueryable();

            
            if (filterBodyType != null && filterBodyType.Length > 0)
            {
                query = query.Where(v => filterBodyType.Contains(v.BodyType.Body));
            }

            if (filterTransmission != null && filterTransmission.Length > 0)
            {
                query = query.Where(v => filterTransmission.Contains(v.Transmission.TransmissionType));
            }
            
            var filteredVehicles = query
                .Include(v => v.VehicleType)
                .Include(v => v.BodyType)
                .Include(v => v.Transmission)
                .Include(v => v.Fuel)
                .Include(v => v.TypeOfDriving)
                .ToList();
            return View("FilteredVehicles", filteredVehicles);
        }
    }
}
