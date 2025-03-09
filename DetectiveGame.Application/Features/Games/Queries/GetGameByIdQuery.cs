using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Games.Queries
{
    public class GetGameByIdQuery : IRequest<GameDto>
    {
        public Guid Id { get; set; }
    }

    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GameDto>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetGameByIdQueryHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<GameDto> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            var game = await _gameRepository.GetGameWithDetailsAsync(request.Id);
            return _mapper.Map<GameDto>(game);
        }
    }
} 