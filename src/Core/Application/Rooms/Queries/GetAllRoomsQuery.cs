using DAT154Oblig4.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Rooms.Queries
{
    public class GetAllRoomsQuery : IRequest<IEnumerable<int>> { }

    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, IEnumerable<int>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllRoomsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _context.Rooms.Select(x => x.Id).ToListAsync(cancellationToken);
            return rooms;
        }
    }
}
