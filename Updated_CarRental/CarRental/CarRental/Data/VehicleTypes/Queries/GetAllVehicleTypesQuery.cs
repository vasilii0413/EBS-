using CarRental.Models;
using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.VehicleTypes.Queries
{
    public record GetAllVehicleTypesQuery : IRequest<List<VehicleType>>;
    public class GetAllVehicleTypesHandler : IRequestHandler<GetAllVehicleTypesQuery, List<VehicleType>>
    {
        private readonly AppDBContext _context;
        public GetAllVehicleTypesHandler(AppDBContext context) => _context = context;

        public async Task<List<VehicleType>> Handle(GetAllVehicleTypesQuery request, CancellationToken cancellationToken)
        {
            List<VehicleType> vehicleTypes = await _context.VehicleType.ToListAsync();
            return vehicleTypes;
        }
    }
}
