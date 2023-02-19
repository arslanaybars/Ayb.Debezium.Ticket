namespace Ayb.Debezium.Ticket.Api.Events;

public abstract record TicketEventBase
{
    public TicketEventBase(TicketEntity ticketEntity)
    {
        Id = ticketEntity.Id;
        Email = ticketEntity.Email;
        FirstName = ticketEntity.FirstName;
        LastName = ticketEntity.LastName;
    }

    public Guid Id { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public abstract string Type { get; }
}