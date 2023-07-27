using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Vehicles.Queries
{
    public record GetSingleVehicleQuery(short VehicleId) : IRequest<Vehicle>;

    public class GetSingleVehicleHandler : IRequestHandler<GetSingleVehicleQuery, Vehicle>
    {
        private readonly AppDBContext _context;
        public GetSingleVehicleHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> Handle(GetSingleVehicleQuery request, CancellationToken cancellationToken)
        {
            Vehicle vehicle = await _context.Vehicle
                .Include(v => v.BodyType)
                .Include(v => v.Fuel)
                .Include(v => v.Transmission)
                .Include(v => v.TypeOfDriving)
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.VehicleId == request.VehicleId);

            return vehicle;
        }
    }
}
