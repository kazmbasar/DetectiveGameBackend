using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Evidences.Queries
{
    public class GetEvidencesByGameQuery : IRequest<IEnumerable<EvidenceDto>>
    {
        public Guid GameId { get; set; }
    }

    public class GetEvidencesByGameQueryHandler : IRequestHandler<GetEvidencesByGameQuery, IEnumerable<EvidenceDto>>
    {
        private readonly IEvidenceRepository _evidenceRepository;
        private readonly IMapper _mapper;

        public GetEvidencesByGameQueryHandler(IEvidenceRepository evidenceRepository, IMapper mapper)
        {
            _evidenceRepository = evidenceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EvidenceDto>> Handle(GetEvidencesByGameQuery request, CancellationToken cancellationToken)
        {
            var evidences = await _evidenceRepository.GetEvidencesByGameIdAsync(request.GameId);
            return _mapper.Map<IEnumerable<EvidenceDto>>(evidences);
        }
    }
} 