
using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Domain.Entities;
using DAT154Oblig4.Domain.Enums;
using MapsterMapper;
using MediatR;

namespace DAT154Oblig4.Application.ServiceTasks.Commands
{
    public class CreateServiceTaskCommand : IRequest<ServiceTaskDto>
    {
        public virtual int RoomId { get; set; }
        public string Description { get; set; }
        public ServiceTaskType TaskType { get; set; }
        public ServiceTaskStatus TaskStatus { get; set; }
        public ServiceTaskPriority Priority { get; set; }
        public string Notes { get; set; }

        public CreateServiceTaskCommand() { }

        public CreateServiceTaskCommand(int roomId, string description, ServiceTaskType taskType, ServiceTaskStatus taskStatus, ServiceTaskPriority priority, string notes)
        {
            RoomId = roomId;
            Description = description;
            TaskType = taskType;
            TaskStatus = taskStatus;
            Priority = priority;
            Notes = notes;
        }
    }

    public class CreateServiceTaskCommandHandler : IRequestHandler<CreateServiceTaskCommand, ServiceTaskDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateServiceTaskCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceTaskDto> Handle(CreateServiceTaskCommand request, CancellationToken cancellationToken)
        {
            var serviceTask = new ServiceTask(request.RoomId, request.TaskType, request.Description, request.TaskStatus, request.Priority, request.Notes);

            await _context.ServiceTasks.AddAsync(serviceTask);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ServiceTaskDto>(serviceTask);
        }
    }
}
