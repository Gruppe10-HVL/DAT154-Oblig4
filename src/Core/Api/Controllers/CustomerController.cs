using DAT154Oblig4.Application.CustomerQueries;
using Microsoft.AspNetCore.Mvc;

namespace DAT154Oblig4.Api.Controllers
{
    [ApiController]
    [Route("api/v1/customer")]
    public class CustomerController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<string>> GetAllCustomers(){
            return Ok(await Mediator.Send(new GetAllUsersQuery()));
        }
         
    }
}
