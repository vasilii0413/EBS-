using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.VehicleTypes.Commands
{
    public record UpdateVehicleTypeCommand(VehicleType VehicleType) : IRequest<VehicleType>;
    public class UpdateVehicleTypeHandler : IRequestHandler<UpdateVehicleTypeCommand, VehicleType>
    {
        private readonly AppDBContext _context;
        public UpdateVehicleTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<VehicleType> Handle(UpdateVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            VehicleType updatedVehicleType = request.VehicleType;
            _context.VehicleType.Update(updatedVehicleType);
            await _context.SaveChangesAsync();
            return updatedVehicleType;
        }
    }
}