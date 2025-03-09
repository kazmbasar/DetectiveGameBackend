using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using DetectiveGame.Domain.Entities;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Games.Commands
{
    public class UpdateGameStatusCommand : IRequest<GameDto>
    {
        public Guid GameId { get; set; }
        public GameStatus NewStatus { get; set; }
    }

    public class UpdateGameStatusCommandHandler : IRequestHandler<UpdateGameStatusCommand, GameDto>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public UpdateGameStatusCommandHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<GameDto> Handle(UpdateGameStatusCommand request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.GetByIdAsync(request.GameId);
            if (game == null)
                throw new NotFoundException("Game not found");

            game.Status = request.NewStatus;
            game.UpdatedDate = DateTime.UtcNow;

            await _gameRepository.UpdateAsync(game);
            
            return _mapper.Map<GameDto>(game);
        }
    }
} 