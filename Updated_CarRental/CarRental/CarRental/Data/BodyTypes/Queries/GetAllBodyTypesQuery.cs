using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.BodyType.Queries
{
    public record GetAllBodyTypesQuery : IRequest<List<BodyType>>;
    public class GetAllBodyTypesHandler : IRequestHandler<GetAllBodyTypesQuery, List<BodyType>>
    {
        private readonly AppDBContext _context;
        public GetAllBodyTypesHandler(AppDBContext context) => _context = context;

        public async Task<List<BodyType>> Handle(GetAllBodyTypesQuery request, CancellationToken cancellationToken)
        {
            List<BodyType> bodyTypes = await _context.BodyType.ToListAsync();
            return bodyTypes;
        }
    }
}
