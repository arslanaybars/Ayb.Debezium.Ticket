namespace Ayb.Debezium.Ticket.Consumer.Repositories;

public interface ITicketRepository
{
    Task Create(TicketEntity ticket);
    Task<bool> Update(TicketEntity ticket);
    Task<bool> Delete(string id);
}