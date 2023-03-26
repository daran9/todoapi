using Amazon.DynamoDBv2.DataModel;
using TodoApi.Domain.Entities;

namespace TodoApi.Infrastructure.Repository;

[DynamoDBTable("Todos")]
public record TodoDocument : Document
{
    [DynamoDBProperty] public string TypeId { get; init; }
    [DynamoDBProperty] public string Name { get; init; }
    [DynamoDBProperty] public bool IsComplete { get; init; }
    [DynamoDBVersion] public int? VersionNumber { get; init; }

    public static TodoDocument Create(Todo todo)
    {
        return new TodoDocument
        {
            Pk = todo.Id.Value.ToString(),
            Sk = todo.Type.Name,
            TypeId = todo.Type.Id,
            Name = todo.Name,
            IsComplete = todo.IsComplete
        };
    }
}