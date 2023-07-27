using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.VehicleTypes.Commands
{
    public record AddVehicleTypeCommand(VehicleType VehicleType) : IRequest<VehicleType>;
    public class AddVehicleTypeHandler : IRequestHandler<AddVehicleTypeCommand, VehicleType>
    {
        private readonly AppDBContext _context;
        public AddVehicleTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<VehicleType> Handle(AddVehicleTypeCommand request, CancellationToken cancellationToken)
        {
            VehicleType newVehicleType = request.VehicleType;
            await _context.VehicleType.AddAsync(newVehicleType);
            await _context.SaveChangesAsync();
            return newVehicleType;
        }
    }
}