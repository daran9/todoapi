using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Domain.Repository;
using TodoApi.Domain.Models;
using CSharpFunctionalExtensions;

namespace TodoApi.Domain.Commands
{
    public class UpdateTodoCommand : IRequest<Result<bool>>
    {
        public TodoId Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, Result<bool>>
    {
        private readonly ITodoRepository _repository;

        public UpdateTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<bool>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            //TODO: Change bool to LinkedResource
            if(request == null)
                throw new ArgumentNullException($"{nameof(request)} is null");

            var item = await _repository.GetByIdAsync(request.Id, request.Type);
            if(item.IsFailure)
                return Result.Failure<bool>($"{nameof(item)} is not found");
                  
            await _repository.Update(request.Id, request.ToTodo());

            return Result.Success(true);
        }
    }
}