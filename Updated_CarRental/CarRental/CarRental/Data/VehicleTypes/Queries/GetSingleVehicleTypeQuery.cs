using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.VehicleTypes.Queries
{
    public record GetSingleVehicleTypeQuery(byte VehicleTypeId) : IRequest<VehicleType>;

    public class GetSingleVehicleTypeHandler : IRequestHandler<GetSingleVehicleTypeQuery, VehicleType>
    {
        private readonly AppDBContext _context;
        public GetSingleVehicleTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<VehicleType> Handle(GetSingleVehicleTypeQuery request, CancellationToken cancellationToken)
        {
            VehicleType vehicleType = await _context.VehicleType.FindAsync(request.VehicleTypeId);
            return vehicleType;
        }
    }
}
