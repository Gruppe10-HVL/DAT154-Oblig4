using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Application.Rooms.Commands;
using DAT154Oblig4.Application.Rooms.Queries;
using DAT154Oblig4.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace DAT154Oblig4.Api.Controllers
{
    [ApiController]
    [Route("api/v1/room")]
    public class RoomController : ApiControllerBase
    {
        /// <summary>
        /// Gets all rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAllRoomIds()
        {
            var rooms = await Mediator.Send(new GetAllRoomsQuery());
            if (rooms == null) return NotFound();
            return Ok(rooms);
        }

        /// <summary>
        /// Gets room by id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoomById(int id)
        {
            var room = await Mediator.Send(new GetRoomByIdQuery() { Id = id});
            if (room == null) return NotFound();
            return Ok(room);
        }

        /// <summary>
        /// Creates a new room
        /// </summary>
        /// <param name="bedCount"></param>
        /// <param name="size"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<RoomDto>> CreateRoom(int bedCount, int size, RoomQuality quality)
        {
            var room = await Mediator.Send(new CreateNewRoomCommand(bedCount, size, quality));
            if (room == null) return BadRequest();
            return Ok(room);
        }

        /// <summary>
        /// Deletes a room by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult<IEnumerable<RoomDto>>> DeleteRoom(int id)
        {
            var room = await Mediator.Send(new DeleteRoomCommand(id));
            if (room == null) return NotFound();
            return Ok(room);
        }
    }
}
