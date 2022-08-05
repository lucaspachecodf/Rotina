using Rotina.Domain.Dtos;
using System.Collections.Generic;

namespace Rotina.Domain.Contracts
{
    public interface IDadosCotacao
    {
        List<DadosCotacaoDTO> DadosCotacaoExtraidos();
    }
}
