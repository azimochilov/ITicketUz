using ITicketUZ.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ITicketUZ.Data.Contexts;
public class AppDbContexts : DbContext
{
    public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options) { }

    public DbSet<Venue> Venues { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Event> Events { get; set; }
}
