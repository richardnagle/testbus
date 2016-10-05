using System;
using System.Threading.Tasks;
using MassTransit;
using TestBus.Commands;

namespace TestBus
{
    public class PingConsumer: IConsumer<IPing>
    {
        public async Task Consume(ConsumeContext<IPing> context)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now.ToShortTimeString()} I was pinged from {context.Message.IPAddress}");

            context.Respond<IPingReply>(new PingReply(context.Message));
        }

        private class PingReply : IPingReply
        {
            public PingReply(IPing request)
            {
                ClientIPAddress = request.IPAddress;
            }

            public string ServerIPAddress => "127.0.0.1";
            public string ClientIPAddress { get; }
        }
    }
}
