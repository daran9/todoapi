using System;
using Amazon.DynamoDBv2.DataModel;

namespace TodoApi.Infrastructure.Repository
{
    [DynamoDBTable("Todos")]
    public class TodoEntity
    {
        [DynamoDBHashKey]
        public Guid Id { get; set; }
        [DynamoDBRangeKey]
        public string Type { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public bool IsComplete { get; set; }
        [DynamoDBVersion]
        public int? VersionNumber { get; set; }
    }
}