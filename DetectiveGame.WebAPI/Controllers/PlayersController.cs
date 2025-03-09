using Microsoft.AspNetCore.Mvc;
using DetectiveGame.Application.Features.Players.Commands;
using DetectiveGame.Application.Features.Players.Queries;

using DetectiveGame.WebAPI.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using FluentValidation;

namespace DetectiveGame.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<GameHub> _hubContext;

        public PlayersController(IMediator mediator, IHubContext<GameHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpPost("join")]
        public async Task<ActionResult<PlayerDto>> JoinGame(JoinGameCommand command)
        {
            try
            {
                var player = await _mediator.Send(command);
                await _hubContext.Clients.Group(command.GameId.ToString())
                    .SendAsync("PlayerJoined", player);
                return Ok(player);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpGet("game/{gameId}")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayersByGame(Guid gameId)
        {
            var query = new GetPlayersByGameQuery { GameId = gameId };
            var players = await _mediator.Send(query);
            return Ok(players);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> LeaveGame(Guid id)
        {
            try
            {
                var command = new LeaveGameCommand { PlayerId = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
} 