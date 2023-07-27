using CarRental.Models;
using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Rentals.Queries
{
    public record GetAllRentalsQuery : IRequest<List<Rental>>;
    public class GetAllRentalsHandler : IRequestHandler<GetAllRentalsQuery, List<Rental>>
    {
        private readonly AppDBContext _context;
        public GetAllRentalsHandler(AppDBContext context) => _context = context;

        public async Task<List<Rental>> Handle(GetAllRentalsQuery request, CancellationToken cancellationToken)
        {
            List<Rental> rentals = await _context.Rental
            .Include(r => r.Vehicle)
            .Include(r => r.ReservationStatus)
            .Include(r => r.User)
            .ToListAsync();
            return rentals;
        }
    }
}
