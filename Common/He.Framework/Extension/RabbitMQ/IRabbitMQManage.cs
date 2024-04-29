namespace He.Framework.Extension.RabbitMQ
{
    /// <summary>
    /// RabbitMQ
    /// </summary>
    public interface IRabbitMQManage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="messageBody"></param>
        void SendMessage(string queueName, byte[] messageBody);
    }
}
