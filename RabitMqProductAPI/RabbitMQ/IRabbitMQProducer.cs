namespace RabitMqProductAPI.RabitMQ
{
    public interface IRabitMQProducer
    {
        public void SendProductMessage<T>(T message);
    }
}