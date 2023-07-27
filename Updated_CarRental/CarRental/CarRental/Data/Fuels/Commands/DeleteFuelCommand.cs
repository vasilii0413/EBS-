using CarRental.Models.AppDBContext;
using MediatR;

namespace CarRental.Data.Fuel.Commands
{
    public record DeleteFuelCommand(Fuel Fuel) : IRequest<Fuel>;
    public class DeleteFuelHandler : IRequestHandler<DeleteFuelCommand, Fuel>
    {
        private readonly AppDBContext _context;
        public DeleteFuelHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Fuel> Handle(DeleteFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel deletedFuel = request.Fuel;
            _context.Fuel.Remove(deletedFuel);
            await _context.SaveChangesAsync();
            return deletedFuel;
        }
    }
}