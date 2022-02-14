namespace TodoApi.Application.Models
{
    public record TodoItemRequest
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}