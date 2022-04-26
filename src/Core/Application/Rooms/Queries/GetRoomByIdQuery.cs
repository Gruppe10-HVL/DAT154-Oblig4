using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Rooms.Queries
{
    public class GetRoomByIdQuery : IRequest<RoomDto> 
    { 
        public int Id { get; set; }
    }

    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, RoomDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetRoomByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.Where(x => x.Id == request.Id)                
                .ProjectToType<RoomDto>(_mapper.Config)
                .FirstOrDefaultAsync(cancellationToken);

            return room;
        }
    }
}

