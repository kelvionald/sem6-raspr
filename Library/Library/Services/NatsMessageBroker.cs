using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NATS.Client;

namespace Library
{
    public class NatsMessageBroker : IMessageBroker
    {
        private async Task ProduceAsync(string key, string message)
        {
            ConnectionFactory cf = new ConnectionFactory();
            using (IConnection c = cf.CreateConnection())
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                c.Publish(key, data);
                c.Drain();
                c.Close();
            }
        }
        public void Send(string key, string message)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task.Factory.StartNew(() => ProduceAsync(key, message), cts.Token);
        }

        public void Publish(string eventName, string value)
        {
            ConnectionFactory cf = new ConnectionFactory();
            using (IConnection c = cf.CreateConnection())
            {
                c.Publish(eventName, Encoding.UTF8.GetBytes(value));

                c.Drain();
                c.Close();
            }
        }
    }
}