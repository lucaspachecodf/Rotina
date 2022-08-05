using System;
using System.IO;

namespace Rotina.Domain.Helpers
{
    public sealed class Util
    {
        private static readonly string DirectoryPath = Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
        private const string FileNameDadosMoeda = "DadosMoeda.csv";
        private const string FileNameDadosCotacao = "DadosCotacao.csv";
        private static readonly string FileNameDadosRetorno = $"Resultado_{DateTime.Now.ToString("yyyyMMdd")}_{DateTime.Now.ToString("HHmmss")}.csv";
        private static readonly string RootPath = Path.GetFullPath(Path.Combine(DirectoryPath, @"..\"));

        public static string RetornoPath = $"{RootPath}\\Retorno\\{FileNameDadosRetorno}";
        public static string FilePathDadosMoeda = $"{RootPath}\\Files\\{FileNameDadosMoeda}";
        public static string FilePathDadosCotacao = $"{RootPath}\\Files\\{FileNameDadosCotacao}";
    }
}
