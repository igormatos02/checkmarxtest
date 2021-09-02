using crosscutting.checkmarx;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace services.checkmarxs
{
    // RabbitMQService.cs
    public class RabbitMQService : IRabbitMQService
    {
        protected readonly ConnectionFactory _factory;
        protected readonly IConnection _connection;
        protected readonly IModel _channel;

        protected readonly IServiceProvider _serviceProvider;

        public RabbitMQService(IServiceProvider serviceProvider)
        {
            // Opens the connections to RabbitMQ
            _factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            _serviceProvider = serviceProvider;
        }

        public virtual void Connect(string queue,string method)
        {
            // Declare a RabbitMQ Queue
            _channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(_channel);

            // When we receive a message from SignalR
            consumer.Received += delegate (object model, BasicDeliverEventArgs ea) {
                // Get the ChatHub from SignalR (using DI)
                var queueHub = (IHubContext<QueueHub>)_serviceProvider.GetService(typeof(IHubContext<QueueHub>));
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                // Send message to all users in SignalR
                queueHub.Clients.All.SendAsync(method, message);

            };

            // Consume a RabbitMQ Queue
            _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
        }

        public void Send(string message,string queue)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queue,
                                     basicProperties: null,
                                     body: body);
                
            }

        

        }
    }
}


