using System;
using System.Linq;
using MassTransit;

namespace Payments
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://51.158.76.230"), h =>
                {
                    h.Username("admin");
                    h.Password("admin");
                });

                sbc.ReceiveEndpoint(host, "Payments", ep =>
                {
                    ep.Consumer(() => new OrderRequestedConsumer());
                });
            });

            bus.Start();
            
            Console.WriteLine("Welcome to Payments");
            Console.WriteLine("Press Q key to exit");
            while (Console.ReadKey(true).Key != ConsoleKey.Q) ;

            bus.Stop();
        }
    }
}
