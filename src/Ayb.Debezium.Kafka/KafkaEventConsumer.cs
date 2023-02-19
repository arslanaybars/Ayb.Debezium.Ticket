namespace Ayb.Debezium.Kafka;

public class KafkaEventConsumer<TK, TV> : BackgroundService
{
    private readonly KafkaConsumerConfig _config;
    private readonly ILogger<KafkaEventConsumer<TK, TV>> _logger;
    private readonly IConsumer<TK, TV> _consumer;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public KafkaEventConsumer(ILogger<KafkaEventConsumer<TK, TV>> logger
        , IOptions<KafkaConsumerConfig> config
        , IServiceScopeFactory serviceScopeFactory
        , KafkaConsumerOptions kafkaConsumerOptions)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _config = config.Value;
        _consumer = new ConsumerBuilder<TK, TV>(config.Value)
            .SetValueDeserializer(new KafkaDeserializer<TV>(kafkaConsumerOptions))
            .Build();
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Subscribing");
        _consumer.Subscribe(_config.Topic);
        
        try
        {
            using var scope = _serviceScopeFactory.CreateScope();
            _logger.LogInformation("Waiting for message...");

            while (!cancellationToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(cancellationToken);

                if (result.Message != null)
                {
                    var handlerType = typeof(IKafkaHandler<,>).MakeGenericType(typeof(TK), result.Message.Value.GetType());
                    var handler = scope.ServiceProvider.GetRequiredService(handlerType);
                    
                    await ((dynamic) handler.GetType().GetMethod("HandleAsync")!
                        .Invoke(handler, new object[]{result.Message.Key, result.Message.Value}))!;

                    _consumer.Commit(result);
                }
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogError($"Shutting down gracefully.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Shutting down gracefully. {@Ex}", ex);
        }
    }
}