using System;
using System.Threading.Tasks;
using Fri.Xhl.Domain.Events.Interfaces;
using MassTransit;

namespace TestBus
{
    public class PostcodeProvidedConsumer : IConsumer<IPostCodeProvidedEvent>
    {
        public async Task Consume(ConsumeContext<IPostCodeProvidedEvent> context)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now.ToShortTimeString()} Received Postcode: {context.Message.PostCode}");
        }
    }
}
