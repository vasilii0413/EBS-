using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRental.Models;
using CarRental.Models.AppDBContext;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using CarRental.Data.Rentals.Queries;
using CarRental.Data.Rentals.Commands;
using CarRental.Data.Vehicles;

namespace CarRental.Data.Rentals
{
    public class RentalsController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;

        public RentalsController(AppDBContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }


        public async Task<IActionResult> IndexRental()
        {
            List<Rental> rentals = await _mediator.Send(new GetAllRentalsQuery());
            return View(rentals);
        }


        public async Task<IActionResult> DetailsRental(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rental = await _mediator.Send(new GetSingleRentalQuery(id.Value));
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }


        [Authorize]
        public IActionResult CreateRental(int vehicleId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["Email"] = new SelectList(_context.Users, "Email", "Email");
            ViewData["ReservationId"] = new SelectList(_context.Set<ReservationStatus.ReservationStatus>(), "ReservationId", "ReservationId");
            ViewData["VehicleId"] = new SelectList(_context.Set<Vehicle>(), "VehicleId", "VehicleId");
            ViewData["VehicleId"] = vehicleId;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRental([Bind("RentalId,RentalDate,ReturnDate,VehicleId,ReservationId,UserId")] Rental rental)
        {
            try
            {
                rental.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                rental.VehicleId = Convert.ToInt16(Request.Form["VehicleId"]);

                await _mediator.Send(new AddRentalCommand(rental));
                return RedirectToAction("IndexVehicle", "Vehicles");
            }
            catch
            {
                return NotFound();
            }
        }


        public async Task<IActionResult> EditRental(short? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            ViewData["Reservations"] = new SelectList(_context.ReservationStatus, "ReservationId", "Status");
            ViewData["Vehicles"] = new SelectList(_context.Vehicle, "VehicleId", "Name");
            ViewData["Users"] = new SelectList(_context.Vehicle, "Id", "Email");


            return View(rental);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRental([Bind("RentalId,UserId,RentalDate,ReturnDate,VehicleId,ReservationId")] Rental rental)
        {
            try
            {
                await _mediator.Send(new UpdateRentalCommand(rental));
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction("IndexRental", "Rentals");
        }


        public async Task<IActionResult> DeleteRental(short? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            Rental rental = await _mediator.Send(new GetSingleRentalQuery(id.Value));

            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }


        [HttpPost, ActionName("DeleteRental")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            try
            {
                Rental rental = await _mediator.Send(new GetSingleRentalQuery(id));
                if (rental == null)
                {
                    return NotFound();
                }
                await _mediator.Send(new DeleteRentalCommand(rental));
                return RedirectToAction(nameof(IndexRental));
            }
            catch
            {
                return NotFound();
            }
        }

        private bool RentalExists(short id)
        {
            return (_context.Rental?.Any(e => e.RentalId == id)).GetValueOrDefault();
        }
    }
}
