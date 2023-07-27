using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Rentals.Queries
{
    public record GetSingleRentalQuery(short RentalId) : IRequest<Rental>;

    public class GetSingleRentalHandler : IRequestHandler<GetSingleRentalQuery, Rental>
    {
        private readonly AppDBContext _context;
        public GetSingleRentalHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Rental> Handle(GetSingleRentalQuery request, CancellationToken cancellationToken)
        {
            Rental rental = await _context.Rental
                .Include(r => r.ReservationStatus)
                .Include(r => r.Vehicle)
                .FirstOrDefaultAsync(r => r.RentalId == request.RentalId);

            return rental;
        }
    }
}
