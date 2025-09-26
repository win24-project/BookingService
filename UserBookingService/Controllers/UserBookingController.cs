using Microsoft.AspNetCore.Mvc;
using UserBookingService.Data;
using UserBookingService.Services;
using Microsoft.AspNetCore.Authorization;

namespace UserBookingService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserBookingController : ControllerBase
    {
        private readonly BookingService _userBookingService;

        public UserBookingController(BookingService userBookingService)
        {
            _userBookingService = userBookingService;
        }

        [HttpPost]
public async Task<IActionResult> CreateBooking([FromBody] BookingRequestDto request)
{
    var booking = await _userBookingService.AddBookingAsync(request.GymClassId);

    return Ok(new
    {
        Message = "Ditt pass är bokat!",
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
