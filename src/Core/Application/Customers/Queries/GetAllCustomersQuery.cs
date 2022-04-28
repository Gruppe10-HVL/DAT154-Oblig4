
using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>> { }

    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;


        public GetAllCustomersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers.ProjectToType<CustomerDto>(_mapper.Config).ToListAsync(cancellationToken);

            return customers;
        }
    }
}
