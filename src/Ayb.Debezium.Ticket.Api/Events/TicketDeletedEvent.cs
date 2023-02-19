namespace Ayb.Debezium.Ticket.Api.Events;

public record TicketDeletedEvent : TicketEventBase
{
    public override string Type => nameof(TicketDeletedEvent);

    public TicketDeletedEvent(TicketEntity ticketEntity) : base(ticketEntity)
    {
    }
}