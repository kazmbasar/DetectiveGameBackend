using Microsoft.AspNetCore.Mvc;
using DetectiveGame.Application.Features.Games.Commands;
using DetectiveGame.Application.Features.Games.Queries;

using MediatR;
using Microsoft.AspNetCore.SignalR;
using DetectiveGame.WebAPI.Hubs;
using FluentValidation;

namespace DetectiveGame.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<GameHub> _hubContext;

        public GamesController(IMediator mediator, IHubContext<GameHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ActionResult<GameDto>> CreateGame(CreateGameCommand command)
        {
            try
            {
                var game = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(Guid id)
        {
            var query = new GetGameByIdQuery { Id = id };
            var game = await _mediator.Send(query);

            if (game == null)
                return NotFound();

            return Ok(game);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetActiveGames()
        {
            var query = new GetActiveGamesQuery();
            var games = await _mediator.Send(query);
            return Ok(games);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult<GameDto>> UpdateGameStatus(Guid id, UpdateGameStatusCommand command)
        {
            if (id != command.GameId)
                return BadRequest();

            try
            {
                var game = await _mediator.Send(command);
                await _hubContext.Clients.Group(id.ToString()).SendAsync("GameStatusUpdated", game);
                return Ok(game);
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
    }
} 