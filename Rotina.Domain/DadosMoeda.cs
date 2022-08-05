using Rotina.Domain.Contracts;
using Rotina.Domain.Dtos;
using Rotina.Domain.Enums;
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

        public DadosMoeda(IFilaService filaService, IDadosCotacao dadosCotacao)
        {
            _filaService = filaService;
            _dadosCotacao = dadosCotacao;
        }

        public async Task Iniciar()
        {
            var itemFila = await _filaService.GetItemFila();
            if (itemFila != null)
            {
                var dadosMoedaExtraidos = DadosMoedaExtraidos();
                var filtroDadosMoedaPorPeriodo = dadosMoedaExtraidos.Where(f => f.Data >= itemFila.DataInicio && f.Data <= itemFila.DataFim).ToList();
                var dadosCotacaoFormatados = _dadosCotacao.DadosCotacaoExtraidos();

                foreach (var moeda in AgruparMoedas(dadosMoedaExtraidos))
                {
                    try
                    {
                        var tEnum = Enum.GetName(typeof(EMoeda), moeda);

                        EMoeda codCotacao = (EMoeda)Enum.Parse(typeof(EMoeda), tEnum);
                        var filtroCotacao = dadosCotacaoFormatados.Where(f => Convert.ToInt32(f.Codigo) == (int)codCotacao).ToList();

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private ICollection<string> AgruparMoedas(List<DadosMoedaDTO> moedas) => moedas.Distinct().Select(m => m.IdMoeda).ToList();

        private List<DadosMoedaDTO> DadosMoedaExtraidos()
        {
            List<DadosMoedaDTO> dadosFormatados = new List<DadosMoedaDTO>();
            DataSet dataSet = ObterDataSet($"{Environment.CurrentDirectory}\\DadosMoeda.csv");
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                dadosFormatados.Add(new DadosMoedaDTO { IdMoeda = row[0].ToString(), Data = Convert.ToDateTime(row[1]) });
            }
            return dadosFormatados;
        }
    }
}
