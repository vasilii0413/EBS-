using CarRental.Models.AppDBContext;
using MediatR;

namespace CarRental.Data.Fuel.Queries
{
    public record GetSingleFuelQuery(byte FuelId) : IRequest<Fuel>;

    public class GetSingleFuelHandler : IRequestHandler<GetSingleFuelQuery, Fuel>
    {
        private readonly AppDBContext _context;
        public GetSingleFuelHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Fuel> Handle(GetSingleFuelQuery request, CancellationToken cancellationToken)
        {
            Fuel fuel = await _context.Fuel.FindAsync(request.FuelId);
            return fuel;
        }
    }
}
