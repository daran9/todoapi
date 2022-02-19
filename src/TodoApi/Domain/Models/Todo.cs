using System;
using System.ComponentModel;
using CSharpFunctionalExtensions;
using StronglyTypedIds;

namespace TodoApi.Domain.Models
{
    [StronglyTypedId]
    public partial struct TodoId { }
    
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

        public TodoType Type { get; private set; }
        
        [Required]
        public string Name { get; private set; }
        
        [DefaultValue(false)]
        public bool IsComplete { get; private set; }

        private class RequiredAttribute : Attribute
        {
        }
    }
}