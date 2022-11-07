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
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using TodoApi.Extensions;

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

        /// <summary>
        /// Get all todo items
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async IAsyncEnumerable<Models.TodoResponse> GetAllAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
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

        /// <summary>
        /// Get todo by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTodo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Models.TodoResponse>> GetByIdAsync(Guid id, 
            CancellationToken cancellationToken = default)
            => await HandleException<ActionResult<Models.TodoResponse>>(async () =>
                {
                    var todoCommand = new GetTodoQuery(){Id = new TodoId(id) };
                    var todoResult = await _mediator.Send(todoCommand, cancellationToken);
                    
                    return todoResult.IsSuccess 
                        ? Ok(todoResult.Value.ToResponse()) 
                        : NotFound();
                }, "Error getting TodoItem!");

        /// <summary>
        /// Create a todo
        /// </summary>
        /// <param name="itemRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        
                    var todoResult = await _mediator.Send(todoCommand, cancellationToken);
                    return todoResult.IsSuccess 
                        ? CreatedAtRoute("GetTodo", new { id = todoCommand.Id }, todoResult.Value.ToResponse())
                        : BadRequest(todoResult.Error);
                }, "Error creating TodoItem!");

        /// <summary>
        /// Update a todo by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="itemRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TodoRequest itemRequest,
            CancellationToken cancellationToken = default)
            => await HandleException<IActionResult>(async () =>
                {
                    var todoCommand = new UpdateTodoCommand(){
                        Id = new TodoId(id),
                        Type = "Note",
                        Name = itemRequest.Name,
                        IsComplete = itemRequest.IsComplete
                    };

                    var todoResult = await _mediator.Send(todoCommand, cancellationToken);
                    return todoResult.IsSuccess 
                        ? NoContent() 
                        : BadRequest(todoResult.Error);
                }, "Error Updating TodoItem!");

        /// <summary>
        /// Delete a todo by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync(Guid id) 
            => await HandleException<IActionResult>(async () =>
                {
                    var todoCommand = new DeleteTodoCommand() { Id = new TodoId(id) };
                    var todoResult = await _mediator.Send(todoCommand);
                    return todoResult.IsSuccess 
                        ? NoContent() 
                        : BadRequest(todoResult.Error);
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