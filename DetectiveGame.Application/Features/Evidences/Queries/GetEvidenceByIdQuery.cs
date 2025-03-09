using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Evidences.Queries
{
    public class GetEvidenceByIdQuery : IRequest<EvidenceDto>
    {
        public Guid Id { get; set; }
    }

    public class GetEvidenceByIdQueryHandler : IRequestHandler<GetEvidenceByIdQuery, EvidenceDto>
    {
        private readonly IEvidenceRepository _evidenceRepository;
        private readonly IMapper _mapper;

        public GetEvidenceByIdQueryHandler(IEvidenceRepository evidenceRepository, IMapper mapper)
        {
            _evidenceRepository = evidenceRepository;
            _mapper = mapper;
        }

        public async Task<EvidenceDto> Handle(GetEvidenceByIdQuery request, CancellationToken cancellationToken)
        {
            var evidence = await _evidenceRepository.GetByIdAsync(request.Id);
            return _mapper.Map<EvidenceDto>(evidence);
        }
    }
} 