using Microsoft.Extensions.DependencyInjection;
using Rotina.Domain;
using Rotina.Domain.Contracts;
using Rotina.Service.Services;

namespace Rotina.IoC
{
    public class StartupIoC
    {
        public static void Config(IServiceCollection services)
        {   
            services.AddScoped<IFilaService, FilaService>();
            services.AddScoped<IDadosMoeda, DadosMoeda>();
            services.AddScoped<IDadosCotacao, DadosCotacao>();
        }
    }
}
