using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace OrderAPI.BackgroundService
{
    public class SendMailService : IHostedService
    {
        private Timer timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(SendQueue, null,TimeSpan.Zero,TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }
        public void SendQueue(object state)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://pflzbbfg:FaB5MCEKfx1SdwsJsNQUzds7k4eAlmGK@hawk.rmq.cloudamqp.com/pflzbbfg");
            using var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("sendTask", true, false, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("sendTask", true, consumer);
            consumer.Received += (obj, e) =>
            {
                var result = Encoding.UTF8.GetString(e.Body.ToArray());
                SendMail(result);
            };
        }
      

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public void SendMail(string mail)
        {
            var from = "";
            var to = mail;
            var subject = "test mail";
            var body = "test body";
            var userName = "username";
            var password = "password";
            var host = "";
            var port = 0;
            var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(userName, password),
                EnableSsl = true
            };
            client.Send(from,to,subject,body);
        }
    }
}
