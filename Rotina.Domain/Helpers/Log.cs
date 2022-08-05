using NLog;

namespace Rotina.Domain.Helpers
{
    public sealed class Log
    {
        public static readonly Logger LoggerRetorno = LogManager.GetLogger("logFileSuccess");
        public static readonly Logger LoggerError = LogManager.GetLogger("logFileError");
    }
}
