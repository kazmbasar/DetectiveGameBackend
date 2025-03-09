using FluentValidation;
using DetectiveGame.Application.Features.Games.Commands;

namespace DetectiveGame.Application.Features.Games.Commands
{
    public class UpdateGameStatusCommandValidator : AbstractValidator<UpdateGameStatusCommand>
    {
        public UpdateGameStatusCommandValidator()
        {
            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("GameId is required");

            RuleFor(x => x.NewStatus)
                .IsInEnum().WithMessage("Invalid game status");
        }
    }
} 