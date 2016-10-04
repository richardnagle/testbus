using System;
using System.Threading.Tasks;
using MassTransit;

namespace SendCommand
{
    public static class DistributedBus
    {
        private static readonly IBusControl _bus;

        static DistributedBus()
        {
            _bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                x.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        }

        public static void Stop()
        {
            _bus.Stop();
        }

        public static void Start()
        {
            _bus.Start();
        }

        public static async Task Send<T>(object command)
            where T : class
        {
            var address = new Uri("rabbitmq://localhost/test_bus");
            var consumerEndpoint = await _bus.GetSendEndpoint(address);
            await consumerEndpoint.Send<T>(command);
        }
    }
}