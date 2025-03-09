using FluentValidation;
using DetectiveGame.Application.Features.Notes.Commands;

namespace DetectiveGame.Application.Features.Notes.Commands
{
    public class AddNoteCommandValidator : AbstractValidator<AddNoteCommand>
    {
        public AddNoteCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Note content is required")
                .MaximumLength(1000).WithMessage("Note content cannot be longer than 1000 characters");

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("GameId is required");

            RuleFor(x => x.PlayerId)
                .NotEmpty().WithMessage("PlayerId is required");
        }
    }
} 