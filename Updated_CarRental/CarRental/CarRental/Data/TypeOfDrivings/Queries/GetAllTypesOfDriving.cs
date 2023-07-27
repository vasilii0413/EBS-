using CarRental.Models;
using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.TypeOfDrivings.Queries
{
    public record GetAllTypesOfDrivingQuery : IRequest<List<TypeOfDriving>>;
    public class GetAllTypesOfDrivingHandler : IRequestHandler<GetAllTypesOfDrivingQuery, List<TypeOfDriving>>
    {
        private readonly AppDBContext _context;
        public GetAllTypesOfDrivingHandler(AppDBContext context) => _context = context;

        public async Task<List< TypeOfDriving>> Handle(GetAllTypesOfDrivingQuery request, CancellationToken cancellationToken)
        {
            List<TypeOfDriving> typesOfDriving = await _context.TypeOfDriving.ToListAsync();
            return typesOfDriving;
        }
    }
}
