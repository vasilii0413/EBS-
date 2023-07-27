using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Vehicles.Commands
{
    public record DeleteVehicleCommand(Vehicle Vehicle) : IRequest<Vehicle>;
    public class DeleteVehicleHandler : IRequestHandler<DeleteVehicleCommand, Vehicle>
    {
        private readonly AppDBContext _context;
        public DeleteVehicleHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
        {
            Vehicle deletedVehicle = request.Vehicle;
            _context.Vehicle.Remove(deletedVehicle);
            await _context.SaveChangesAsync();
            return deletedVehicle;
        }
    }
}