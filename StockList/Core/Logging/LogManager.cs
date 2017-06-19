using System;
using Core.Interfaces;


namespace Core.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class FrameworkLogManager
    {
        public static ILog GetLog(Type type, LogFacade facade)
        {
            if (facade.Name == "Log4Net")
            {
                return new Log4Net(type);
            }

            // Default
            return new Nlog(type);
        }


        public static void LogDebug(Type caller, string format, params object[] args)
        {
            Log(caller,LogLevel.Debug, format, args);
        }

        public static void Log(Type caller, LogLevel level, string format, params object[] args)
        {
            var log = GetLog(caller, LogFacade.Default);
            log.Log(level, format, args);
        }

        public static void Log(this object caller, LogLevel level, string format, params object[] args)
        {
            Log(caller.GetType(), level, format, args);
        }
    }
}
