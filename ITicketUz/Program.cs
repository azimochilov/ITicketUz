using ITicketUZ.Data.Contexts;
using ITicketUZ.Data.IRepositories;
using ITicketUZ.Data.Repositories;
using ITicketUZ.Service.Interfaces;
using ITicketUZ.Service.Mappers;
using ITicketUZ.Service.Services;
using ITicketUZ.Service.Servicesl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddAutoMapper(typeof(MapperProfile));


builder.Services.AddDbContext<AppDbContexts>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
