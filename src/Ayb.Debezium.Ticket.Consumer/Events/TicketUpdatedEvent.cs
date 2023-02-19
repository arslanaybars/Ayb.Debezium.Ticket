namespace Ayb.Debezium.Ticket.Consumer.Events;

public class TicketUpdatedEvent : TicketEventBase
{
    public override string Type => nameof(TicketUpdatedEvent);

    public TicketUpdatedEvent(string id, string email, string firstName, string lastName) : base(id, email, firstName, lastName)
    {
    }
    
    public class Handler : IKafkaHandler<string,TicketUpdatedEvent>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(ITicketRepository ticketRepository, ILogger<Handler> logger)
        {
            _ticketRepository = ticketRepository;
            _logger = logger;
        }

        public async Task HandleAsync(string key, TicketUpdatedEvent @event)
        {
            _logger.LogInformation("Event of type {EventType} received by the consumer application", @event.Type);

            await _ticketRepository.Update(new TicketEntity
            {
                TicketId = @event.Id,
                Email = @event.Email,
                FirstName = @event.FirstName,
                LastName = @event.LastName
            });
        }
    }
}