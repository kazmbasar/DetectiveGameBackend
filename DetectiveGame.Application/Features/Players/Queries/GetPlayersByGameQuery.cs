using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Players.Queries
{
    public class GetPlayersByGameQuery : IRequest<IEnumerable<PlayerDto>>
    {
        public Guid GameId { get; set; }
    }

    public class GetPlayersByGameQueryHandler : IRequestHandler<GetPlayersByGameQuery, IEnumerable<PlayerDto>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public GetPlayersByGameQueryHandler(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerDto>> Handle(GetPlayersByGameQuery request, CancellationToken cancellationToken)
        {
            var players = await _playerRepository.GetPlayersByGameIdAsync(request.GameId);
            return _mapper.Map<IEnumerable<PlayerDto>>(players);
        }
    }
} 