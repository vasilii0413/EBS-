using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.ReservationStatus.Commands
{
    public record DeleteReservationStatusCommand(ReservationStatus ReservationStatus) : IRequest<ReservationStatus>;
    public class DeleteReservationStatusHandler : IRequestHandler<DeleteReservationStatusCommand, ReservationStatus>
    {
        private readonly AppDBContext _context;
        public DeleteReservationStatusHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<ReservationStatus> Handle(DeleteReservationStatusCommand request, CancellationToken cancellationToken)
        {
            ReservationStatus deletedReservationStatus = request.ReservationStatus;
            _context.ReservationStatus.Remove(deletedReservationStatus);
            await _context.SaveChangesAsync();
            return deletedReservationStatus;
        }
    }
}