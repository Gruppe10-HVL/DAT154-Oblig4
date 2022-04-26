﻿using DAT154Oblig4.Application.Dto;
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

        public int CustomerId => Int32.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name).Value);
            
        /// <summary>
        /// Get own bookings
        /// </summary>
        /// <remarks>Customer endpoint</remarks>
        [HttpGet("customer")]
        public async Task<ActionResult<IEnumerable<int>>> GetAllOwnBookings()
        {
            var customerId = CustomerId;
            Console.WriteLine(customerId);
            return Ok();
        }

        /// <summary>
        /// Get all customer bookings by customer id
        /// </summary>
        [HttpGet("customer/{id}")]
        public async Task<ActionResult<IEnumerable<int>>> GetAllCustomerBookings(int id)
        {
            return Ok();
        }

        /// <summary>
        /// Get booking by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById()
        {
            return Ok();
        }

        /// <summary>
        /// Create new booking for self
        /// </summary>
        /// <remarks>Customer endpoint</remarks>
        [HttpPost("customer")]
        public async Task<ActionResult<BookingDto>> CreateOwnBooking()
        {
            var customerId = CustomerId;
            return Ok();
        }

        /// <summary>
        /// Create booking for user 
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBookingForCustomer()
        {
            return Ok();
        }

        /// <summary>
        /// Set booking to checked in  
        /// </summary>
        [HttpPatch("checkin")]
        public async Task<ActionResult<BookingDto>> CheckInBooking()
        {
            return Ok();
        }

        /// <summary>
        /// Set booking to checked out -> Also create new ServiceTask (Cleanup)  
        /// </summary>
        [HttpPatch("checkout")]
        public async Task<ActionResult<BookingDto>> CheckOutBooking()
        {
            return Ok();
        }

        /// <summary>
        /// Cancel booking 
        /// </summary>
        /// <remarks>Customer endpoint</remarks>
        [HttpPatch("customer/cancel")]
        public async Task<ActionResult<BookingDto>> CancelOwnBooking()
        {
            var customerId = CustomerId;
            return Ok();
        }
        /// <summary>
        /// Cancel booking 
        /// </summary>
        [HttpPatch("cancel")]
        public async Task<ActionResult<BookingDto>> CancelBooking()
        {
            return Ok();
        }

    }
}
