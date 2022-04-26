
using DAT154Oblig4.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<int>> { }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<int>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllUsersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<int>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers.Select(x => x.Id).ToListAsync(cancellationToken);

            return customers;
        }
    }
}
