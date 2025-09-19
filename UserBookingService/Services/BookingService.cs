using Microsoft.EntityFrameworkCore;
using UserBookingService.Data;
using UserBookingService.Entities;

namespace UserBookingService.Services
{
    public class BookingService(DataContext context)
    {
        private readonly DataContext _context = context;

        private DbSet<UserBookingEntity> Bookings => _context.Set<UserBookingEntity>();

        public async Task<UserBookingEntity> AddBookingAsync()
        {
            var booking = new UserBookingEntity();
            Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public IEnumerable<UserBookingEntity> GetAllBookings()
        {
            return Bookings.ToList();
        }
    }
}
