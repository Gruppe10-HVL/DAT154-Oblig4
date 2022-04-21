using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.Dto;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DAT154Oblig4.Application.Common.Identity;

namespace DAT154Oblig4.Application.CustomerCommands
{
    public class LoginCustomerCommand : IRequest<CustomerAuthDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginCustomerCommandHandler : IRequestHandler<LoginCustomerCommand, CustomerAuthDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthentication _auth;

        public LoginCustomerCommandHandler(IApplicationDbContext context, IMapper mapper, IAuthentication auth)
        {
            _context = context;
            _mapper = mapper;
            _auth = auth;

        }

        public async Task<CustomerAuthDto> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(x => x.Username == request.Username && x.Password == request.Password).FirstOrDefaultAsync();

            if (customer == null) return null;
            CustomerDto mappedCustomer = _mapper.Map<CustomerDto>(customer);
            string JWT = _auth.GenerateJWT(mappedCustomer);
            return new CustomerAuthDto(mappedCustomer, JWT);
        }
    }
}
