using System;
using Fri.Xhl.Domain.Events.Interfaces;
using MassTransit;

namespace TestBus
{
    class Program
    {
        static void Main()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                x.ReceiveEndpoint(host, "test_bus", endpoint =>
                {
                    endpoint.Handler<IPostCodeProvidedEvent>(async context =>
                    {
                        await Console.Out.WriteLineAsync($"{DateTime.Now.ToShortTimeString()} Received Postcode: {context.Message.PostCode}");
                    });
                });
            });

            bus.Start();

        }
    }
}
