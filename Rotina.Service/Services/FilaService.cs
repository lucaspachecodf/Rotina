using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rotina.Domain.Contracts;
using Rotina.Domain.Dtos;
using Rotina.Service.Config;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Serialization;

namespace Rotina.Service.Services
{
    public class FilaService : IFilaService
    {
        private readonly IConfiguration _config;
        public FilaService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<MoedaDTO> GetItemFila()
        {
            var host = $"{UtilConfig.GetUri(_config)}/GetItemFila";            
            using (var client = new HttpClient())
            {
                try
                {
                    var result = await client.GetAsync(host);
                    if (result.IsSuccessStatusCode)
                    {
                        var contentResult = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                        var settings = new JsonSerializerSettings
                        {
                            ContractResolver = new DefaultContractResolver()
                            {
                                NamingStrategy = new SnakeCaseNamingStrategy()
                            }
                        };
                        return JsonConvert.DeserializeObject<MoedaDTO>(contentResult, settings);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return null;
        }
    }
}
