using FluentValidation;
using DetectiveGame.Application.Features.Games.Commands;

namespace DetectiveGame.Application.Features.Games.Commands
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters");
        }
    }
} 