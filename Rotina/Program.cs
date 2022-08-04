using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rotina.Domain.Contracts;
using Rotina.IoC;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Rotina
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();

                var dadosMoeda = host.Services.GetService<IDadosMoeda>();
                await dadosMoeda.Iniciar();

                Console.WriteLine("Finalizando");
            }
            catch (Exception ex)
            {

            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddJsonFile("appsettings.json", optional: true);
                })
                .ConfigureServices((context, services) =>
                {
                    StartupIoC.Config(services);
                });

            return hostBuilder;
        }
    }
}
