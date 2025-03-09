using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;

using DetectiveGame.Domain.Entities;
using DetectiveGame.Application.Repositories;


namespace DetectiveGame.Application.Features.Notes.Commands
{
    public class AddNoteCommand : IRequest<NoteDto>
    {
        public string Content { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
    }

    public class AddNoteCommandHandler : IRequestHandler<AddNoteCommand, NoteDto>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public AddNoteCommandHandler(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<NoteDto> Handle(AddNoteCommand request, CancellationToken cancellationToken)
        {
            var note = _mapper.Map<Note>(request);
            note.CreatedDate = DateTime.UtcNow;

            await _noteRepository.AddAsync(note);
            
            return _mapper.Map<NoteDto>(note);
        }
    }
} 