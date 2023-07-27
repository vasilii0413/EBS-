using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.ReservationStatus.Commands
{
    public record UpdateReservationStatusCommand(ReservationStatus ReservationStatus) : IRequest<ReservationStatus>;
    public class UpdateReservationStatusHandler : IRequestHandler<UpdateReservationStatusCommand, ReservationStatus>
    {
        private readonly AppDBContext _context;
        public UpdateReservationStatusHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<ReservationStatus> Handle(UpdateReservationStatusCommand request, CancellationToken cancellationToken)
        {
            ReservationStatus updatedReservationStatus = request.ReservationStatus;
            _context.ReservationStatus.Update(updatedReservationStatus);
            await _context.SaveChangesAsync();
            return updatedReservationStatus;
        }
    }
}