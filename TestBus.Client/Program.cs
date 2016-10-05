using System;
using TestBus.Commands;

namespace TestBus.Client
{
    class Program
    {
        static void Main()
        {
            Console.ForegroundColor = DefaultConsoleColour;

            DistributedBus.Start();

            var quit = false;

            do
            {
                Log("Enter message S <name> for SEND, R <ip address> for REQUEST/RESPONSE, P <email> for PUBLISH,  Q for quit");
                Console.Write("> ");
                var value = Console.ReadLine();
                var cmd = value?.Length > 0 ? value.ToUpper()[0] : ' ';
                var arg = value?.Length > 2 ? value.Substring(2) : "default";

                switch(cmd)
                {
                    case 'S':
                        var command = new HelloWorldCommand{ Name = arg };
                        DistributedBus.Send(command).Wait();
                        break;

                    case 'R':
                        var request = new PingRequest {IPAddress = arg};
                        var reply = DistributedBus.Request<IPing, IPingReply>(request).Result;
                        var output = $"Reply: Client IP Address {reply.ClientIPAddress} Server IP Addess: {reply.ServerIPAddress}";
                        Log(output, ConsoleColor.Cyan);
                        break;

                    case 'P':
                        var @event = new UserRegisteredEvent { EmailAddress = arg};
                        DistributedBus.Publish(@event);
                        break;

                    case 'Q':
                        quit = true;
                        break;

                    default:
                        Log("huh?", ConsoleColor.Red);
                        break;
                }

            } while (!quit);

            DistributedBus.Stop();
        }

        private const ConsoleColor DefaultConsoleColour = ConsoleColor.Green;

        private static void Log(string message, ConsoleColor colour = DefaultConsoleColour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(message);
            Console.ForegroundColor = DefaultConsoleColour;
        }
    }

    public class HelloWorldCommand : IHelloWorld
    {
        public string Name { get; set; }
    }

    public class PingRequest : IPing
    {
        public string IPAddress { get; set; }
    }

    public class UserRegisteredEvent : IUserRegistered
    {
        public string EmailAddress { get; set; }
    }
}
