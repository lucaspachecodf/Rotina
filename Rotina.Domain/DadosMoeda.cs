using Rotina.Domain.Contracts;
using System.Threading.Tasks;

namespace Rotina.Domain
{
    public class DadosMoeda : IDadosMoeda
    {
        private readonly IFilaService _filaService;
        public DadosMoeda(IFilaService filaService)
        {
            _filaService = filaService;
        }

        public async Task Iniciar()
        {
            var itemsFila = await _filaService.GetItemFila();

            if (itemsFila != null)
            {

            }
        }
    }
}
