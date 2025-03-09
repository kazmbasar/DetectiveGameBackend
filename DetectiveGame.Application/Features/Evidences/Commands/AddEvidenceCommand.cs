using System;
using MediatR;

using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using DetectiveGame.Domain.Entities;
using DetectiveGame.Application.Repositories;

namespace DetectiveGame.Application.Features.Evidences.Commands
{
    public class AddEvidenceCommand : IRequest<EvidenceDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid GameId { get; set; }
    }

    public class AddEvidenceCommandHandler : IRequestHandler<AddEvidenceCommand, EvidenceDto>
    {
        private readonly IEvidenceRepository _evidenceRepository;
        private readonly IMapper _mapper;

        public AddEvidenceCommandHandler(IEvidenceRepository evidenceRepository, IMapper mapper)
        {
            _evidenceRepository = evidenceRepository;
            _mapper = mapper;
        }

        public async Task<EvidenceDto> Handle(AddEvidenceCommand request, CancellationToken cancellationToken)
        {
            var evidence = _mapper.Map<Evidence>(request);
            evidence.CreatedDate = DateTime.UtcNow;

            await _evidenceRepository.AddAsync(evidence);
            
            return _mapper.Map<EvidenceDto>(evidence);
        }
    }
} 