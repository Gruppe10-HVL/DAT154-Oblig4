using DAT154Oblig4.Application.Common.Interfaces;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Bookings.Commands
{
    public class DeleteBookingCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookingCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
