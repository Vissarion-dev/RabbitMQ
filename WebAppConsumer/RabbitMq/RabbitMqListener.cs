using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace WebAppConsumer.RabbitMq
{
    public class RabbitMqListener : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IConfiguration conf;
        public RabbitMqListener(IConfiguration iConf)
        {
            conf = iConf;
            var factory = new ConnectionFactory { HostName = conf["RabbitMq:Host"] };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: conf["RabbitMq:Queue"], durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                Debug.WriteLine($"Receive message: {content}");
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume("MyQueue", false, consumer);
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
