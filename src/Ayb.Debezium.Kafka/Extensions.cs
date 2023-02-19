namespace Ayb.Debezium.Kafka;

public class KafkaConsumerOptions
{
    private readonly IServiceCollection _services;

    public KafkaConsumerOptions(IServiceCollection services)
    {
        _services = services;
        Events = new();

    }

    public KafkaConsumerOptions AddEvent<TK, TV, THandler>() where THandler : class, IKafkaHandler<TK, TV>
    {
        Events.Add(typeof(TV));
        
        _services.AddScoped<IKafkaHandler<TK, TV>, THandler>();

        return this;
    }

    public List<Type> Events { get; }
}