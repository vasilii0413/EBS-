using CarRental.Models.AppDBContext;
using MediatR;

namespace CarRental.Data.Vehicles.Commands
{
    public record UpdateVehicleCommand(Vehicle Vehicle) : IRequest<Vehicle>;
    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleCommand, Vehicle>
    {
        private readonly AppDBContext _context;
        public UpdateVehicleHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle updatedVehicle = request.Vehicle;
            _context.Vehicle.Update(updatedVehicle);
            await _context.SaveChangesAsync();
            return updatedVehicle;
        }
    }
}