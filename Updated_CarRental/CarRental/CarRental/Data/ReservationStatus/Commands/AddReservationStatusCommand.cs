using CarRental.Models.AppDBContext;
using MediatR;

namespace CarRental.Data.ReservationStatus.Commands
{
    public record AddReservationStatusCommand(ReservationStatus ReservationStatus) : IRequest<ReservationStatus>;
    public class AddReservationStatusHandler : IRequestHandler<AddReservationStatusCommand, ReservationStatus>
    {
        private readonly AppDBContext _context;
        public AddReservationStatusHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<ReservationStatus> Handle(AddReservationStatusCommand request, CancellationToken cancellationToken)
        {
            ReservationStatus newReservationStatus = request.ReservationStatus;
            await _context.ReservationStatus.AddAsync(newReservationStatus);
            await _context.SaveChangesAsync();
            return newReservationStatus;
        }
    }
}