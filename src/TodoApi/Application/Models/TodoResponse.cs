using System;
using TodoApi.Domain.Models;

namespace TodoApi.Application.Models
{
    public record TodoResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}