using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MultiShop.RabbitMQMessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateMessage()
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            var connection = connectionFactory.CreateConnection();

            //channel ile kuyruk tanımlama işlemleri yapılacak
            var channel = connection.CreateModel();

            //Overloads meanings
            //1. queue adı
            //2. rabbitmq restart oldugunda kuyrukta restart olsun mu (false : hayır olmasın)
            //4. kuyruktaki işlemler tamamlandıgında kuyruk silinsin mi (false : hayır silinmesin)
            channel.QueueDeclare("Kuyruk1", false, false, false, arguments: null);

            //gönderilecek mesaj içerigi
            var messageContent = "Merhaba bu bir RabbitMQ kuyruk mesajıdır.";

            //gönderilecek mesaj değişken içeriğinin byte türündeki hali
            var byteMessageContent = Encoding.UTF8.GetBytes(messageContent);

            //mesajı gönderme işlemi
            channel.BasicPublish(exchange:"", routingKey:"Kuyruk1", basicProperties:null,
                body:byteMessageContent);

            return Ok("Mesajınız Kuyruga Alınmıstır.");
        }

        private static string message;

        [HttpGet]
        public IActionResult ReadMessage()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            //mesajı okuma işlemi
            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, x) =>
            {
                var byteMessage = x.Body.ToArray();
                message = Encoding.UTF8.GetString(byteMessage);
            };

            channel.BasicConsume(queue: "Kuyruk1", autoAck:false, consumer:consumer);

            if (string.IsNullOrEmpty(message))
            {
                return NoContent();
            }
            else
            {
                return Ok(message);
            }
        }
    }
}
