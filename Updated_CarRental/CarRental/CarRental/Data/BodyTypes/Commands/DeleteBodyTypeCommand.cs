using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.BodyType.Commands
{
    public record DeleteBodyTypeCommand(BodyType BodyType) : IRequest<BodyType>;
    public class DeleteBodyTypeHandler : IRequestHandler<DeleteBodyTypeCommand, BodyType>
    {
        private readonly AppDBContext _context;
        public DeleteBodyTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<BodyType> Handle(DeleteBodyTypeCommand request, CancellationToken cancellationToken)
        {
            BodyType deletedBodyType = request.BodyType;
            _context.BodyType.Remove(deletedBodyType);
            await _context.SaveChangesAsync();
            return deletedBodyType;
        }
    }
}