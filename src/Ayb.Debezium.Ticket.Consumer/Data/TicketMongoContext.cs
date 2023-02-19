namespace Ayb.Debezium.Ticket.Consumer.Data;

public class TicketMongoContext : ITicketMongoContext
{
    public TicketMongoContext(IConfiguration configuration)
    {
        var client = new MongoClient( configuration["MongoDb:ConnectionString"]);
        var database = client.GetDatabase( configuration["MongoDb:DatabaseName"]);
        
        Tickets = database.GetCollection<TicketEntity>(configuration["MongoDb:CollectionName"]);
    }
    
    public IMongoCollection<TicketEntity> Tickets { get; }
}