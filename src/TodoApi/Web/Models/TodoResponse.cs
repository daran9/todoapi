using System;

namespace TodoApi.Web.Models;

public record TodoResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
}