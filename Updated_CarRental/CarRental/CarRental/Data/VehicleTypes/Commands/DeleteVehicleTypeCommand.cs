using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.VehicleTypes.Commands
{
    public record DeleteVehicleTypeCommand(VehicleType VehicleType) : IRequest<VehicleType>;
    public class DeleteVehicleTypeHandler : IRequestHandler<DeleteVehicleTypeCommand, VehicleType>
    {
        private readonly AppDBContext _context;
        public DeleteVehicleTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<VehicleType> Handle(DeleteVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            VehicleType deletedVehicleType = request.VehicleType;
            _context.VehicleType.Remove(deletedVehicleType);
            await _context.SaveChangesAsync();
            return deletedVehicleType;
        }
    }
}