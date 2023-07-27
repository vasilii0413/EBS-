using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;

namespace CarRental.Data.Rentals.Commands
{
    public record AddRentalCommand(Rental Rental) : IRequest<Rental>;
    public class AddRentalHandler : IRequestHandler<AddRentalCommand, Rental>
    {
        private readonly AppDBContext _context;
        public AddRentalHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Rental> Handle(AddRentalCommand request, CancellationToken cancellationToken)
        {
            Rental newRental = request.Rental;
            await _context.Rental.AddAsync(newRental);
            await _context.SaveChangesAsync();
            return newRental;
        }
    }
}