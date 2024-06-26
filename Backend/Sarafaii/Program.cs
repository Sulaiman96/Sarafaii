using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sarafaii.Data;
using Sarafaii.Extensions;
using Sarafaii.Models;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddCors();
    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddIdentityServices(builder.Configuration);
}

var app = builder.Build();
{
    app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
        .WithOrigins("https://localhost:3000", "http://localhost:3000", "http://localhost:3001"));
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
        
        await context.Database.MigrateAsync();
        Seed.ResetAutoIncrement(context);

        await Seed.SeedUsers(userManager, roleManager);
        await Seed.SeedCurrency(context);
        await Seed.SeedExchangeRate(context);
        await Seed.SeedCustomer(context, userManager);
        await Seed.SeedDailyRate(context, userManager);
        await Seed.SeedLedger(context, userManager);

    }catch (Exception ex)
    {
        var logger = services.GetService<ILogger<Program>>();
        logger.LogError(ex, "An error occured during migration");
    }
    
    app.Run();
}
