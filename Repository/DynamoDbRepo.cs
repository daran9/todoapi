using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System.Threading.Tasks;

namespace TodoApi.Repository
{
    public class DynamoDbRepo //TODO : Use IRepository
    {
        public void InsertItem()
        {
            var client = new AmazonDynamoDBClient();
            var context = new DynamoDBContext(client);
            var item = new Item
            {
                Id = 4,
                Type = "Fish",
                Name = "Goldie"
            };

            context.SaveAsync(item);
        }

        public async Task<Item> GetItemAsync()
        {
            var client = new AmazonDynamoDBClient();
            var context = new DynamoDBContext(client);
            var item = await context.LoadAsync<Item>(4, "Fish");

            Console.WriteLine("Id = {0}", item.Id);
            Console.WriteLine("Type = {0}", item.Type);
            Console.WriteLine("Name = {0}", item.Name);

            return item;
        }
    }
}