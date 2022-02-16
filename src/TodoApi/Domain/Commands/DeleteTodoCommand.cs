using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Domain.Commands
{
    public class DeleteTodoCommand : IRequest<Result>
    {
        public TodoId Id { get; set; }
    }

    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, Result>
    {
        private readonly ITodoRepository _repository;

        public DeleteTodoCommandHandler(ITodoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new ArgumentNullException($"{nameof(request)} is null");
            return await _repository.Delete(request.Id);
        }
    }
}