using Microsoft.AspNetCore.Mvc;
using DetectiveGame.Application.Features.Notes.Commands;
using DetectiveGame.Application.Features.Notes.Queries;

using Microsoft.AspNetCore.SignalR;

using DetectiveGame.WebAPI.Hubs;
using MediatR;
using FluentValidation;

namespace DetectiveGame.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<GameHub> _hubContext;

        public NotesController(IMediator mediator, IHubContext<GameHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ActionResult<NoteDto>> AddNote(AddNoteCommand command)
        {
            try
            {
                var note = await _mediator.Send(command);
                await _hubContext.Clients.Group(command.GameId.ToString())
                    .SendAsync("NoteAdded", note);
                return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> GetNote(Guid id)
        {
            var query = new GetNoteByIdQuery { Id = id };
            var note = await _mediator.Send(query);

            if (note == null)
                return NotFound();

            return Ok(note);
        }

        [HttpGet("game/{gameId}")]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetNotesByGame(Guid gameId)
        {
            var query = new GetNotesByGameQuery { GameId = gameId };
            var notes = await _mediator.Send(query);
            return Ok(notes);
        }

        [HttpGet("player/{playerId}")]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetNotesByPlayer(Guid playerId)
        {
            var query = new GetNotesByPlayerQuery { PlayerId = playerId };
            var notes = await _mediator.Send(query);
            return Ok(notes);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNote(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteNoteCommand { Id = id });
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
} 