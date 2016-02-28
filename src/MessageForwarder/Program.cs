using System.Text;
using CommandLine;
using EasyNetQ;
using EasyNetQ.Topology;
using NLog;

namespace MessageForwarder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();

            var arguments = new Arguments();
            if (!Parser.Default.ParseArguments(args, arguments))
                return;

            var connectionString = $"host={arguments.Host};port={arguments.Port};username={arguments.UserName};password={arguments.Password}";
            var advancedBus = RabbitHutch.CreateBus(connectionString).Advanced;
            var exchange = advancedBus.ExchangeDeclare(arguments.ToExchange, ExchangeType.Direct, true);
            var queue = advancedBus.QueueDeclare(arguments.FromQueue, true);

            advancedBus.Consume(queue, async (body, properties, info) =>
            {
                logger.Info(Encoding.UTF8.GetString(body));

                await advancedBus.PublishAsync(exchange, arguments.PublisherRoutingKey, false, new MessageProperties(), body);
            });
        }
    }
}
