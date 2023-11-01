using Rabbit.Models.Entities;
using Rabbit.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Text.Json;

namespace Rabbit.Repositories
{
    public class RabbitMessageRepository : IRabbitMessageRepository
    {
        public void SendMessage(RabbitMessage message)
        {
            var factory = new ConnectionFactory { HostName = "localhost", UserName = "admin", Password = "123456" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "messages",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            string json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: "messages",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
