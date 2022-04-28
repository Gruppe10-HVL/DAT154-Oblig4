using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Application.ServiceTasks.Commands;
using DAT154Oblig4.Application.ServiceTasks.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DAT154Oblig4.Api.Controllers
{
    [ApiController]
    [Route("api/v1/task")]
    public class ServiceTaskController : ApiControllerBase
    {
        /// <summary>
        /// Get all service tasks
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceTaskDto>>> GetAllServiceTasks()
        {
            var servicetasks = await Mediator.Send(new GetAllServiceTasksQuery());
            if (servicetasks == null) return NotFound();
            return Ok(servicetasks);
        }


        /// <summary>
        /// Get service task by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceTaskDto>> GetServiceTaskById(int id)
        {
            var servicetask = await Mediator.Send(new GetServiceTaskByIdQuery() { Id = id});
            if (servicetask == null) return NotFound();
            return Ok(servicetask);
        }

        /// <summary>
        /// Create new service task
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ServiceTaskDto>> CreateServiceTask(CreateServiceTaskCommand request)
        {
            var servicetask = await Mediator.Send(request);
            if (servicetask == null) return BadRequest();
            return Ok(servicetask);
        }

        /// <summary>
        /// Change service task status
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<ActionResult<ServiceTaskDto>> UpdateServiceTask(UpdateServiceTaskCommand request)
        {
            var servicetask = await Mediator.Send(request);
            if (servicetask == null) return NotFound();
            return Ok(servicetask);
        }
    }
}
