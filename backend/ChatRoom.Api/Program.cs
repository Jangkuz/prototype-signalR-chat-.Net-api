using ChatRoom.Api.Chat;
using ChatRoom.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ChatRoomDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRoomRepository, MessageRoomRepository>();
builder.Services.AddScoped<IRoomMemberRepository, RoomMemberRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddControllers();

builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChatRoom.Api", Version = "v1" });
}); 

builder.WebHost.ConfigureKestrel(options =>{
    options.ListenLocalhost(5021);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("ChatPolicy", policy =>
    {
        policy
              .WithOrigins("http://localhost:3699")
        .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatRoom API V1");
});

    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
app.MapHub<ChatHub>("/chatHub");
app.UseCors("ChatPolicy");
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
