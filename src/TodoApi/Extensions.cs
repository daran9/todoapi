using TodoApi.Domain.Models;
using TodoApi.Application.Models;
using TodoApi.Infrastructure.Repository;
using TodoApi.Domain.Commands;

namespace TodoApi
{
    public static class Extensions
    {
        public static TodoItemResponse ToResponse(this TodoItem item)
        {
            return new TodoItemResponse(){
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }

        public static TodoItem ToItem(this TodoItemEntity item)
        {
            return new TodoItem(){
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }

        public static TodoItem ToItem(this CreateTodoCommand request)
        {
            return new TodoItem(){
                Id = request.Id,
                Name = request.Name,
                Type = request.Type,
                IsComplete = request.IsComplete
            };
        }

        
        public static TodoItem ToItem(this UpdateTodoCommand request)
        {
            return new TodoItem(){
                Id = request.Id,
                Name = request.Name,
                Type = request.Type,
                IsComplete = request.IsComplete
            };
        }
    }
}