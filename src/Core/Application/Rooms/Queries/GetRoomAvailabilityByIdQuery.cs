using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Domain.Enums.Booking;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Rooms.Queries
{
    public class GetRoomAvailabilityByIdQuery : IRequest<RoomDto>
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetRoomAvailabilityByIdQuery(int id, DateTime startDate, DateTime endDate)
        {
            Id = id;
            StartDate = startDate.Date;
            EndDate = endDate.Date;
        }
    }

    public class GetAllRoomByIdByPeriodQueryHandler : IRequestHandler<GetRoomAvailabilityByIdQuery, RoomDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllRoomByIdByPeriodQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomDto> Handle(GetRoomAvailabilityByIdQuery request, CancellationToken cancellationToken)
        {
            var availableRoom = await _context.Rooms.Where(room =>
            !room.Bookings.Where(x => x.RoomId == request.Id && x.Status != BookingStatus.Cancelled).Any(x =>
                (x.BookingStart <= request.StartDate && x.BookingEnd >= request.StartDate) ||
                (x.BookingStart <= request.EndDate && x.BookingEnd >= request.EndDate) ||
                (request.StartDate <= x.BookingStart && request.EndDate >= x.BookingStart) ||
                (request.StartDate <= x.BookingEnd && request.EndDate >= x.BookingEnd)))
               .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<RoomDto>(availableRoom);
        }
    }

}
