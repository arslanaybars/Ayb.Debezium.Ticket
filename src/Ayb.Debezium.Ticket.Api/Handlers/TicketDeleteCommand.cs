using Ayb.Debezium.Ticket.Api.Events;

namespace Ayb.Debezium.Ticket.Api.Handlers;

public class TicketDeleteCommand: IRequest
{
    #region [ Command ]

    public TicketDeleteCommand(Guid ticketId)
    {
        Id = ticketId;
    }
    
    public Guid Id { get; }
    
    #endregion

    public class Handler : IRequestHandler<TicketDeleteCommand>
    {
        private readonly TicketDbContext _ticketDbContext;

        public Handler(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<Unit> Handle(TicketDeleteCommand command, CancellationToken cancellationToken)
        {
            var ticket = await _ticketDbContext.Tickets.SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);
            if (ticket == null)
                throw new ArgumentNullException($"Ticket is not found. TicketId : {command.Id}");
            
            var ticketOutbox = new OutboxEvent
            {
                Id = Guid.NewGuid(),
                AggregateId = ticket.Id,
                AggregateType = nameof(Ticket),
                Type = nameof(TicketDeletedEvent),
                Payload = JsonSerializer.Serialize(new TicketDeletedEvent(ticket))
            };
            
            _ticketDbContext.Tickets.Remove(ticket);
            await _ticketDbContext.OutboxEvents.AddAsync(ticketOutbox, cancellationToken);
            
            await _ticketDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}