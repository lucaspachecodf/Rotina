using ExcelDataReader;
using System.Data;
using System.IO;

namespace Rotina.Domain
{
    public abstract class DadosBase
    {
        protected static DataSet ObterDataSet(string pathFile)
        {
            using FileStream stream = File.Open(pathFile, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateCsvReader(stream);
            var dataSet = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true,
                }
            });
            return dataSet;          
        }       
    }
}
