using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using DetectiveGame.Domain.Entities;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Players.Commands
{
    public class JoinGameCommand : IRequest<PlayerDto>
    {
        public string Username { get; set; }
        public string ConnectionId { get; set; }
        public Guid GameId { get; set; }
    }

    public class JoinGameCommandHandler : IRequestHandler<JoinGameCommand, PlayerDto>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public JoinGameCommandHandler(
            IPlayerRepository playerRepository,
            IGameRepository gameRepository,
            IMapper mapper)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<PlayerDto> Handle(JoinGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.GetByIdAsync(request.GameId);
            if (game == null)
                throw new NotFoundException("Game not found");

            Player player = new Player
            {
                Username = request.Username,
                ConnectionId = request.ConnectionId,
                GameId = request.GameId,
                CreatedDate = DateTime.UtcNow
            };

            await _playerRepository.AddAsync(player);
            
            return _mapper.Map<PlayerDto>(player);
        }
    }
} 