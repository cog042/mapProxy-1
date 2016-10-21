using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var host = new WebHostBuilder()
            .UseKestrel()
            .UseWebRoot("assets")
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .UseStartup<Handler>()
            .Build();

        host.Run();
    }
}

