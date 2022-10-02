namespace WebAppProducer.RabbitMq
{
    using RabbitMQ.Client;
    using System.Text;
    using System.Text.Json;

    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConfiguration conf;
        public RabbitMqService(IConfiguration iConf)
        {
            conf = iConf;
        }
        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = conf["RabbitMq:Host"] };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: conf["RabbitMq:Queue"],
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                               routingKey: "MyQueue",
                               basicProperties: null,
                               body: body);
            }
        }
    }
}
