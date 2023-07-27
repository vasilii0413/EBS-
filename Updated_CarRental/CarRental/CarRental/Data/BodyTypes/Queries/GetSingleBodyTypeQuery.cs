using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.BodyType.Queries
{
    public record GetSingleBodyTypeQuery(byte BodyTypeId) : IRequest<BodyType>;

    public class GetSingleBodyTypeHandler : IRequestHandler<GetSingleBodyTypeQuery, BodyType>
    {
        private readonly AppDBContext _context;
        public GetSingleBodyTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<BodyType> Handle(GetSingleBodyTypeQuery request, CancellationToken cancellationToken)
        {
            BodyType bodyType = await _context.BodyType.FindAsync(request.BodyTypeId);
            return bodyType;
        }
    }
}
