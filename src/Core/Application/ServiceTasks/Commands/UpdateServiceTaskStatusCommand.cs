using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Domain.Entities;
using DAT154Oblig4.Domain.Enums;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.ServiceTasks.Commands
{
    
    public class UpdateServiceTaskStatusCommand : IRequest<ServiceTaskDto>
    {
        public int Id { get; set; }
        public ServiceTaskStatus TaskStatus { get; set; }
    }

    public class UpdateServiceTaskStatusCommandHandler : IRequestHandler<UpdateServiceTaskStatusCommand, ServiceTaskDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateServiceTaskStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceTaskDto> Handle(UpdateServiceTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var serviceTask = await _context.ServiceTasks.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (serviceTask == null) return null;

            serviceTask.TaskStatus = request.TaskStatus;

            _context.ServiceTasks.Update(serviceTask);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ServiceTaskDto>(serviceTask);
        }
    }
}
