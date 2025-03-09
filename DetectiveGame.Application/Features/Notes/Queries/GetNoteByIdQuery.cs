using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Notes.Queries
{
    public class GetNoteByIdQuery : IRequest<NoteDto>
    {
        public Guid Id { get; set; }
    }

    public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteDto>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetNoteByIdQueryHandler(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<NoteDto> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetByIdAsync(request.Id);
            return _mapper.Map<NoteDto>(note);
        }
    }
} 