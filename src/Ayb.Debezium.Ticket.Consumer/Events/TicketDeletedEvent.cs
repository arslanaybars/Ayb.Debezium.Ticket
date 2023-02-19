namespace Ayb.Debezium.Ticket.Consumer.Events;

public class TicketDeletedEvent : TicketEventBase
{
    public override string Type => nameof(TicketDeletedEvent);

    public TicketDeletedEvent(string id, string email, string firstName, string lastName) : base(id, email, firstName,
        lastName)
    {
    }

    public class Handler : IKafkaHandler<string, TicketDeletedEvent>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(ITicketRepository ticketRepository, ILogger<Handler> logger)
        {
            _ticketRepository = ticketRepository;
            _logger = logger;
        }

        public async Task HandleAsync(string key, TicketDeletedEvent @event)
        {
            _logger.LogInformation("Event of type {EventType} received by the consumer application", @event.Type);

            await _ticketRepository.Delete(@event.Id);
        }
    }
}