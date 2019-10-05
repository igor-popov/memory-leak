using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace ClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await Scenarios.LoadTestEvents();
            await Scenarios.LoadTestDynamicAssembly();

            Console.WriteLine("Completed!");
        }

    }
}
