using CSharpFunctionalExtensions;

public class TodoType: EnumValueObject<TodoType>
{
    public static TodoType None = new ("0", nameof(Note));
    public static TodoType Note = new("1", nameof(Note));

    public string Name { get; }
    
    public TodoType(string id, string name) 
            : base(id)
        {
            Name = name;
        }
}