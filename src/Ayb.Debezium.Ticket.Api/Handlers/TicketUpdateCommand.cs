using Ayb.Debezium.Ticket.Api.Events;

namespace Ayb.Debezium.Ticket.Api.Handlers;

public class TicketUpdateCommand : IRequest
{
    #region [ Command ]
    
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    #endregion

    public class Handler : IRequestHandler<TicketUpdateCommand>
    {
        private readonly TicketDbContext _ticketDbContext;

        public Handler(TicketDbContext ticketDbContext)
        {
            _ticketDbContext = ticketDbContext;
        }

        public async Task<Unit> Handle(TicketUpdateCommand command, CancellationToken cancellationToken)
        {
            var ticket = await _ticketDbContext.Tickets.SingleOrDefaultAsync(x => x.Id == command.Id, cancellationToken: cancellationToken);
            if (ticket == null)
                throw new ArgumentNullException($"Ticket is not found. TicketId : {command.Id}");
            
            ticket.Email = command.Email;
            ticket.FirstName = command.FirstName;
            ticket.LastName = command.LastName;
            ticket.PhoneNumber = command.PhoneNumber;
            
            var ticketOutbox = new OutboxEvent
            {
                Id = Guid.NewGuid(),
                AggregateId = ticket.Id,
                AggregateType = nameof(Ticket),
                Type = nameof(TicketUpdatedEvent),
                Payload = JsonSerializer.Serialize(new TicketUpdatedEvent(ticket))
            };
            
            _ticketDbContext.Tickets.Update(ticket);
            await _ticketDbContext.OutboxEvents.AddAsync(ticketOutbox, cancellationToken);
            
            await _ticketDbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}