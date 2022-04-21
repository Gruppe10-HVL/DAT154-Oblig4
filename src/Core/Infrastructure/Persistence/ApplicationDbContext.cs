using System.Reflection;
using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Room> Rooms { get; set; }

    public DbSet<ServiceTask> ServiceTasks { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
