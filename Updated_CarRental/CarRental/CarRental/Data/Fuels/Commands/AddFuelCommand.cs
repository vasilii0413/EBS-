using CarRental.Models.AppDBContext;
using MediatR;
namespace CarRental.Data.Fuel.Commands
{
    public record AddFuelCommand(Fuel Fuel) : IRequest<Fuel>;
    public class AddFuelHandler : IRequestHandler<AddFuelCommand, Fuel>
    {
        private readonly AppDBContext _context;
        public AddFuelHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Fuel> Handle(AddFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel newFuel = request.Fuel;
            await _context.Fuel.AddAsync(newFuel);
            await _context.SaveChangesAsync();
            return newFuel;
        }
    }
}