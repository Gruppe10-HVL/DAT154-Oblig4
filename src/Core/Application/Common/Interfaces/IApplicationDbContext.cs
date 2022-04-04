using DAT154Oblig4.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAT154Oblig4.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
