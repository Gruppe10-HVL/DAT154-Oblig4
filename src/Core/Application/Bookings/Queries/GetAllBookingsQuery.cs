
using DAT154Oblig4.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Bookings.Queries
{
    public class GetAllBookingsQuery : IRequest<IEnumerable<int>> { }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllBookingsQuery, IEnumerable<int>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllUsersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _context.Bookings.Select(x => x.Id).ToListAsync(cancellationToken);

            return bookings;
        }
    }
}
