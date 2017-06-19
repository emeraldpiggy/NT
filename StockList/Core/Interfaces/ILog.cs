using Core.Logging;

namespace Core.Interfaces
{
    public interface ILog
    {
        void Log(LogLevel level, string format, params object[] args);
    }
}