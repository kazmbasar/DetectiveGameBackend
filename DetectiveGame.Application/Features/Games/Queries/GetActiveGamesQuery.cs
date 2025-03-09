using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Games.Queries
{
    public class GetActiveGamesQuery : IRequest<IEnumerable<GameDto>>
    {
    }

    public class GetActiveGamesQueryHandler : IRequestHandler<GetActiveGamesQuery, IEnumerable<GameDto>>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetActiveGamesQueryHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameDto>> Handle(GetActiveGamesQuery request, CancellationToken cancellationToken)
        {
            var games = await _gameRepository.GetActiveGamesAsync();
            return _mapper.Map<IEnumerable<GameDto>>(games);
        }
    }
} 