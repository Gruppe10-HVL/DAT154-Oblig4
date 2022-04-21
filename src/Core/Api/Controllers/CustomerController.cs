using DAT154Oblig4.Application.CustomerCommands;
using DAT154Oblig4.Application.CustomerQueries;
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
        /// Gets all users
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers(){
            var customers = await Mediator.Send(new GetAllUsersQuery());
            if (customers == null) return NotFound();
            return Ok(customers);
        }

        /// <summary>
        /// Registers a new user and returns created object with JWT
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
        /// Logs in user, returns JWT
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
