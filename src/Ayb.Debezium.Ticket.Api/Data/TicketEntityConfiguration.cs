namespace Ayb.Debezium.Ticket.Api.Data;

public class TicketEntityConfiguration : IEntityTypeConfiguration<TicketEntity>
{
    public void Configure(EntityTypeBuilder<TicketEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName);
        builder.Property(e => e.LastName);
        builder.Property(e => e.Email);
        builder.Property(e => e.PhoneNumber);
    }
}