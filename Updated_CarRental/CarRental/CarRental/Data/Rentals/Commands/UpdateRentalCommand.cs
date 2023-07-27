using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Rentals.Commands
{
    public record UpdateRentalCommand(Rental Rental) : IRequest<Rental>;
    public class UpdateRentalHandler : IRequestHandler<UpdateRentalCommand, Rental>
    {
        private readonly AppDBContext _context;
        public UpdateRentalHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Rental> Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
        {
            Rental updatedRental = request.Rental;
            _context.Rental.Update(updatedRental);
            await _context.SaveChangesAsync();
            return updatedRental;
        }
    }
}