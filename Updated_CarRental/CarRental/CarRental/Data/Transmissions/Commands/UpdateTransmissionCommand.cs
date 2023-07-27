using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CarRental.Data.Transmissions.Commands
{
    public record UpdateTransmissionCommand(Transmission Transmission) : IRequest<Transmission>;
    public class UpdateTransmissionHandler : IRequestHandler<UpdateTransmissionCommand, Transmission>
    {
        private readonly AppDBContext _context;
        public UpdateTransmissionHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Transmission> Handle(UpdateTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission updatedTransmission = request.Transmission;
            _context.Transmission.Update(updatedTransmission);
            await _context.SaveChangesAsync();
            return updatedTransmission;
        }
    }
}