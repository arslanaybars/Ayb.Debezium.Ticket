namespace Ayb.Debezium.Kafka;

public class KafkaConsumerConfig : ConsumerConfig
{
    public string Topic { get; set; }
    
    // Todo: reads configs from settings
    public KafkaConsumerConfig()
    {
        AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest;
        EnableAutoOffsetStore = true;
        EnablePartitionEof = true;
        EnableAutoCommit = true;
        Topic = "Ticket.events";
        GroupId = "ticket_events_group";
        BootstrapServers = "localhost:29092";
    }
}