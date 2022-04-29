using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace DAT154Oblig4.Application.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<BookingDto>
    {

        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CreateBookingCommand(int customerId, int roomId, DateTime startDate, DateTime endDate)
        {
            CustomerId = customerId;
            RoomId = roomId;
            StartDate = startDate.Date;
            EndDate = endDate.Date;
        }

    }

    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookingCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            //Check if there are any active bookings for period
            var existingBookings = _context.Bookings.Any(x => x.RoomId == request.RoomId && !(
                (x.BookingStart <= request.StartDate && x.BookingEnd >= request.StartDate) ||
                (x.BookingStart <= request.EndDate && x.BookingEnd >= request.EndDate) ||
                (request.StartDate <= x.BookingStart && request.EndDate >= x.BookingStart) ||
                (request.StartDate <= x.BookingEnd && request.EndDate >= x.BookingEnd)));

            //Return null if there are existing bookings in requested range.
            //Could be implemented more cleanly if all Handle-methods were wrapped by a ServiceResult class.
            if(existingBookings) return null;

            var booking = new Booking(request.CustomerId, request.RoomId, request.StartDate, request.EndDate);
            _context.Bookings.Attach(booking);
            await _context.SaveChangesAsync(cancellationToken);
            
            //Explicitly load customer navigational property
            await _context.Entry(booking).Reference("Customer").LoadAsync();
            return _mapper.Map<BookingDto>(booking);
        }
    }
}