using Confluent.Kafka;
using System;
using System.Threading;

class Consumer
{
    public static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "chat-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("chat-topic");

        Console.WriteLine("Listening for messages. Press Ctrl+C to exit.");

        try
        {
            while (true)
            {
                var result = consumer.Consume(CancellationToken.None);
                Console.WriteLine($"Received: {result.Message.Value}");
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }
    }
}
