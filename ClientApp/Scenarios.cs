using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp
{
    public static class Scenarios
    {
        public static async Task LoadTestEvents()
        {
            await LoadTestServer(@"http://localhost/Events.Server/api/ping", 30000);
        }

        public static async Task LoadTestLargeObjectsHeapOnOwin()
        {
            await LoadTestServer(@"http://localhost/Events.Server/api/heavy?very=true", 1000);
        }

        public static async Task LoadTestDynamicAssemblyOnOwin()
        {
            await LoadTestServer(@"http://localhost/Events.Server/api/microservice", 5000);
        }

        public static async Task LoadTestLargeObjectsHeap()
        {
            await LoadTestServer(@"https://localhost:5005/api/heavy?very=true", 1000);
        }

        public static async Task LoadTestDynamicAssembly()
        {
            await LoadTestServer(@"https://localhost:5010/api/microservice", 10000);
        }

        private static async Task LoadTestServer(string url, int count)
        {
            var httpClient = new HttpClient();
            for (int i = 0; i < count; i++)
            {
                string response = await httpClient.GetStringAsync(url);
                Console.WriteLine($"{i} - {response}");
            }
        }
    }
}
