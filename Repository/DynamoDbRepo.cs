using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using System.Threading.Tasks;

namespace TodoApi.Repository
{
    public class DynamoDbRepo: IRepository<Item>
    {
        IAmazonDynamoDB _client;
        IDynamoDBContext _context;

        public DynamoDbRepo(){
            _client = new AmazonDynamoDBClient();
            _context = new DynamoDBContext(_client);
        }

        public Item GetById(Object id){
            throw new NotImplementedException();
        }

        public void Create(Item entity){
            throw new NotImplementedException();
        }

        public void Delete(Item entity){
            throw new NotImplementedException();
        }

        public void Update(Item entity){
            throw new NotImplementedException();
        }

        public void Insert()
        {              
            var item = new Item
            {
                Id = 4,
                Type = "Fish",
                Name = "Goldie"
            };

            _context.SaveAsync(item);
        }

        public async Task<Item> GetAsync()
        {
            var item = await _context.LoadAsync<Item>(4, "Fish");

            Console.WriteLine("Id = {0}", item.Id);
            Console.WriteLine("Type = {0}", item.Type);
            Console.WriteLine("Name = {0}", item.Name);

            return item;
        }
    }
}