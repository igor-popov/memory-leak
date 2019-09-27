﻿using System;
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
