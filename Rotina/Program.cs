using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rotina.Domain.Contracts;
using Rotina.Domain.Helpers;
using Rotina.IoC;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Rotina
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Iniciando a Rotina");
                var host = CreateHostBuilder(args).Build();

                var watch = System.Diagnostics.Stopwatch.StartNew();
                var dadosMoeda = host.Services.GetService<IDadosMoeda>();

                await dadosMoeda.Iniciar();

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;

                Log.LoggerRetorno.Info($"Tempo de Execução: {TimeSpan.FromMilliseconds(elapsedMs).TotalSeconds } segundos");
                Console.WriteLine("Finalizando");
            }
            catch (Exception ex)
            {
                Log.LoggerError.Error(ex.Message);
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

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
