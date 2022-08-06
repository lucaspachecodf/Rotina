using Rotina.Domain.Contracts;
using Rotina.Domain.Dtos;
using Rotina.Domain.Enums;
using Rotina.Domain.Helpers;
using Rotina.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Rotina.Domain
{
    public class DadosMoeda : DadosBase, IDadosMoeda
    {
        private readonly IFilaService _filaService;
        private readonly IDadosCotacao _dadosCotacao;
        private readonly IDadosRetorno _dadosRetorno;

        public DadosMoeda(IFilaService filaService, IDadosCotacao dadosCotacao, IDadosRetorno dadosRetorno)
        {
            _filaService = filaService;
            _dadosCotacao = dadosCotacao;
            _dadosRetorno = dadosRetorno;
        }

        public async Task Iniciar()
        {
            var itemFila = await _filaService.GetItemFila();
            if (itemFila != null)
            {
                var dadosMoedaExtraidos = DadosMoedaExtraidos();
                var filtroDadosMoedaPorPeriodo = dadosMoedaExtraidos.Where(f => f.Data >= itemFila.DataInicio && f.Data <= itemFila.DataFim).ToList();
                var dadosCotacaoFormatados = _dadosCotacao.DadosCotacaoExtraidos();

                _dadosRetorno.Iniciar();
                ICollection<DadosRetornoVO> retorno = null;

                var moedasAgrupadas = AgruparMoedas(filtroDadosMoedaPorPeriodo);
                foreach (var moeda in moedasAgrupadas)
                {
                    Enum.TryParse(moeda.IdMoeda, out EMoeda codCotacao);
                    var filtroCotacao = dadosCotacaoFormatados.Where(f => Convert.ToInt32(f.Codigo) == (int)codCotacao && f.Data >= itemFila.DataInicio && f.Data <= itemFila.DataFim).ToList();
                    retorno = _dadosRetorno.GerarDadosRetorno(moeda.IdMoeda, filtroCotacao);
                }
                _dadosRetorno.GerarArquivoRetorno(retorno);
            }
            else
            {
                string mensagem = "Nenhum dado retornado da API ";
                Console.WriteLine(mensagem);
                Log.LoggerRetorno.Info(mensagem);
            }
        }

        private ICollection<DadosMoedaDTO> AgruparMoedas(List<DadosMoedaDTO> moedas) => moedas.GroupBy(g => new { g.IdMoeda }).Select(m => m.First()).ToList();

        private List<DadosMoedaDTO> DadosMoedaExtraidos()
        {
            Console.WriteLine("Extraindo os dados da moeda no arquivo .csv");
            List<DadosMoedaDTO> dadosFormatados = new List<DadosMoedaDTO>();
            DataSet dataSet = ObterDataSet(Util.FilePathDadosMoeda);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                dadosFormatados.Add(new DadosMoedaDTO { IdMoeda = row[0].ToString(), Data = Convert.ToDateTime(row[1]) });
            }
            return dadosFormatados;
        }
    }
}
