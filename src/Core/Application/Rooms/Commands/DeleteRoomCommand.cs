
using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Domain.Enums;
using MapsterMapper;
using MediatR;

namespace DAT154Oblig4.Application.Rooms.Commands
{
    public class DeleteRoomCommand : IRequest<RoomDto>
    {
        public int Id { get; set; }

        public DeleteRoomCommand(int id)
        {
            Id = id;
        }
    }

    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, RoomDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DeleteRoomCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RoomDto> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms.FindAsync(new object?[] { request.Id }, cancellationToken);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return _mapper.Map<RoomDto>(room);
        }
    }
}
