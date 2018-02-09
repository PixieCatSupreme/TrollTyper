using Common.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrollTyper.Quirks.Logging
{
    public enum LogType
    {
        Info,
        Warning,
        Error
    }

    public static class Logger
    {
        private static ILog _logger;

        public static void Initialize(ILog logger)
        {
            _logger = logger;
        }

        public static void WriteInfo(string message, bool showMethod = false, [CallerMemberName] string member = "", [CallerLineNumber] int line = -1)
        {
            WriteEventlog(message, LogType.Info, showMethod, member, line);
        }

        public static void WriteWarn(string message, bool showMethod = true, [CallerMemberName] string member = "", [CallerLineNumber] int line = -1)
        {
            WriteEventlog(message, LogType.Warning, showMethod, member, line);
        }

        public static void WriteError(string message, bool showMethod = true, [CallerMemberName] string member = "", [CallerLineNumber] int line = -1)
        {
            WriteEventlog(message, LogType.Error, showMethod, member, line);
        }

        public static void WriteException(Exception exception, [CallerMemberName] string member = "", [CallerLineNumber] int line = -1)
        {
#if DEBUG
            WriteEventlog(exception.ToString(), LogType.Error, false, member, line);
#else
            WriteEventlog(exception.Message, LogType.Error, false);
#endif
        }

        private static void WriteEventlog(string message, LogType logType, bool showMethodInfo, string member = "", int line = -1 )
        {
#if DEBUG
            if (showMethodInfo)
            {
                message = $"{member}:{line} | {message}";
            }
#endif

            switch (logType)
            {
                case LogType.Info:
                    _logger.Info(message);
                    break;
                case LogType.Warning:
                    _logger.Warn(message);
                    break;
                case LogType.Error:
                    _logger.Error(message);
                    break;
            }
        }

        //private static void ApplyMethodInfo(ref string message)
        //{

        //    MethodBase method = new Stack(3).GetMethod();
        //    ParameterInfo[] parameters = method.GetParameters();
        //    StringBuilder parametersStr = new StringBuilder("(");

        //    for (int i = 0; i < parameters.Length; i++)
        //    {
        //        ParameterInfo info = parameters[i];
        //        parametersStr.Append(info.ParameterType.Name);
        //        parametersStr.Append(' ');
        //        parametersStr.Append(info.Name);
        //        if (i + 1 < parameters.Length)
        //        {
        //            parametersStr.Append(", ");
        //        }
        //    }

        //    parametersStr.Append(')');

        //    message = $"{method.DeclaringType.Name}.{method.Name}{parametersStr} | {message}";
        //}
    }
}
