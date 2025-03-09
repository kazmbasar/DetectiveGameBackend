using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using DetectiveGame.Application.Repositories;
using DetectiveGame.Domain.Entities;

namespace DetectiveGame.Application.Features.Games.Commands
{
    public class CreateGameCommand : IRequest<GameDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, GameDto>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<GameDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = _mapper.Map<Game>(request);
            game.Status = GameStatus.Waiting;
            game.CreatedDate = DateTime.UtcNow;

            await _gameRepository.AddAsync(game);
            
            return _mapper.Map<GameDto>(game);
        }
    }
} 