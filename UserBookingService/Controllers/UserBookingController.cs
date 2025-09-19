using Microsoft.AspNetCore.Mvc;
using UserBookingService.Data;
using UserBookingService.Services;

namespace UserBookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookingController(BookingService userBookingService) : ControllerBase
    {
        private readonly BookingService _userBookingService = userBookingService;

        [HttpPost]
        public async Task<IActionResult> CreateBooking()
        {
            var booking = await _userBookingService.AddBookingAsync();

            return Ok(new
            {
                Message = "Booking created successfully",
                booking.BookingId
            });
        }

        [HttpGet]
        public IActionResult GetAllBookings()
        {
            var bookings = _userBookingService.GetAllBookings();
            return Ok(bookings);
        }
    }
}
