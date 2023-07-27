using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.TypeOfDrivings.Commands
{
    public record UpdateDriveTypeCommand(TypeOfDriving TypeOfDriving) : IRequest<TypeOfDriving>;
    public class UpdateDriveTypeHandler : IRequestHandler<UpdateDriveTypeCommand, TypeOfDriving>
    {
        private readonly AppDBContext _context;
        public UpdateDriveTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<TypeOfDriving> Handle(UpdateDriveTypeCommand request, CancellationToken cancellationToken)
        {
            TypeOfDriving updatedTypeOfDriving = request.TypeOfDriving;
            _context.TypeOfDriving.Update(updatedTypeOfDriving);
            await _context.SaveChangesAsync();
            return updatedTypeOfDriving;
        }
    }
}