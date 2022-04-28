using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Bookings.Queries
{ 
    public class GetBookingsByCustomerId : IRequest<IEnumerable<BookingDto>> 
    { 
        public int CustomerId { get; set; }
    }

    public class GetBookingsByCustomerIdHandler : IRequestHandler<GetBookingsByCustomerId, IEnumerable<BookingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetBookingsByCustomerIdHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookingDto>> Handle(GetBookingsByCustomerId request, CancellationToken cancellationToken)
        {
            var bookings = await _context.Bookings.Where(x => x.Customer.Id == request.CustomerId).ProjectToType<BookingDto>(_mapper.Config).ToListAsync(cancellationToken);

            return bookings;
        }
    }
}
