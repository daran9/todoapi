using Amazon.DynamoDBv2.DataModel;

namespace TodoApi.Infrastructure.Repository
{
    [DynamoDBTable("TodoItems")]
    public class TodoItemEntity
    {
        [DynamoDBHashKey]
        public long Id { get; set; }
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