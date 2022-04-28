using DAT154Oblig4.Application.Bookings.Commands;
using DAT154Oblig4.Application.Bookings.Queries;
using DAT154Oblig4.Application.Dto;
using DAT154Oblig4.Application.ServiceTasks.Commands;
using DAT154Oblig4.Domain.Enums;
using DAT154Oblig4.Domain.Enums.Booking;
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

        //Borked, fix later
        public int CustomerId => Int32.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value);
            
        /// <summary>
        /// Get own bookings
        /// </summary>
        /// <remarks>Customer endpoint</remarks>
        [HttpGet("customer")]
        public async Task<ActionResult<IEnumerable<int>>> GetAllOwnBookings()
        {
            var customerId = CustomerId;
            var bookings = await Mediator.Send(new GetBookingsByCustomerId() { CustomerId = customerId });
            if (bookings == null) return NotFound();
            return Ok(bookings);
        }

        /// <summary>
        /// Get all customer bookings by customer id
        /// </summary>
        [HttpGet("customer/{id}")]
        public async Task<ActionResult<IEnumerable<int>>> GetAllCustomerBookings(int id)
        {
            var bookings = await Mediator.Send(new GetBookingsByCustomerId() { CustomerId = id });
            if (bookings == null) return NotFound();
            return Ok(bookings);
        }

        /// <summary>
        /// Get booking by id
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<int>>> GetAllBookings()
        {
            var bookings = await Mediator.Send(new GetAllBookingsQuery());
            if (bookings == null) return NotFound();
            return Ok(bookings);
        }

        /// <summary>
        /// Get booking by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(int id)
        {
            var booking = await Mediator.Send(new GetBookingByIdQuery() { Id = id });
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        /// <summary>
        /// Create new booking for self
        /// </summary>
        /// <remarks>Customer endpoint</remarks>
        [HttpPost("customer")]
        public async Task<ActionResult<BookingDto>> CreateOwnBooking(int roomId, DateTime startDate, DateTime endDate)
        {
            var customerId = CustomerId;
            var booking = await Mediator.Send(new CreateBookingCommand(customerId, roomId, startDate, endDate));
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        /// <summary>
        /// Create booking for user 
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBookingForCustomer(CreateBookingCommand request)
        {
            var booking = await Mediator.Send(request);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        /// <summary>
        /// Set booking to checked in  
        /// </summary>
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
        /// Cancel booking 
        /// </summary>
        /// <remarks>Customer endpoint</remarks>
        [HttpPatch("customer/cancel")]
        public async Task<ActionResult<BookingDto>> CancelOwnBooking(int id)
        {
            var booking = await Mediator.Send(new ChangeBookingStatusCommand() { Id = id, Status = BookingStatus.CheckedOut });
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        /// <summary>
        /// Cancel booking 
        /// </summary>
        [HttpPatch("cancel")]
        public async Task<ActionResult<BookingDto>> CancelBooking(int id)
        {
            var booking = await Mediator.Send(new ChangeBookingStatusCommand() { Id = id, Status = BookingStatus.CheckedOut });
            if (booking == null) return NotFound();
            return Ok(booking);
        }

    }
}
