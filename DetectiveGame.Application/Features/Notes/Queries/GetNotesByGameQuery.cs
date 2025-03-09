using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Notes.Queries
{
    public class GetNotesByGameQuery : IRequest<IEnumerable<NoteDto>>
    {
        public Guid GameId { get; set; }
    }

    public class GetNotesByGameQueryHandler : IRequestHandler<GetNotesByGameQuery, IEnumerable<NoteDto>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetNotesByGameQueryHandler(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NoteDto>> Handle(GetNotesByGameQuery request, CancellationToken cancellationToken)
        {
            var notes = await _noteRepository.GetNotesByGameIdAsync(request.GameId);
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }
    }
} 