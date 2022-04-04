using DAT154Oblig4.Application.Common.Mappings;
using DAT154Oblig4.Domain.Entities;

namespace DAT154Oblig4.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto : IMapFrom<TodoItem>
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}
