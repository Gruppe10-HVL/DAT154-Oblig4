using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DAT154Oblig4.Application.Common.Identity;
using DAT154Oblig4.Domain.Entities;

namespace DAT154Oblig4.Application.CustomerCommands
{
        public class CreateCustomerCommand : IRequest<CustomerAuthDto>
        {
            public string Name { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerAuthDto>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly IAuthentication _auth;

            public CreateCustomerCommandHandler(IApplicationDbContext context, IMapper mapper, IAuthentication auth)
            {
                _context = context;
                _mapper = mapper;
                _auth = auth;

            }

            public async Task<CustomerAuthDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var newCustomer = new Customer(request.Name, request.Username, request.Password);

                await _context.Customers.AddAsync(newCustomer);
                await _context.SaveChangesAsync(cancellationToken);

                CustomerDto mappedCustomer = _mapper.Map<CustomerDto>(newCustomer);
                string JWT = _auth.GenerateJWT(mappedCustomer);
                return new CustomerAuthDto(mappedCustomer, JWT);
            }
        }
    }

