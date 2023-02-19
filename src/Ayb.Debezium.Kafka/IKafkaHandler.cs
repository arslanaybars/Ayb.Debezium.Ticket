namespace Ayb.Debezium.Kafka
{
    public interface IKafkaHandler<TK, TV>
    {
        Task HandleAsync(TK key, TV value);
    }
}