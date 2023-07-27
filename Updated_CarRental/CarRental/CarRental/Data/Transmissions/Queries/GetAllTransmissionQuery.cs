using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Transmissions.Queries
{
    public record GetAllTransmissionsQuery : IRequest<List<Transmission>>;
    public class GetAllTransmissionsHandler : IRequestHandler<GetAllTransmissionsQuery, List<Transmission>>
    {
        private readonly AppDBContext _context;
        public GetAllTransmissionsHandler(AppDBContext context) => _context = context;

        public async Task<List<Transmission>> Handle(GetAllTransmissionsQuery request, CancellationToken cancellationToken)
        {
            List<Transmission> transmissions = await _context.Transmission.ToListAsync();
            return transmissions;
        }
    }
}
