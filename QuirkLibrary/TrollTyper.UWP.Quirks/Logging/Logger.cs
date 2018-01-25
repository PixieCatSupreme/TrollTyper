using NLog;
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
        private static NLog.Logger logger;

        static Logger()
        {
            logger = LogManager.GetLogger("Railcube.Mobile.Service");
        }

        public static void WriteInfo(string message, bool showMethod = false)
        {
            WriteEventlog(message, LogType.Info, showMethod);
        }

        public static void WriteWarn(string message, bool showMethod = true)
        {
            WriteEventlog(message, LogType.Warning, showMethod);
        }

        public static void WriteError(string message, bool showMethod = true)
        {
            WriteEventlog(message, LogType.Error, showMethod);
        }

        public static void WriteException(Exception exception)
        {
            WriteEventlog(exception.ToString(), LogType.Error, false);
        }

        private static void WriteEventlog(string message, LogType logType, bool showMethodInfo)
        {
            if (showMethodInfo)
            {
                ApplyMethodInfo(ref message);
            }

            switch (logType)
            {
                case LogType.Info:
                    logger.Info(message);
                    break;
                case LogType.Warning:
                    logger.Warn(message);
                    break;
                case LogType.Error:
                    logger.Error(message);
                    break;
            }
        }

        private static void ApplyMethodInfo(ref string message)
        {
            System.Reflection.MethodBase method = new StackFrame(3).GetMethod();
            ParameterInfo[] parameters = method.GetParameters();
            StringBuilder parametersStr = new StringBuilder("(");

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo info = parameters[i];
                parametersStr.Append(info.ParameterType.Name);
                parametersStr.Append(' ');
                parametersStr.Append(info.Name);
                if (i + 1 < parameters.Length)
                {
                    parametersStr.Append(", ");
                }
            }

            parametersStr.Append(')');

            message = $"{method.DeclaringType.Name}.{method.Name}{parametersStr} | {message}";
        }
    }
}
