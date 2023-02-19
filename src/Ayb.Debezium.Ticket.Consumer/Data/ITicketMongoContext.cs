namespace Ayb.Debezium.Ticket.Consumer.Data;

public interface ITicketMongoContext
{
    IMongoCollection<TicketEntity> Tickets { get; }
}