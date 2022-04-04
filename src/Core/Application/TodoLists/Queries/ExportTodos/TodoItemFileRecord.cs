using DAT154Oblig4.Application.Common.Mappings;
using DAT154Oblig4.Domain.Entities;

namespace DAT154Oblig4.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
