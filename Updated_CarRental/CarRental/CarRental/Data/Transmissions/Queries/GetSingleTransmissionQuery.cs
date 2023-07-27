using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Transmissions.Queries
{
    public record GetSingleTransmissionQuery(byte TransmissionsId) : IRequest<Transmission>;

    public class GetSingleTransmissionHandler : IRequestHandler<GetSingleTransmissionQuery, Transmission>
    {
        private readonly AppDBContext _context;
        public GetSingleTransmissionHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Transmission> Handle(GetSingleTransmissionQuery request, CancellationToken cancellationToken)
        {
            Transmission transmissions = await _context.Transmission.FindAsync(request.TransmissionsId);
            return transmissions;
        }
    }
}
