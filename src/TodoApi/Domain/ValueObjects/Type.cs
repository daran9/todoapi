using CSharpFunctionalExtensions;

namespace TodoApi.Domain.ValueObjects;

public class TodoType : EnumValueObject<TodoType>
{
    public static readonly TodoType None = new("0", nameof(Note));
    public static readonly TodoType Note = new("1", nameof(Note));

    public TodoType(string id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; }
}