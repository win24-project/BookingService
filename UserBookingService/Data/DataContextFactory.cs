using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UserBookingService.Data;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;AttachDbFilename=D:\\VS2022Projects\\Projekt-Grupp\\BookingService\\UserBookingService\\Data\\BookingServiceDB.mdf;Database=database;Trusted_Connection=True;MultipleActiveResultSets=True");

        return new DataContext(optionsBuilder.Options);
    }
}
