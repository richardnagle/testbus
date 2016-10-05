using System;
using MassTransit;

namespace TestBus.EventEndpoint
{

    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                x.ReceiveEndpoint(host, "test_bus_events", endpoint =>
                {
                    endpoint.Consumer<UserRegisteredConsumer>();
                });
            });

            bus.Start();

        }
    }
}
