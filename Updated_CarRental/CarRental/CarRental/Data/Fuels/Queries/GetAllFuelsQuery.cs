using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Fuel.Queries
{
    public record GetAllFuelsQuery : IRequest<List<Fuel>>;
    public class GetAllFuelsHandler : IRequestHandler<GetAllFuelsQuery, List<Fuel>>
    {
        private readonly AppDBContext _context;
        public GetAllFuelsHandler(AppDBContext context) => _context = context;

        public async Task<List<Fuel>> Handle(GetAllFuelsQuery request, CancellationToken cancellationToken)
        {
            List<Fuel> fuels = await _context.Fuel.ToListAsync();
            return fuels;
        }
    }
}
