using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rotina.Domain.Contracts;
using Rotina.Domain.Dtos;
using Rotina.Service.Config;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Rotina.Service.Services
{
    public class FilaService : IFilaService
    {
        private readonly IConfiguration _config;
        public FilaService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ICollection<MoedaDTO>> GetItemFila()
        {
            var host = $"{UtilConfig.GetUri(_config)}/GetItemFila";

            using (var client = new HttpClient())
            {                
                var result = await client.GetAsync(host);

                if (result.IsSuccessStatusCode)
                {
                    var contentResult = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return JsonConvert.DeserializeObject<ICollection<MoedaDTO>>(contentResult);
                }
            }

            return null;
        }
    }
}
