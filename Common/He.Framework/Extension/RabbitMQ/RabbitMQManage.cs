using He.Common.Extension;
using RabbitMQ.Client;
using System.Text;

namespace He.Framework.Extension.RabbitMQ
{
    /// <summary>
    /// RabbitMQ 管理类
    /// </summary>
    public class RabbitMQManage : IRabbitMQManage, IDisposable
    {
        private readonly IConnection connection;
        private readonly IModel channel;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="factory"></param>
        public RabbitMQManage(ConnectionFactory factory)
        {
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        /// <summary>
        /// 发送消息到指定的队列
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="messageBody"></param>
        public void SendMessage(string queueName, object messageBody) => SendMessage(queueName, messageBody.ToJson());

        /// <summary>
        /// 发送消息到指定的队列
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="messageBody"></param>
        public void SendMessage(string queueName, string messageBody) => SendMessage(queueName, Encoding.UTF8.GetBytes(messageBody));

        /// <summary>
        /// 发送消息到指定的队列
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="messageBody"></param>
        public void SendMessage(string queueName, byte[] messageBody)
        {
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: messageBody);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            channel.Close();
            connection.Close();
        }
    }
}
