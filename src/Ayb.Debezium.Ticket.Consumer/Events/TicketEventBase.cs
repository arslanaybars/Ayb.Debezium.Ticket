namespace Ayb.Debezium.Ticket.Consumer.Events;

public abstract class TicketEventBase
{
    public TicketEventBase(string id, string  email, string firstName, string lastName)
    {
        Id = id;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    public string Id { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public abstract string Type { get; }
}