using FluentValidation;
using DetectiveGame.Application.Features.Players.Commands;

namespace DetectiveGame.Application.Features.Players.Commands
{
    public class JoinGameCommandValidator : AbstractValidator<JoinGameCommand>
    {
        public JoinGameCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters");

            RuleFor(x => x.ConnectionId)
                .NotEmpty().WithMessage("ConnectionId is required");

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("GameId is required");
        }
    }
} 