using Amazon.DynamoDBv2.DataModel;

namespace TodoApi.Repository
{
    [DynamoDBTable("AnimalsInventory")]
    public class Item
    {
    [DynamoDBHashKey]
    public int Id { get; set; }
    [DynamoDBRangeKey]
    public string Type { get; set; }
    public string Name { get; set; }
    }
}