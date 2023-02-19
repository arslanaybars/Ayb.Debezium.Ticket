using Ayb.Debezium.Ticket.Api.Events;

namespace Ayb.Debezium.Ticket.Api.Handlers;

public class TicketCreateCommand : IRequest
{
    #region [ Command ]

    public TicketCreateCommand()
    {
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    #endregion

    public class Handler : IRequestHandler<TicketCreateCommand>
    {
        private readonly TicketDbContext _ticketDbContext;

        public Handler(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<Unit> Handle(TicketCreateCommand command, CancellationToken cancellationToken)
        {
            var ticket = new TicketEntity
            {
                Id = Guid.NewGuid(),
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
                PhoneNumber = command.PhoneNumber
            };     
            
            var ticketOutbox = new OutboxEvent
            {
                Id = Guid.NewGuid(),
                AggregateId = ticket.Id,
                AggregateType = nameof(Ticket),
                Type = nameof(TicketCreatedEvent),
                Payload = JsonSerializer.Serialize(new TicketCreatedEvent(ticket))
            };
            
            // may add async with Task.WhenAll()
            await _ticketDbContext.Tickets.AddAsync(ticket, cancellationToken);
            await _ticketDbContext.OutboxEvents.AddAsync(ticketOutbox, cancellationToken);
            
            await _ticketDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}