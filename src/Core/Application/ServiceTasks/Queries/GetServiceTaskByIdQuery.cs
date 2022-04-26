
using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.ServiceTasks.Queries
{
    public class GetServiceTaskByIdQuery : IRequest<ServiceTaskDto> 
    { 
        public int Id { get; set; }
    }

    public class GetServiceTaskByIdQueryHandler : IRequestHandler<GetServiceTaskByIdQuery, ServiceTaskDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetServiceTaskByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceTaskDto> Handle(GetServiceTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var servicetasks = await _context.ServiceTasks.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            return _mapper.Map<ServiceTaskDto>(servicetasks);
        }
    }
}
