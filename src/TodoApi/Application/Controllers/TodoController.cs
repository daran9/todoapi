using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApi.Application.Models;
using MediatR;
using TodoApi.Domain.Commands;
using TodoApi.Domain.Queries;
using Microsoft.Extensions.Logging;
using System;
using TodoApi.Domain.Models;
using System.Threading;

namespace TodoApi.Application.Controllers
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
        public async IAsyncEnumerable<Models.TodoResponse> GetAllAsync(CancellationToken cancellationToken = default)
        {        
            var items = await HandleException<IEnumerable<Todo>>(async () =>
                {
                   var todoCommand = new GetTodoListQuery();
                    var itemsResult = await _mediator.Send(todoCommand, cancellationToken);
                    if(itemsResult.IsSuccess)
                        return itemsResult.Value;
                    return null;
                }, "Error getting TodoItem");

            foreach (var item in items)
            {
                yield return item.ToResponse();
            }
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<ActionResult<Models.TodoResponse>> GetByIdAsync(TodoId id, 
            CancellationToken cancellationToken = default)
            => await HandleException<ActionResult<Models.TodoResponse>>(async () =>
                {
                    var todoCommand = new GetTodoQuery(){Id = id};
                    var todoResult = await _mediator.Send(todoCommand, cancellationToken);
                    
                    return todoResult.IsSuccess 
                        ? Ok(todoResult.Value.ToResponse()) 
                        : NotFound();
                }, "Error getting TodoItem!");

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TodoRequest itemRequest,
            CancellationToken cancellationToken = default)
            => await HandleException<IActionResult>(async () =>
                {
                    var todoCommand = new CreateTodoCommand(){
                        Id = TodoId.New(),
                        Type = "Note",
                        Name = itemRequest.Name,
                        IsComplete = itemRequest.IsComplete
                    };
        
                    var item = await _mediator.Send(todoCommand, cancellationToken);
                
                    return CreatedAtRoute("GetTodo", new { id = todoCommand.Id }, item.ToResponse());
                }, "Error creating TodoItem!");


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(TodoId id, [FromBody] TodoRequest itemRequest,
            CancellationToken cancellationToken = default)
            => await HandleException<IActionResult>(async () =>
                {
                    var todoCommand = new UpdateTodoCommand(){
                        Id = id,
                        Type = "Note",
                        Name = itemRequest.Name,
                        IsComplete = itemRequest.IsComplete
                    };

                    var result = await _mediator.Send(todoCommand, cancellationToken);
                    return NoContent();
                }, "Error Updating TodoItem!");

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(TodoId id) 
            => await HandleException<IActionResult>(async () =>
                {
                    var todoCommand = new DeleteTodoCommand() { Id = id };
                    var result = await _mediator.Send(todoCommand);
                    return NoContent();
                }, "Error deleting TodoItem!");

        private async Task<T> HandleException<T>(Func<Task<T>> func, string errMsg)
        {
            try
            {
                return await func();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, errMsg);
                throw;
            }
        }
    }
}