
using DAT154Oblig4.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.ServiceTasks.Queries
{
    public class GetAllServiceTasksQuery : IRequest<IEnumerable<int>> { }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllServiceTasksQuery, IEnumerable<int>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllUsersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> Handle(GetAllServiceTasksQuery request, CancellationToken cancellationToken)
        {
            var servicetasks = await _context.ServiceTasks.Select(x => x.Id).ToListAsync(cancellationToken);

            return servicetasks;
        }
    }
}
