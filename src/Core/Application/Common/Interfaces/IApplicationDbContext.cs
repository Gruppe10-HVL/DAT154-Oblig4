using DAT154Oblig4.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Common.Interfaces;

public interface IApplicationDbContext
{

    DbSet<Customer> Customers { get; set; }

    DbSet<Room> Rooms { get; set; }

    DbSet<ServiceTask> ServiceTasks { get; set; }

    DbSet<Booking> Bookings { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
