using CsvHelper.Configuration.Attributes;
using System;

namespace Rotina.Domain.ValueObjects
{
    public class DadosRetornoVO
    {
        [Name("ID_MOEDA")]
        public string Moeda { get; set; }
        [Name("DATA_REF")]
        [Format("dd/MM/yyyy")]
        public DateTime Data { get; set; }
        [Name("VL_COTACAO")]
        public string ValorCotacao { get; set; }

        public DadosRetornoVO(string moeda, DateTime data, string valorCotacao)
        {
            Moeda = moeda;
            Data = data;
            ValorCotacao = valorCotacao;
        }
    }
}
