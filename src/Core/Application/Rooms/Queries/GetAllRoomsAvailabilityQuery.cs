using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Domain.Enums.Booking;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Rooms.Queries
{
    public class GetAllRoomsAvailabilityQuery : IRequest<IEnumerable<RoomDto>> 
    { 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public GetAllRoomsAvailabilityQuery(DateTime from, DateTime to)
        {
            StartDate = from;
            EndDate = to;
        }
    }

    public class GetAllRoomsAvailabilityQueryHandler : IRequestHandler<GetAllRoomsAvailabilityQuery, IEnumerable<RoomDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllRoomsAvailabilityQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDto>> Handle(GetAllRoomsAvailabilityQuery request, CancellationToken cancellationToken)
        {
            List<RoomDto> availableRoomsId = await _context.Rooms.Where(room => 
            !room.Bookings.Where(x => x.Status != BookingStatus.Cancelled).Any(x => 
                (x.BookingStart <= request.StartDate && x.BookingEnd >= request.StartDate) ||
                (x.BookingStart <= request.EndDate && x.BookingEnd >= request.EndDate)     ||
                (request.StartDate <= x.BookingStart && request.EndDate >= x.BookingStart) ||
                (request.StartDate <= x.BookingEnd && request.EndDate >= x.BookingEnd)))
            .ProjectToType<RoomDto>(_mapper.Config)
            .ToListAsync();
                

            return availableRoomsId;
        }
    }
}