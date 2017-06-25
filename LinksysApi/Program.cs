using System;
using System.Linq;

namespace LinksysApi
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                var clients = LinksysApi.GetClients("192.168.1.1", "admin", "admin");

                clients = clients.OrderBy(c => c.Type).ThenBy(c => c.Name);
                foreach (var client in clients)
                {
                    Console.WriteLine("{0, -30}\t{1}\t{2}\t{3}",
                        client.Name, client.IP, client.Expire, client.Type);
                }

                Console.WriteLine();
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
