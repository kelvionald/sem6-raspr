namespace Valudator
{
    public interface IMessageBroker
    {
        void Send(string key, string message);
    }
}