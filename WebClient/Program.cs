using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BusinessLogic.ViewModel;
using BusinessLogic;
using DatabaseImplement.Implements;
using Unity;
using Unity.Lifetime;

namespace WebClient
{
    public class Program
    {
        public static UserViewModel User = null;
        public static AgentViewModel Agent = null;
        public static int ClientId = 0;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args ) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
