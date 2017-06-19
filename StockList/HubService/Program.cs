using System;
using Microsoft.Owin.Hosting;

namespace HubService
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"http://localhost:8088/Nab";
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine($"Server running at {url}");
                Console.ReadLine();
            }
        }
    }
}
