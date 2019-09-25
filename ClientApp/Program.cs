using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace ClientApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            for (int i = 0; i < 100000; i++)
            {
                string response = await httpClient.GetStringAsync(@"http://localhost/Events.Server/api/ping");
                Console.WriteLine(response);
            }
            
            Console.WriteLine("Completed!");
        }
    }
}
