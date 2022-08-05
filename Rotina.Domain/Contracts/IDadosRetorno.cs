using Rotina.Domain.Dtos;
using Rotina.Domain.ValueObjects;
using System.Collections.Generic;

namespace Rotina.Domain.Contracts
{
    public interface IDadosRetorno
    {
        ICollection<DadosRetornoVO> Iniciar();
        ICollection<DadosRetornoVO> GerarDadosRetorno(string moeda, List<DadosCotacaoDTO> dadosCotacao);
        void GerarArquivoRetorno(ICollection<DadosRetornoVO> dadosRetorno);
    }
}
