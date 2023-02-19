namespace Ayb.Debezium.Ticket.Consumer.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly ITicketMongoContext _context;

    public TicketRepository(ITicketMongoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Create(TicketEntity ticket)
    {
        await _context.Tickets.InsertOneAsync(ticket);
    }

    public async Task<bool> Update(TicketEntity ticket)
    {
        var existingTicket = await _context
            .Tickets
            .Find(p => p.TicketId == ticket.TicketId)
            .FirstOrDefaultAsync();

        if (existingTicket == null)
        {
            throw new ArgumentNullException($"{ticket.TicketId}");
        }

        ticket.Id = existingTicket.Id;
        var updateResult = await _context
            .Tickets
            .ReplaceOneAsync(filter: g => g.Id == ticket.Id, replacement: ticket);
        
        return updateResult.IsAcknowledged
               && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id)
    {
        FilterDefinition<TicketEntity> filter = Builders<TicketEntity>.Filter.Eq(p => p.TicketId, id);

        DeleteResult deleteResult = await _context
            .Tickets
            .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged
               && deleteResult.DeletedCount > 0;
    }
}