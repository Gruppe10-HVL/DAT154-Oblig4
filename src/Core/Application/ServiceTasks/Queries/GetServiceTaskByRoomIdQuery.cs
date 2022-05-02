using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.ServiceTasks.Queries
{
    public class GetServiceTaskByRoomIdQuery : IRequest<ServiceTaskDto>
    {
        public int Id { get; set; }
    }

    public class GetServiceTaskByRoomIdQueryHandler : IRequestHandler<GetServiceTaskByRoomIdQuery, ServiceTaskDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetServiceTaskByRoomIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceTaskDto> Handle(GetServiceTaskByRoomIdQuery request, CancellationToken cancellationToken)
        {
            var servicetasks = await _context.ServiceTasks.Where(x => x.RoomId == request.Id).FirstOrDefaultAsync();

            return _mapper.Map<ServiceTaskDto>(servicetasks);
        }
    }
}
