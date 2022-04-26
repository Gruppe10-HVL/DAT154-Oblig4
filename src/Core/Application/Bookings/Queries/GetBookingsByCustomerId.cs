using DAT154Oblig4.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Bookings.Queries
{ 
    public class GetBookingsByCustomerId : IRequest<IEnumerable<int>> 
    { 
        public int CustomerId { get; set; }
    }

    public class GetBookingsByCustomerIdHandler : IRequestHandler<GetBookingsByCustomerId, IEnumerable<int>>
    {
        private readonly IApplicationDbContext _context;

        public GetBookingsByCustomerIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> Handle(GetBookingsByCustomerId request, CancellationToken cancellationToken)
        {
            var bookings = await _context.Bookings.Where(x => x.Customer.Id == request.CustomerId).Select(x => x.Id).ToListAsync(cancellationToken);

            return bookings;
        }
    }
}
