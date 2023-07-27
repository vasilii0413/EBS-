using CarRental.Models.AppDBContext;
using MediatR;

namespace CarRental.Data.TypeOfDrivings.Queries
{
    public record GetSingleTypeOfDrivingQuery(byte TypeOfDrivingId) : IRequest<TypeOfDriving>;

    public class GetSingleTypeOfDrivingHandler : IRequestHandler<GetSingleTypeOfDrivingQuery, TypeOfDriving>
    {
        private readonly AppDBContext _context;
        public GetSingleTypeOfDrivingHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<TypeOfDriving> Handle(GetSingleTypeOfDrivingQuery request, CancellationToken cancellationToken)
        {
            TypeOfDriving typeOfDriving = await _context.TypeOfDriving.FindAsync(request.TypeOfDrivingId);
            return typeOfDriving;
        }
    }
}
