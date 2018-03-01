using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace diary
{
    public class Program
    {
        public static readonly string WEB_PORT = "8000";
        public static readonly string WEBAPI_PORT = "8080";

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:" + WEB_PORT, "http://*:" + WEBAPI_PORT)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
