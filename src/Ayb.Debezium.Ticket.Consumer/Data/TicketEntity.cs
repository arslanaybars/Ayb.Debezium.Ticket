using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ayb.Debezium.Ticket.Consumer.Data;

public class TicketEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string TicketId { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

}