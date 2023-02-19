namespace Ayb.Debezium.Ticket.Api.Events;

public record TicketCreatedEvent : TicketEventBase
{
    public override string Type => nameof(TicketCreatedEvent);

    public TicketCreatedEvent(TicketEntity ticketEntity) : base(ticketEntity)
    {
    }
}