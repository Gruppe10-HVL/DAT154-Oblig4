using System.Globalization;
using DAT154Oblig4.Application.Common.Interfaces;
using DAT154Oblig4.Application.TodoLists.Queries.ExportTodos;
using DAT154Oblig4.Infrastructure.Files.Maps;
using CsvHelper;

namespace DAT154Oblig4.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Configuration.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
