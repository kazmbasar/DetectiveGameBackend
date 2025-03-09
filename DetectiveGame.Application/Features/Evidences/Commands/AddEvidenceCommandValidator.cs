using FluentValidation;


namespace DetectiveGame.Application.Features.Evidences.Commands
{
    public class AddEvidenceCommandValidator : AbstractValidator<AddEvidenceCommand>
    {
        public AddEvidenceCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot be longer than 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .MaximumLength(1000).WithMessage("Description cannot be longer than 1000 characters");

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("GameId is required");

            RuleFor(x => x.ImageUrl)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.ImageUrl))
                .WithMessage("Invalid URL format");
        }
    }
} 