using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

using DetectiveGame.Domain.Entities;

using System.Collections.Generic;
using DetectiveGame.Application.Repositories;

namespace DetectiveGame.Application.Features.Notes.Commands
{
    public class DeleteNoteCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Unit>
    {
        private readonly INoteRepository _noteRepository;

        public DeleteNoteCommandHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.GetByIdAsync(request.Id);
            if (note == null)
                throw new NotFoundException("Note not found");

            await _noteRepository.DeleteAsync(note);
            return Unit.Value;
        }
    }
} 