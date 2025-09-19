using Microsoft.EntityFrameworkCore;
using UserBookingService.Entities;

namespace UserBookingService.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<UserBookingEntity> Bookings { get; set; }
}
