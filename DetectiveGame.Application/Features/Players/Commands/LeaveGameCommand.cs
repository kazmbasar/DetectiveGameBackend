using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;


using DetectiveGame.Domain.Entities;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Players.Commands
{
    public class LeaveGameCommand : IRequest<Unit>
    {
        public Guid PlayerId { get; set; }
    }

    public class LeaveGameCommandHandler : IRequestHandler<LeaveGameCommand, Unit>
    {
        private readonly IPlayerRepository _playerRepository;

        public LeaveGameCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<Unit> Handle(LeaveGameCommand request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.PlayerId);
            if (player == null)
                throw new NotFoundException("Player not found");

            await _playerRepository.DeleteAsync(player);
            return Unit.Value;
        }
    }
} 