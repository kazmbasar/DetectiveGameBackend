using Microsoft.AspNetCore.SignalR;
using MediatR;
using System;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using DetectiveGame.Application.Features.Evidences.Commands;
using DetectiveGame.Application.Features.Notes.Commands;

namespace DetectiveGame.WebAPI.Hubs
{
    public class GameHub : Hub
    {
        private readonly IMediator _mediator;

        public GameHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinGame(Guid gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        }

        public async Task LeaveGame(Guid gameId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId.ToString());
        }

        public async Task AddEvidence(Guid gameId, AddEvidenceCommand command)
        {
            var evidence = await _mediator.Send(command);
            await Clients.Group(gameId.ToString()).SendAsync("EvidenceAdded", evidence);
        }

        public async Task AddNote(Guid gameId, AddNoteCommand command)
        {
            var note = await _mediator.Send(command);
            await Clients.Group(gameId.ToString()).SendAsync("NoteAdded", note);
        }
    }
} 