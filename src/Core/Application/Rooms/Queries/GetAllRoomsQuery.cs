using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Rooms.Queries
{
    public class GetAllRoomsQuery : IRequest<IEnumerable<RoomDto>> { }

    public class GetAllRoomsQueryHandler : IRequestHandler<GetAllRoomsQuery, IEnumerable<RoomDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;


        public GetAllRoomsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _context.Rooms.ProjectToType<RoomDto>(_mapper.Config).ToListAsync(cancellationToken);
            return rooms;
        }
    }
}
