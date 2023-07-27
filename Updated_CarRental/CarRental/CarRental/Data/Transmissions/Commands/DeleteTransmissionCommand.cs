using CarRental.Models.AppDBContext;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Data.Transmissions.Commands
{
    public record DeleteTransmissionCommand(Transmission Transmission) : IRequest<Transmission>;
    public class DeleteTransmissionHandler : IRequestHandler<DeleteTransmissionCommand, Transmission>
    {
        private readonly AppDBContext _context;
        public DeleteTransmissionHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<Transmission> Handle(DeleteTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission deletedTransmission = request.Transmission;
            _context.Transmission.Remove(deletedTransmission);
            await _context.SaveChangesAsync();
            return deletedTransmission;
        }
    }
}