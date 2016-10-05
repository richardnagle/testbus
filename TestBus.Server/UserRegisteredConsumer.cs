using System;
using System.Threading.Tasks;
using MassTransit;
using TestBus.Commands;

namespace TestBus.Server
{
    public class UserRegisteredConsumer: IConsumer<IUserRegistered>
    {
        public async Task Consume(ConsumeContext<IUserRegistered> context)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now.ToShortTimeString()} New registration for {context.Message.EmailAddress}");
        }
    }
}
