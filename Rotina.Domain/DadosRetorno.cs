using CsvHelper;
using Rotina.Domain.Contracts;
using Rotina.Domain.Dtos;
using Rotina.Domain.Helpers;
using Rotina.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Rotina.Domain
{
    public class DadosRetorno : IDadosRetorno
    {
        private ICollection<DadosRetornoVO> DadosRetornoMontados { get; set; }

        public ICollection<DadosRetornoVO> Iniciar()
        {
            if (DadosRetornoMontados == null)
            {
                DadosRetornoMontados = new List<DadosRetornoVO>();
                Console.WriteLine("Iniciando o retorno");
            }

            return DadosRetornoMontados;
        }
        public ICollection<DadosRetornoVO> GerarDadosRetorno(string moeda, List<DadosCotacaoDTO> dadosCotacao)
        {
            if (DadosRetornoMontados == null)
                throw new Exception("Retorno não inicializado");

            foreach (var cotacao in dadosCotacao)
            {
                DadosRetornoMontados.Add(new DadosRetornoVO(moeda, cotacao.Data, cotacao.ValorCotacao));
            }
            return DadosRetornoMontados;
        }

        public void GerarArquivoRetorno(ICollection<DadosRetornoVO> dadosRetorno)
        {
            if (dadosRetorno != null)
            {
                Console.WriteLine($"Gerando arquivo de retorno .csv, no caminho {Util.RetornoPath}");
                using var writer = new StreamWriter(Util.RetornoPath);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(dadosRetorno);
            }
        }
    }
}
