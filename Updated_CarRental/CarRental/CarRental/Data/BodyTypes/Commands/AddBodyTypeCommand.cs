using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.BodyType.Commands
{
    public record AddBodyTypeCommand(BodyType BodyType) : IRequest<BodyType>;
    public class AddBodyTypeHandler : IRequestHandler<AddBodyTypeCommand, BodyType>
    {
        private readonly AppDBContext _context;
        public AddBodyTypeHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<BodyType> Handle(AddBodyTypeCommand request, CancellationToken cancellationToken)
        {
            BodyType newBodyType = request.BodyType;
            await _context.BodyType.AddAsync(newBodyType);
            await _context.SaveChangesAsync();
            return newBodyType;
        }
    }
}