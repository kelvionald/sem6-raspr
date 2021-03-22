namespace Library
{
    public interface IMessageBroker
    {
        void Send(string key, string message);
        void Publish(string eventName, string value);
    }
}