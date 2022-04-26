
using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Domain.Entities;
using DAT154Oblig4.Domain.Enums;
using MapsterMapper;
using MediatR;

namespace DAT154Oblig4.Application.Rooms.Commands
{
    public class CreateNewRoomCommand : IRequest<RoomDto>
    {
        public int BedCount { get; set; }
        public int Size { get; set; }
        public RoomQuality Quality { get; set; }

        public CreateNewRoomCommand(int bedCount, int size, RoomQuality quality)
        {
            BedCount = bedCount;
            Size = size;
            Quality = quality;
        }
    }

    public class CreateNewRoomCommandHandler : IRequestHandler<CreateNewRoomCommand, RoomDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateNewRoomCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomDto> Handle(CreateNewRoomCommand request, CancellationToken cancellationToken)
        {
            var room = new Room(request.BedCount, request.Size, request.Quality);
             await _context.Rooms.AddAsync(room, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<RoomDto>(room);
        }
    }
}
