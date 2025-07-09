using System;
using TodoApi.Domain.Entities;
using TodoApi.Domain.ValueObjects;
using TodoApi.Infrastructure.Repository;

namespace TodoApi.Infrastructure.Extensions;

public static class DocumentExtensions
{
    public static Todo ToTodo(this TodoDocument item) =>
        new(new TodoId(Guid.Parse(item.Pk)),
            new TodoType(item.TypeId, item.Sk),
            item.Name,
            item.IsComplete);
}