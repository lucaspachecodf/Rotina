using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rotina.Domain.Contracts;
using Rotina.Service.Services;

namespace Rotina.IoC
{
    public class StartupIoC
    {
        public static void Config(IServiceCollection services, IConfiguration configuration)
        {   
            services.AddScoped<IFilaService, FilaService>();
        }
    }
}
