using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApi.Models;
using MediatR;
using TodoApi.Domain.Commands;
using TodoApi.Domain.Queries;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        } 

        [HttpGet]
        public async IAsyncEnumerable<Models.TodoItemResponse> GetAll()
        {
            var todoCommand = new GetTodoListQuery();
            var items = await _mediator.Send(todoCommand);
            
            foreach (var item in items)
            {
                yield return item.ToResponse();
            }
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<ActionResult<Models.TodoItemResponse>> GetById(long id)
        {
            var todoCommand = new GetTodoQuery(){Id = id};
            var item = await _mediator.Send(todoCommand);
            if (item == null)
            {
                return NotFound();
            }
            return item.ToResponse();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItemRequest itemRequest)
        {
            var todoCommand = new CreateTodoCommand(){
                Id = itemRequest.Id,
                Type = "Note",
                Name = itemRequest.Name,
                IsComplete = itemRequest.IsComplete
            };
 
            var item = await _mediator.Send(todoCommand);

            return CreatedAtRoute("GetTodo", new { id = todoCommand.Id }, item.ToResponse());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] TodoItemRequest itemRequest)
        {
            var todoCommand = new UpdateTodoCommand(){
                Id = itemRequest.Id,
                Type = "Note",
                Name = itemRequest.Name,
                IsComplete = itemRequest.IsComplete
            };

            await _mediator.Send(todoCommand);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var todoCommand = new DeleteTodoCommand(){Id = id};
            await _mediator.Send(todoCommand);
            return new NoContentResult();
        }
    }
}