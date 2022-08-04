using CsvHelper;
using Rotina.Domain.Contracts;
using Rotina.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
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
                List<DadosMoedaDTO> dadosFormatados = new List<DadosMoedaDTO>();
                _ = DadosExtraidos(dadosFormatados);

                
            }
        }

        private List<DadosMoedaDTO> DadosExtraidos(List<DadosMoedaDTO> dadosFormatados)
        {            
            using var reader = new StreamReader($"{Environment.CurrentDirectory}\\DadosMoeda.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            using var dr = new CsvDataReader(csv);
            var dataTable = new DataTable();
            dataTable.Load(dr);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray[0].ToString();
                string[] rowArray = row.Split(';');
                dadosFormatados.Add(new DadosMoedaDTO { IdMoeda = rowArray[0], Data = Convert.ToDateTime(rowArray[1]) });
            }
            return dadosFormatados;
        }        
    }
}
