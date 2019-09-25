using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace ClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await LoadTestServer(@"http://localhost/Events.Server/api/ping", 30000);

            Console.WriteLine("Completed!");
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
