using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Mediator;
using TodoApi.Application.Extensions;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Repository;

namespace TodoApi.Application.Commands;

public class UpdateTodoCommand : IRequest<Result>
{
    public TodoId Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
}

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, Result>
{
    private readonly ITodoRepository _repository;

    public UpdateTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        //TODO: Change return result to LinkedResource
        if (request == null)
            throw new ArgumentNullException($"{nameof(request)} is null");

        var item = await _repository.GetByIdAsync(request.Id, request.Type);
        if (item.IsFailure)
            return Result.Failure($"{nameof(item)} is not found");

        await _repository.Update(request.Id, request.ToTodo());

        return Result.Success();
    }
}