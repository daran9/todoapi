using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApi.Models;
using MediatR;
using TodoApi.Domain.Commands;
using TodoApi.Domain.Queries;
using Microsoft.Extensions.Logging;
using System;
using TodoApi.Domain.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TodoController> _logger;

        public TodoController(IMediator mediator, ILogger<TodoController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async IAsyncEnumerable<Models.TodoItemResponse> GetAllAsync()
        {
            IEnumerable<TodoItem> items;
            try
            {
                var todoCommand = new GetTodoListQuery();
                items = await _mediator.Send(todoCommand);             
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting TodoItem!");
                throw;
            }
            
            foreach (var item in items)
            {
                yield return item.ToResponse();
            }
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<ActionResult<Models.TodoItemResponse>> GetByIdAsync(long id)
        {
            try
            {
                var todoCommand = new GetTodoQuery(){Id = id};
                var item = await _mediator.Send(todoCommand);
                if (item == null)
                {
                    return NotFound();
                }
                return item.ToResponse();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting TodoItem!");
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TodoItemRequest itemRequest)
        {
            try
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error creating TodoItem!");
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] TodoItemRequest itemRequest)
        {
            try
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
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error Updating TodoItem!");
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                var todoCommand = new DeleteTodoCommand(){Id = id};
                await _mediator.Send(todoCommand);
                return new NoContentResult();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error deleting TodoItem!");
                throw;
            }
        }
    }
}