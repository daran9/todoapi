using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using StronglyTypedIds;
using TodoApi.Domain.ValueObjects;

namespace TodoApi.Domain.Entities;

[StronglyTypedId(converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct TodoId
{
}

public class Todo : Entity<TodoId>
{
    //TODO: Change to value Objects
    public Todo(TodoId id, TodoType type, string name, bool isComplete)
        : base(id)
    {
        Type = type;
        Name = name;
        IsComplete = isComplete;
    }

    public TodoType Type { get; }

    [Required] 
    public string Name { get; }

    [DefaultValue(false)] 
    public bool IsComplete { get; }
}