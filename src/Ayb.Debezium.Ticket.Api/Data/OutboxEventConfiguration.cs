
namespace Ayb.Debezium.Ticket.Api.Data;

public class OutboxEventConfiguration : IEntityTypeConfiguration<OutboxEvent>
{
    public void Configure(EntityTypeBuilder<OutboxEvent> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.AggregateId);
        builder.Property(e => e.AggregateType);
        builder.Property(e => e.Type);
        builder.Property(e => e.Payload);
    }
}