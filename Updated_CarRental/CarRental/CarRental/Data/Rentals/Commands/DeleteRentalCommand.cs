using CarRental.Models.AppDBContext;
using MediatR;

namespace CarRental.Data.Rentals.Commands
{
    public record DeleteRentalCommand(Rental Rental) : IRequest<Rental>;
    public class DeleteRentalHandler : IRequestHandler<DeleteRentalCommand, Rental>
    {
        private readonly AppDBContext _context;
        public DeleteRentalHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Rental> Handle(DeleteRentalCommand request, CancellationToken cancellationToken)
        {
            Rental deletedRental = request.Rental;
            _context.Rental.Remove(deletedRental);
            await _context.SaveChangesAsync();
            return deletedRental;
        }
    }
}