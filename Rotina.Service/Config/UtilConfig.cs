using Microsoft.Extensions.Configuration;
using System;

namespace Rotina.Service.Config
{
    public static class UtilConfig
    {
        public static string GetUri(IConfiguration config)
        {
            string address = config.GetSection("FilaServices:Address").Value;

            if (string.IsNullOrEmpty(address))
                throw new ArgumentException($"Erro ao encontrar host service, verifique se realmente foi configurado");

            return address;
        }
    }
}
