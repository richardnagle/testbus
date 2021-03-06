﻿using System;
using MassTransit;

namespace TestBus.Server
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            var bus = Bus.Factory.CreateUsingRabbitMq(x =>
            {
                var host = x.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                x.ReceiveEndpoint(host, "test_bus", endpoint =>
                {
                    endpoint.Consumer<PostcodeProvidedConsumer>();
                    endpoint.Consumer<HelloWorldConsumer>();
                    endpoint.Consumer<PingConsumer>();
                    endpoint.Consumer<UserRegisteredConsumer>();
                });
            });

            bus.Start();

        }
    }
}
