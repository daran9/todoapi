using TodoApi.Domain.Models;
using TodoApi.Application.Models;
using TodoApi.Infrastructure.Repository;
using TodoApi.Domain.Commands;

namespace TodoApi
{
    public static class Extensions
    {
        public static TodoResponse ToResponse(this Todo item)
        {
            return new TodoResponse(){
                Id = item.Id.Value,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }

        public static Todo ToTodo(this TodoEntity item) 
            => new Todo(new TodoId(item.Id), new TodoType(item.TypeId, item.TypeName), item.Name, item.IsComplete);

        public static Todo ToTodo(this CreateTodoCommand request)
            => new Todo(request.Id, request.Type.ToType(), request.Name, request.IsComplete);


        public static Todo ToTodo(this UpdateTodoCommand request) 
            => new Todo(request.Id, request.Type.ToType(), request.Name, request.IsComplete);


        public static TodoType ToType(this string typeName) 
        {
            if(TodoType.Note.Name == typeName)
                return TodoType.Note;
            return TodoType.None;
        }
    }
}