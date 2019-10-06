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
            //await Scenarios.LoadTestDynamicAssemblyOnOwin();
            await Scenarios.LoadTestLargeObjectsHeapOnOwin();
            //await Scenarios.LoadTestDynamicAssembly();
            //await Scenarios.LoadTestLargeObjectsHeap();

            Console.WriteLine("Completed!");
        }

    }
}
