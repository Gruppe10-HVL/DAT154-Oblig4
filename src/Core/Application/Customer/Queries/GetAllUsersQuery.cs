
using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.CustomerQueries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<CustomerDto>> { }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<CustomerDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllUsersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers.ToListAsync(cancellationToken);
            var customerDtos = customers.Select(x => new CustomerDto()
            {
                Id = x.Id,
                Name = x.Name,
                Username = x.Username,
            });

            return customerDtos;
        }
    }
}
