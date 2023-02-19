namespace Ayb.Debezium.Ticket.Api.Events;

public record TicketUpdatedEvent : TicketEventBase
{
    public override string Type => nameof(TicketUpdatedEvent);

    public TicketUpdatedEvent(TicketEntity ticketEntity) : base(ticketEntity)
    {
    }
}