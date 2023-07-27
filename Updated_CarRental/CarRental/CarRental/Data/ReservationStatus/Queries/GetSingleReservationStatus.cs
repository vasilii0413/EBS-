using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.ReservationStatus.Queries
{
    public record GetSingleReservationStatusQuery(byte ReservationId) : IRequest<ReservationStatus>;

    public class GetSingleReservationStatusHandler : IRequestHandler<GetSingleReservationStatusQuery, ReservationStatus>
    {
        private readonly AppDBContext _context;
        public GetSingleReservationStatusHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<ReservationStatus> Handle(GetSingleReservationStatusQuery request, CancellationToken cancellationToken)
        {
            ReservationStatus reservationStatus = await _context.ReservationStatus.FindAsync(request.ReservationId);
            return reservationStatus;
        }
    }
}
