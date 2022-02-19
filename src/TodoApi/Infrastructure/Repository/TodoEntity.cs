using System;
using Amazon.DynamoDBv2.DataModel;
using TodoApi.Domain.Models;

namespace TodoApi.Infrastructure.Repository
{
    [DynamoDBTable("Todos")]
    public record TodoEntity
    {
        [DynamoDBHashKey]
        public Guid Id { get; init; }
        [DynamoDBRangeKey]
        public string TypeName { get; init; }   
        [DynamoDBProperty]
        public string TypeId { get; init; }
        [DynamoDBProperty]
        public string Name { get; init; }
        [DynamoDBProperty]
        public bool IsComplete { get; init; }
        [DynamoDBVersion]
        public int? VersionNumber { get; init; }

        public static TodoEntity Create(Todo todo){
            return new TodoEntity(){
                Id = todo.Id.Value,
                TypeName = todo.Type.Name,
                TypeId = todo.Type.Id,
                Name = todo.Name,
                IsComplete = todo.IsComplete
            };
        }
    }
}