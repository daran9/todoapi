using TodoApi.Application.Commands;
using TodoApi.Domain.Entities;
using TodoApi.Domain.ValueObjects;
using TodoApi.Web.Models;

namespace TodoApi.Application.Extensions;

public static class DtoExtensions
{
    public static TodoResponse ToResponse(this Todo item)
    {
        return new TodoResponse
        {
            Id = item.Id.Value,
            Name = item.Name,
            IsComplete = item.IsComplete
        };
    }

    public static Todo ToTodo(this CreateTodoCommand request)
    {
        return new(request.Id, request.Type.ToType(), request.Name, request.IsComplete);
    }

    public static Todo ToTodo(this UpdateTodoCommand request)
    {
        return new(request.Id, request.Type.ToType(), request.Name, request.IsComplete);
    }

    private static TodoType ToType(this string typeName)
    {
        if (TodoType.Note.Name == typeName)
            return TodoType.Note;
        return TodoType.None;
    }
}