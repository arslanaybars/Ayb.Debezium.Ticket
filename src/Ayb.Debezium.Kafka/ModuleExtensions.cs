namespace Ayb.Debezium.Kafka;

public static class ModuleExtensions
{
    public static IServiceCollection AddKafkaConsumer(this IServiceCollection services, Action<KafkaConsumerOptions> opt )
    {
        var options = new KafkaConsumerOptions(services);
        opt.Invoke(options);

        services.AddSingleton(options);
        
        return services;
    }
}