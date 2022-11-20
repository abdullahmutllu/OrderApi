using RabbitMQ.Client;

namespace OrderAPI.RaabitMQ
{
    public class SetQueues
    {
        public static void SendQueue(byte[] datas)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://pflzbbfg:FaB5MCEKfx1SdwsJsNQUzds7k4eAlmGK@hawk.rmq.cloudamqp.com/pflzbbfg");
            using  var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare("sendTask",true,false,false);
            channel.BasicPublish(String.Empty,"sendTask",null,datas);

        }
    }
}
