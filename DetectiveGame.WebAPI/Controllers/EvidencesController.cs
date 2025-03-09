using Microsoft.AspNetCore.Mvc;
using DetectiveGame.Application.Features.Evidences.Commands;
using DetectiveGame.Application.Features.Evidences.Queries;

using DetectiveGame.WebAPI.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;


namespace DetectiveGame.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvidencesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IHubContext<GameHub> _hubContext;

        public EvidencesController(IMediator mediator, IHubContext<GameHub> hubContext)
        {
            _mediator = mediator;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<ActionResult<EvidenceDto>> AddEvidence(AddEvidenceCommand command)
        {
            try
            {
                var evidence = await _mediator.Send(command);
                await _hubContext.Clients.Group(command.GameId.ToString())
                    .SendAsync("EvidenceAdded", evidence);
                return CreatedAtAction(nameof(GetEvidence), new { id = evidence.Id }, evidence);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EvidenceDto>> GetEvidence(Guid id)
        {
            var query = new GetEvidenceByIdQuery { Id = id };
            var evidence = await _mediator.Send(query);

            if (evidence == null)
                return NotFound();

            return Ok(evidence);
        }

        [HttpGet("game/{gameId}")]
        public async Task<ActionResult<IEnumerable<EvidenceDto>>> GetEvidencesByGame(Guid gameId)
        {
            var query = new GetEvidencesByGameQuery { GameId = gameId };
            var evidences = await _mediator.Send(query);
            return Ok(evidences);
        }
    }
} 