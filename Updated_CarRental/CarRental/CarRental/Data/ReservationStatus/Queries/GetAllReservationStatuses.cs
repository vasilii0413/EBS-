using CarRental.Models;
using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.ReservationStatus.Queries
{
    public record GetAllReservationStatusesQuery : IRequest<List<ReservationStatus>>;
    public class GetAllReservationStatusesHandler : IRequestHandler<GetAllReservationStatusesQuery, List<ReservationStatus>>
    {
        private readonly AppDBContext _context;
        public GetAllReservationStatusesHandler(AppDBContext context) => _context = context;

        public async Task<List<ReservationStatus>> Handle(GetAllReservationStatusesQuery request, CancellationToken cancellationToken)
        {
            List<ReservationStatus> reservationStatuses = await _context.ReservationStatus.ToListAsync();
            return reservationStatuses;
        }
    }
}
