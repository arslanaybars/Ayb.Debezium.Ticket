using Ayb.Debezium.Ticket.Api.Handlers;

namespace Ayb.Debezium.Ticket.Api.Controllers;

[Route("api/ticket")]
public class TicketController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult> Ticket([FromBody] TicketCreateCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPut("{ticketId}")]
    public async Task<ActionResult> Ticket(Guid ticketId, [FromBody] TicketUpdateCommand command)
    {
        command.Id = ticketId;
        return Ok(await _mediator.Send(command));
    }

    [HttpDelete("{ticketId}")]
    public async Task<ActionResult> Ticket(Guid ticketId)
    {
        return Ok(await _mediator.Send(new TicketDeleteCommand(ticketId)));
    }
}