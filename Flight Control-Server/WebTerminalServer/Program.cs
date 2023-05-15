using Microsoft.EntityFrameworkCore;
using WebTerminalServer.DAL;
using WebTerminalServer.Hubs;
using WebTerminalServer.Logic;
using WebTerminalServer.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("AirPortConnection");
builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(connectionString), ServiceLifetime.Singleton);

builder.Services.AddSingleton<IAirPortRepository, AirPortRepository>();
builder.Services.AddScoped<MovementLogic>();
builder.Services.AddScoped<FlightHub>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
       .AllowAnyMethod()
       .AllowAnyHeader()
       .SetIsOriginAllowed(origin => true) // allow any origin
       .AllowCredentials()); 
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<FlightHub>("/flighthub");

app.Run();
