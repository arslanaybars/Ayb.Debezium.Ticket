namespace Ayb.Debezium.Kafka
{
    internal sealed class KafkaDeserializer<T> : IDeserializer<T>
    {
        private readonly KafkaConsumerOptions _kafkaConsumerOptions;

        public KafkaDeserializer(KafkaConsumerOptions kafkaConsumerOptions)
        {
            _kafkaConsumerOptions = kafkaConsumerOptions;
        }

        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            var serializedString = Encoding.UTF8.GetString(data);
            var eventType = Encoding.UTF8.GetString(context.Headers.GetLastBytes("eventType"));

            var type = _kafkaConsumerOptions.Events.FirstOrDefault(x => x.Name == eventType);

            if (type == null)
            {
                throw new ArgumentNullException($"{eventType}");
            }
            
            return (T)JsonConvert.DeserializeObject(serializedString, type);
        }
    }
}