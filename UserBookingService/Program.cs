using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using UserBookingService.Data;
using UserBookingService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
var keyVaultUrl = "https://group-project-keyvault.vault.azure.net/";
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());

var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
KeyVaultSecret dbSecret = await client.GetSecretAsync("DbConnectionString-GroupProject");

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(dbSecret.Value));
//

builder.Services.AddScoped<BookingService>();

var app = builder.Build();

//
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}
//

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();

app.Run();
