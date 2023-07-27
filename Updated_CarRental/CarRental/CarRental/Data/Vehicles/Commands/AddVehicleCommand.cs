using CarRental.Models.AppDBContext;
using MediatR;

namespace CarRental.Data.Vehicles.Commands
{
    public record AddVehicleCommand(Vehicle Vehicle) : IRequest<Vehicle>;
    public class AddVehicleHandler : IRequestHandler<AddVehicleCommand, Vehicle>
    {
        private readonly AppDBContext _context;
        public AddVehicleHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle newVehicle = request.Vehicle;
            await _context.Vehicle.AddAsync(newVehicle);
            await _context.SaveChangesAsync();
            return newVehicle;
        }
    }
}