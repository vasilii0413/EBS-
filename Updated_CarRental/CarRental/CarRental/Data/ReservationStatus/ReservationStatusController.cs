using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRental.Models.AppDBContext;
using MediatR;
using LazyCache;
using CarRental.Data.ReservationStatus.Commands;
using CarRental.Data.ReservationStatus.Queries;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CarRental.Data.ReservationStatus
{
    public class ReservationStatusController : Controller
    {
        private readonly AppDBContext _context;
        private readonly IMediator _mediator;
        private readonly IAppCache _cache;

        public ReservationStatusController(AppDBContext context, IMediator mediator, IAppCache cache)
        {
            _context = context;
            _mediator = mediator;
            _cache = cache;
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> IndexReservation()
        {
            List<ReservationStatus> reservations = await _cache.GetOrAddAsync("AllReservationStatuses", async () => await _mediator.Send(new GetAllReservationStatusesQuery()));

            return View(reservations);
        }


        public async Task<IActionResult> DetailsReservation(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ReservationStatus reservation = await _cache.GetOrAddAsync($"ReservationStatus_{id}", async () => await _mediator.Send(new GetSingleReservationStatusQuery(id.Value)));

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }


        public IActionResult CreateReservation()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReservation([Bind("ReservationId,Status")] ReservationStatus reservationStatus)
        {
            try
            {
                await _mediator.Send(new AddReservationStatusCommand(reservationStatus));
                _cache.Remove("AllReservationStatuses");
                return RedirectToAction(nameof(IndexReservation));

            }
            catch
            {
                return NotFound();
            }

        }


        public async Task<IActionResult> EditReservation(byte? id)
        {
            if (id == null || _context.ReservationStatus == null)
            {
                return NotFound();
            }

            var reservationStatus = await _context.ReservationStatus.FindAsync(id);
            if (reservationStatus == null)
            {
                return NotFound();
            }
            return View(reservationStatus);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReservation(byte id, [Bind("ReservationId,Status")] ReservationStatus reservationStatus)
        {
            try
            {
                await _mediator.Send(new UpdateReservationStatusCommand(reservationStatus));
                _cache.Remove("AllReservationStatuses");
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(IndexReservation));

        }


        public async Task<IActionResult> DeleteReservation(byte? id)
        {
            if (id == null || _context.ReservationStatus == null)
            {
                return NotFound();
            }

            ReservationStatus reservation = await _mediator.Send(new GetSingleReservationStatusQuery(id.Value));

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }


        [HttpPost, ActionName("DeleteReservation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            try
            {
                ReservationStatus reservation = await _mediator.Send(new GetSingleReservationStatusQuery(id));
                if (reservation == null)
                {
                    return NotFound();
                }
                await _mediator.Send(new DeleteReservationStatusCommand(reservation));
                _cache.Remove("AllReservationStatuses");
                return RedirectToAction(nameof(IndexReservation));
            }
            catch
            {
                return NotFound();
            }
        }

        private bool ReservationStatusExists(byte id)
        {
            return (_context.ReservationStatus?.Any(e => e.ReservationId == id)).GetValueOrDefault();
        }
    }
}
