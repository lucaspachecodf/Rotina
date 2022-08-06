using Rotina.Domain.Contracts;
using Rotina.Domain.Dtos;
using Rotina.Domain.Helpers;
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
            Console.WriteLine("Extraindo os dados da cotação no arquivo .csv");
            List<DadosCotacaoDTO> dadosFormatados = new List<DadosCotacaoDTO>();
            DataSet dataSet = ObterDataSet(Util.FilePathDadosCotacao);
            dadosFormatados.AddRange(from DataRow row in dataSet.Tables[0].Rows select new DadosCotacaoDTO { ValorCotacao = row[0].ToString(), Codigo = Convert.ToInt32(row[1]), Data = Convert.ToDateTime(row[2]) });
            return dadosFormatados;
        }
    }
}
