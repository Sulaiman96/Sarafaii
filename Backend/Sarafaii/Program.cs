using Sarafaii.Extensions;

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
        .WithOrigins("https://localhost:3000", "http://localhost:3000"));
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}