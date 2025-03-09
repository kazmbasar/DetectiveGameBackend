using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using DetectiveGame.Application.Repositories;

namespace DetectiveGame.Application.Features.Notes.Queries
{
    public class GetNotesByPlayerQuery : IRequest<IEnumerable<NoteDto>>
    {
        public Guid PlayerId { get; set; }
    }

    public class GetNotesByPlayerQueryHandler : IRequestHandler<GetNotesByPlayerQuery, IEnumerable<NoteDto>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetNotesByPlayerQueryHandler(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteDto>> Handle(GetNotesByPlayerQuery request, CancellationToken cancellationToken)
        {
            var notes = await _noteRepository.GetNotesByPlayerIdAsync(request.PlayerId);
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }
    }
} 