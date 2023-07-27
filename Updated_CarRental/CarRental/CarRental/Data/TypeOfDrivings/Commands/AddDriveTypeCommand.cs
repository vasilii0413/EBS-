using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.TypeOfDrivings.Commands
{
    public record AddDriveTypeCommand(TypeOfDriving TypeOfDriving) : IRequest<TypeOfDriving>;
    public class AddDriveTypeHandler : IRequestHandler<AddDriveTypeCommand, TypeOfDriving>
    {
        private readonly AppDBContext _context;
        public AddDriveTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<TypeOfDriving> Handle(AddDriveTypeCommand request, CancellationToken cancellationToken)
        {
            TypeOfDriving newTypeOfDriving = request.TypeOfDriving;
            await _context.TypeOfDriving.AddAsync(newTypeOfDriving);
            await _context.SaveChangesAsync();
            return newTypeOfDriving;
        }
    }
}