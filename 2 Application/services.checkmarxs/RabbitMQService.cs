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

        public virtual void Connect()
        {
            // Declare a RabbitMQ Queue
            _channel.QueueDeclare(queue: AppConstants.ORDER_QUEUE, durable: false, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(_channel);

            // When we receive a message from SignalR
            consumer.Received += delegate (object model, BasicDeliverEventArgs ea) {
                // Get the ChatHub from SignalR (using DI)
                var queueHub = (IHubContext<QueueHub>)_serviceProvider.GetService(typeof(IHubContext<QueueHub>));
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                // Send message to all users in SignalR
                queueHub.Clients.All.SendAsync("orderReceived", "You Have a New Order On the to Be Prepared on the Queue");

            };

            // Consume a RabbitMQ Queue
            _channel.BasicConsume(queue: AppConstants.ORDER_QUEUE, autoAck: true, consumer: consumer);
        }

        public string Receive(string queue)
        {
            string message = string.Empty;
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {

                channel.QueueDeclare(queue: queue,
                                durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                // Fair dispatch: prefetchCount: 1
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var channel1 = channel;
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    message = Encoding.UTF8.GetString(body);


                    var dots = message.Split('.').Length - 1;
                   

                    // Message acknowledgment
                    //channel1.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };

                // Message acknowledgment: autoAck: false
                channel.BasicConsume(queue: queue,
                    autoAck: false,
                    consumer: consumer);

            }
            return message;
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


