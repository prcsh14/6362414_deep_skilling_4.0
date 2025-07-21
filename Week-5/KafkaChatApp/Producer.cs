using Confluent.Kafka;
using System;
using System.Threading.Tasks;

class Producer
{
    public static async Task Main(string[] args)
    {
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        using var producer = new ProducerBuilder<Null, string>(config).Build();

        Console.WriteLine("Enter chat messages (type 'exit' to quit):");
        string message;
        do
        {
            message = Console.ReadLine();
            await producer.ProduceAsync("chat-topic", new Message<Null, string> { Value = message });
        } while (message != "exit");
    }
}
