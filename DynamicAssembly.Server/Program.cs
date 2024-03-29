﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DynamicAssembly.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("https://localhost:5010")
                .UseStartup<Startup>();
    }
}
