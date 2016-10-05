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

        public static async Task Send<T>(T command)
            where T : class
        {
            var address = new Uri("rabbitmq://localhost/test_bus");
            var consumerEndpoint = await _bus.GetSendEndpoint(address);
            await consumerEndpoint.Send<T>(command);
        }

        public static async Task<TReply> Request<TRequest, TReply>(TRequest request)
            where TRequest: class
            where TReply: class
        {
            var address = new Uri("rabbitmq://localhost/test_bus");
            var timeout = TimeSpan.FromSeconds(10);
            var client = _bus.CreateRequestClient<TRequest, TReply>(address, timeout);

            return await client.Request(request);
        }
    }
}