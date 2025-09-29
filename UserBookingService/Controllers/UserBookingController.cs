using Microsoft.AspNetCore.Mvc;
using UserBookingService.Data;
using UserBookingService.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using UserBookingService.Entities;

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
            // Hämta UserID från JWT-token
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdString == null || !Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized("User ID saknas eller är ogiltigt.");
            }

            var booking = await _userBookingService.AddBookingAsync(request.GymClassId, userId);

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
