using DAT154Oblig4.Application.Bookings.Commands;
using DAT154Oblig4.Application.Bookings.Queries;
using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Application.ServiceTasks.Commands;
using DAT154Oblig4.Domain.Enums;
using DAT154Oblig4.Domain.Enums.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DAT154Oblig4.Api.Controllers
{
    [ApiController]
    [Route("api/v1/booking")]
    public class BookingController : ApiControllerBase
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingController(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CustomerIdString => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
        public int CustomerId => CustomerIdString != null ? Int32.Parse(CustomerIdString) : (-1);
            
        /// <summary>
        /// Allow customer to get own bookings
        /// </summary>
        /// <remarks>Customer endpoint</remarks>
        [HttpGet("customer")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllOwnBookings()
        {
            var customerId = CustomerId;
            if (customerId == -1) return BadRequest();
            var bookings = await Mediator.Send(new GetBookingsByCustomerId() { CustomerId = customerId });
            if (bookings == null) return NotFound();
            return Ok(bookings);
        }

        /// <summary>
        /// Get all customer bookings by customer id
        /// </summary>
        /// <remarks>Front desk endpoint</remarks>
        [HttpGet("customer/{id}")]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllCustomerBookings(int id)
        {
            var bookings = await Mediator.Send(new GetBookingsByCustomerId() { CustomerId = id });
            if (bookings == null) return NotFound();
            return Ok(bookings);
        }

        /// <summary>
        /// Get all bookings
        /// </summary>
        /// <remarks>Front desk endpoint</remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetAllBookings()
        {
            var bookings = await Mediator.Send(new GetAllBookingsQuery());
            if (bookings == null) return NotFound();
            return Ok(bookings);
        }

        /// <summary>
        /// Get booking by id
        /// </summary>
        /// <remarks>Front desk endpoint</remarks>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(int id)
        {
            var booking = await Mediator.Send(new GetBookingByIdQuery() { Id = id });
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        /// <summary>
        /// Allows customer to get create own booking
        /// </summary>
        /// <remarks>Customer endpoint</remarks>
        [HttpPost("customer")]
        [Authorize]
        public async Task<ActionResult<BookingDto>> CreateOwnBooking(CreateBookingCommand request)
        {
            var customerId = CustomerId;
            if (customerId == -1) return BadRequest();
            request.CustomerId = customerId;
            var booking = await Mediator.Send(request);
            if (booking == null) return BadRequest();
            return Ok(booking);
        }

        /// <summary>
        /// Create booking for user 
        /// </summary>
        /// <remarks>Front desk endpoint</remarks>
        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBookingForCustomer(CreateBookingCommand request)
        {
            var booking = await Mediator.Send(request);
            if (booking == null) return BadRequest();
            return Ok(booking);
        }

        /// <summary>
        /// Set booking to checked in  
        /// </summary>
        /// <remarks>Front desk endpoint</remarks>

        [HttpPatch("checkin")]
        public async Task<ActionResult<BookingDto>> CheckInBooking(int id)
        {
            var booking = await Mediator.Send(new ChangeBookingStatusCommand() { Id = id, Status = BookingStatus.CheckedIn});
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        /// <summary>
        /// Set booking to checked out -> Also create new ServiceTask (Cleanup)  
        /// </summary>
        /// <remarks>Front desk endpoint</remarks>
        [HttpPatch("checkout")]
        public async Task<ActionResult<BookingDto>> CheckOutBooking(int id)
        {
            var booking = await Mediator.Send(new ChangeBookingStatusCommand() { Id = id, Status = BookingStatus.CheckedOut });
            if (booking == null) return NotFound();
            var newServiceTask = await Mediator.Send(new CreateServiceTaskCommand(booking.RoomId, "Cleanup on checkout",ServiceTaskType.Cleaning,ServiceTaskStatus.New,ServiceTaskPriority.MEDIUM,"None"));
            if (newServiceTask == null) Console.WriteLine("Automatic Cleanup Task Failed");
            return Ok(booking);
        }

        /// <summary>
        /// Allows customer to cancel their own booking
        /// </summary>
        /// <remarks>Customer endpoint</remarks>
        [HttpPatch("customer/cancel")]
        [Authorize]
        public async Task<ActionResult<BookingDto>> CancelOwnBooking(int id)
        {
            var customerId = CustomerId;
            if (customerId == -1) return BadRequest();
            var booking = await Mediator.Send(new ChangeBookingStatusCommand() { Id = id, Status = BookingStatus.CheckedOut });
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        /// <summary>
        /// Cancel booking 
        /// </summary>
        /// <remarks>Front desk endpoint</remarks>
        [HttpPatch("cancel")]
        public async Task<ActionResult<BookingDto>> CancelBooking(int id)
        {
            var booking = await Mediator.Send(new ChangeBookingStatusCommand() { Id = id, Status = BookingStatus.CheckedOut });
            if (booking == null) return NotFound();
            return Ok(booking);
        }

    }
}
