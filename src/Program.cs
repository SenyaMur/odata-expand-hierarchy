using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace OdataExample {
  public class Program {
    public static void Main(string[] args) {
      Console.WriteLine(Directory.GetCurrentDirectory());
      var Configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        //.SetBasePath(env.ContentRootPath)
        .AddEnvironmentVariables()
        .AddJsonFile("appsettings.json", optional : true, reloadOnChange : true)        
        .Build();
      
        CreateWebHostBuilder(args).Build().Run();      
    }
    /// <summary>
    /// Запуск Веб-службы
    /// </summary>
    /// <param name="args">список аргументов</param>
    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
      .UseStartup<Startup>();
  }
}