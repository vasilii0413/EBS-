using CarRental.Models;
using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Vehicles.Queries
{
    public record GetAllVehiclesQuery : IRequest<List<Vehicle>>;
    public class GetAllVehiclesHandler : IRequestHandler<GetAllVehiclesQuery, List<Vehicle>>
    {
        private readonly AppDBContext _context;
        public GetAllVehiclesHandler(AppDBContext context) => _context = context;

        public async Task<List<Vehicle>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            List<Vehicle> vehicles = await _context.Vehicle
                .Include(v => v.BodyType)
                .Include(v => v.Fuel)
                .Include(v => v.Transmission)
                .Include(v => v.TypeOfDriving)
                .Include(v => v.VehicleType)
                .ToListAsync();
            return vehicles;

        }
    }
}
