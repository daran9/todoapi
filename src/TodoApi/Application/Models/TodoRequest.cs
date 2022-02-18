using FluentValidation;

namespace TodoApi.Application.Models
{
    public record TodoRequest
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class TodoRequestValidator : AbstractValidator<TodoRequest>
    {
        public TodoRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Please specify a name")
                .Length(1, 250)
                .WithMessage("Must be between 1 and 250 characters");
        }
    }
}