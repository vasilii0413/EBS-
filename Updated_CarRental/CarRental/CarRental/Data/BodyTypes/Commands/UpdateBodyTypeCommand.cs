using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.BodyType.Commands
{
    public record UpdateBodyTypeCommand(BodyType BodyType) : IRequest<BodyType>;
    public class UpdateBodyTypeHandler : IRequestHandler<UpdateBodyTypeCommand, BodyType>
    {
        private readonly AppDBContext _context;
        public UpdateBodyTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<BodyType> Handle(UpdateBodyTypeCommand request, CancellationToken cancellationToken)
        {
            BodyType updatedBodyType = request.BodyType;
            _context.BodyType.Update(updatedBodyType);
            await _context.SaveChangesAsync();
            return updatedBodyType;
        }
    }
}