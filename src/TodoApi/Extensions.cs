using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;
using TodoApi.Models;

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
    }
}