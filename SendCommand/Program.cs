using System;
using TestBus.Commands;

namespace SendCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            DistributedBus.Start();

            do
            {
                Console.WriteLine("Enter message (or quit to exit)");
                Console.Write("> ");
                string value = Console.ReadLine();

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                DistributedBus.Send<IHelloWorld>(new {Name = value, Age = 21}).Wait();
            }

            while (true); DistributedBus.Stop();
        }
    }
}
