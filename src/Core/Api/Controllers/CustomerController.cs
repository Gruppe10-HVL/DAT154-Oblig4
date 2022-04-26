using DAT154Oblig4.Application.Customers.Commands;
using DAT154Oblig4.Application.Customers.Queries;
using DAT154Oblig4.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAT154Oblig4.Api.Controllers
{
    [ApiController]
    [Route("api/v1/customer")]
    public class CustomerController : ApiControllerBase
    {
        /// <summary>
        /// Gets all customer ids
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<int>>> GetAllCustomers(){
            var customers = await Mediator.Send(new GetAllCustomersQuery());
            if (customers == null) return NotFound();
            return Ok(customers);
        }

        /// <summary>
        /// Gets customer by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            var customer = await Mediator.Send(new GetCustomerByIdQuery() { Id = id });
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        /// <summary>
        /// Registers a new customer and returns created object with JWT
        /// </summary>
        /// <param name="request">Name, Username and Password</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CustomerAuthDto>> CreateCustomer(CreateCustomerCommand request)
        {
            var customer = await Mediator.Send(request);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        /// <summary>
        /// Logs in customer, returns JWT as proof of auth
        /// </summary>
        /// <param name="request">Username and Password</param>
        [HttpPost("login")]
        public async Task<ActionResult<CustomerAuthDto>> CustomerLogin(LoginCustomerCommand request)
        {
            var customer = await Mediator.Send(request);
            if (customer == null) return BadRequest();
            return Ok(customer);
        }

    }
}
