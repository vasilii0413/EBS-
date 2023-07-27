using CarRental.Models.AppDBContext;
using MediatR;

namespace CarRental.Data.Fuel.Commands
{
    public record UpdateFuelCommand(Fuel Fuel) : IRequest<Fuel>;
    public class UpdateFuelHandler : IRequestHandler<UpdateFuelCommand, Fuel>
    {
        private readonly AppDBContext _context;
        public UpdateFuelHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Fuel> Handle(UpdateFuelCommand request, CancellationToken cancellationToken)
        {
            Fuel updatedFuel = request.Fuel;
            _context.Fuel.Update(updatedFuel);
            await _context.SaveChangesAsync();
            return updatedFuel;
        }
    }
}