using TodoApi.Domain.Models;
using TodoApi.Application.Models;
using TodoApi.Infrastructure.Repository;
using TodoApi.Domain.Commands;

namespace TodoApi
{
    public static class Extensions
    {
        public static TodoItemResponse ToResponse(this Todo item)
        {
            return new TodoItemResponse(){
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }

        public static Todo ToTodo(this TodoEntity item)
        {
            return new Todo(){
                Id = new TodoId(item.Id),
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }

        public static Todo ToTodo(this CreateTodoCommand request)
        {
            return new Todo(){
                Id = request.Id,
                Name = request.Name,
                Type = request.Type,
                IsComplete = request.IsComplete
            };
        }

        
        public static Todo ToTodo(this UpdateTodoCommand request)
        {
            return new Todo(){
                Id = request.Id,
                Name = request.Name,
                Type = request.Type,
                IsComplete = request.IsComplete
            };
        }
    }
}