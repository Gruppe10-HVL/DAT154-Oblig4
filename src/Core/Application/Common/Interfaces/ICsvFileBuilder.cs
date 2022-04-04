using DAT154Oblig4.Application.TodoLists.Queries.ExportTodos;

namespace DAT154Oblig4.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
