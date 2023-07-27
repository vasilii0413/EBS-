using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Transmissions.Commands
{
    public record AddTransmissionCommand(Transmission Transmission) : IRequest<Transmission>;
    public class AddTransmissionHandler : IRequestHandler<AddTransmissionCommand, Transmission>
    {
        private readonly AppDBContext _context;
        public AddTransmissionHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Transmission> Handle(AddTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission newTransmission = request.Transmission;
            await _context.Transmission.AddAsync(newTransmission);
            await _context.SaveChangesAsync();
            return newTransmission;
        }
    }
}