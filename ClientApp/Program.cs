using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace ClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Scenarios.LoadTestEvents();

            Console.WriteLine("Completed!");
        }

    }
}
