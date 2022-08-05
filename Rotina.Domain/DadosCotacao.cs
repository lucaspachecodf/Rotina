using Rotina.Domain.Contracts;
using Rotina.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rotina.Domain
{
    public class DadosCotacao : DadosBase, IDadosCotacao
    {
        public List<DadosCotacaoDTO> DadosCotacaoExtraidos()
        {
            List<DadosCotacaoDTO> dadosFormatados = new List<DadosCotacaoDTO>();
            DataSet dataSet = ObterDataSet($"{Environment.CurrentDirectory}\\DadosCotacao.csv");
            dadosFormatados.AddRange(from DataRow row in dataSet.Tables[0].Rows
                                     select new DadosCotacaoDTO { ValorCotacao = Convert.ToDecimal(row[0]), Codigo = Convert.ToInt32(row[1]), Data = Convert.ToDateTime(row[2]) });
            return dadosFormatados;
        }
    }
}
