using Rotina.Domain.Dtos;
using System.Threading.Tasks;

namespace Rotina.Domain.Contracts
{
    public interface IFilaService
    {
        Task<MoedaDTO> GetItemFila();
    }
}
