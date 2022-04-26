using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Domain.Enums.Booking;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Bookings.Commands
{ 
    public class ChangeBookingStatusCommand : IRequest<BookingDto>
    {
        public int Id { get; set; }
        public BookingStatus Status { get; set; }
    }

    public class ChangeBookingStatusCommandHandler : IRequestHandler<ChangeBookingStatusCommand, BookingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ChangeBookingStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(ChangeBookingStatusCommand request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (booking == null) return null;

            booking.Status = request.Status;

            _context.Bookings.Update(booking);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
