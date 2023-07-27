using CarRental.Models.AppDBContext;
using CarRental.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.TypeOfDrivings.Commands
{
    public record DeleteDriveTypeCommand(TypeOfDriving TypeOfDriving) : IRequest<TypeOfDriving>;
    public class DeleteDriveTypeHandler : IRequestHandler<DeleteDriveTypeCommand, TypeOfDriving>
    {
        private readonly AppDBContext _context;
        public DeleteDriveTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<TypeOfDriving> Handle(DeleteDriveTypeCommand request, CancellationToken cancellationToken)
        {
            TypeOfDriving deletedTypeOfDriving = request.TypeOfDriving;
            _context.TypeOfDriving.Remove(deletedTypeOfDriving);
            await _context.SaveChangesAsync();
            return deletedTypeOfDriving;
        }
    }
}