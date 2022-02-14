using StronglyTypedIds;

namespace TodoApi.Domain.Models
{
    [StronglyTypedId]
    public partial struct TodoId { }
    
    public record Todo
    {
        public TodoId Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}