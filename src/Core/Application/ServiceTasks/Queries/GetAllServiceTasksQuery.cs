
using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.ServiceTasks.Queries
{
    public class GetAllServiceTasksQuery : IRequest<IEnumerable<ServiceTaskDto>> { }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllServiceTasksQuery, IEnumerable<ServiceTaskDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceTaskDto>> Handle(GetAllServiceTasksQuery request, CancellationToken cancellationToken)
        {
            var servicetasks = await _context.ServiceTasks.ProjectToType<ServiceTaskDto>(_mapper.Config).ToListAsync(cancellationToken);

            return servicetasks;
        }
    }
}
