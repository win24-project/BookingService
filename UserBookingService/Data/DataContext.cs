using Microsoft.EntityFrameworkCore;
using UserBookingService.Entities;

namespace UserBookingService.Data;


public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<UserBookingEntity> Bookings { get; set; }
}

//public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
//{
//    public DbSet<UserBookingEntity> Bookings { get; set; }
//}
