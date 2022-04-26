using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Bookings.Queries
{
    public class GetBookingByIdQuery : IRequest<BookingDto>
    {
        public int Id { get; set; }

    }

    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBookingByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BookingDto> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<BookingDto>(booking);
        }
    }
}
