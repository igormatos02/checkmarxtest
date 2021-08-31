using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

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

        public virtual void Connect()
        {
            // Declare a RabbitMQ Queue
            _channel.QueueDeclare(queue: "TestQueue", durable: true, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(_channel);

            // When we receive a message from SignalR
            consumer.Received += delegate (object model, BasicDeliverEventArgs ea) {
                // Get the ChatHub from SignalR (using DI)
                var queueHub = (IHubContext<QueueHub>)_serviceProvider.GetService(typeof(IHubContext<QueueHub>));

                // Send message to all users in SignalR
                queueHub.Clients.All.SendAsync("messageReceived", "You have received a message");

            };

            // Consume a RabbitMQ Queue
            _channel.BasicConsume(queue: "TestQueue", autoAck: true, consumer: consumer);
        }

        public string Receive(string queue)
        {
            return "MEssage Received!";
        }

        public void Send(string message,string queue)
        {
            //var factory = new ConnectionFactory() { HostName = "localhost" };
            //using (var connection = factory.CreateConnection())
            //using (var channel = connection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: "newOrders",
            //                         durable: false,
            //                         exclusive: false,
            //                         autoDelete: false,
            //                         arguments: null);

            //    var body = Encoding.UTF8.GetBytes(message);

            //    channel.BasicPublish(exchange: "",
            //                         routingKey: "newOrders",
            //                         basicProperties: null,
            //                         body: body);
            //    Console.WriteLine(" [x] Sent {0}", message);
            //}

            //Console.WriteLine(" Press [enter] to exit.");
            //Console.ReadLine();

        }
    }
}


