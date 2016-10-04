using System;
using System.Threading.Tasks;
using MassTransit;
using TestBus.Commands;

namespace TestBus
{
    public class HelloWorldConsumer : IConsumer<IHelloWorld>
    {
        public async Task Consume(ConsumeContext<IHelloWorld> context)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now.ToShortTimeString()} Hello {context.Message.Name}. Good to see you.");
        }
    }
}