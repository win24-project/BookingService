using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using UserBookingService.Data;
using UserBookingService.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // LOKAL DATABAS VIA appsettings.json
        // builder.Services.AddDbContext<DataContext>(x =>
        //  x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Azure Key Vault
        var keyVaultUrl = "https://group-project-keyvault.vault.azure.net/";
        builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());

        var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        KeyVaultSecret dbSecret = await client.GetSecretAsync("DbConnectionString-GroupProject");

        builder.Services.AddDbContext<DataContext>(x =>
            x.UseSqlServer(dbSecret.Value));

        builder.Services.AddScoped<BookingService>();

        var app = builder.Build();

        
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            db.Database.Migrate();
        }

        
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API");
            c.RoutePrefix = string.Empty;
        });

        app.MapControllers();
        app.Run();
    }
}
