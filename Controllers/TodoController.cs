using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Repository;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoItemRepository _repository;

        public TodoController(ITodoItemRepository repository)
        {
            _repository = repository;
        } 

        [HttpGet]
        public async IAsyncEnumerable<Models.TodoItem> GetAll()
        {
            var items = await _repository.GetAllAsync();
            
            foreach (var item in items)
            {
                yield return new Models.TodoItem(){
                    Id = item.Id,
                    Name = item.Name,
                    IsComplete = item.IsComplete
                };
            }
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<IActionResult> GetById(long id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(new Models.TodoItem(){
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.TodoItem item)
        {
            var todoItem = new TodoItem(){
                Id = item.Id,
                Type = "Note",
                Name = item.Name,
                IsComplete = item.IsComplete
            };
            await _repository.CreateAsync(todoItem);

            return CreatedAtRoute("GetTodo", new { id = todoItem.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Models.TodoItem item)
        {
            var todoItem = new TodoItem(){
                Id = item.Id,
                Type = "Note",
                Name = item.Name,
                IsComplete = item.IsComplete
            };

            await _repository.Update(id, todoItem);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _repository.Delete(id);
            return new NoContentResult();
        }
    }
}