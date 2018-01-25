using NLog;
using NLog.Config;
using NLog.Targets;
using System;

namespace TrollTyper.UWP
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            config.AddTarget("console", consoleTarget);

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            consoleTarget.Layout = @"${date:format=HH\:mm\:ss} ${logger} ${message}";
            fileTarget.FileName = "${basedir}/Logging/log.txt";
            fileTarget.Layout = "${message}";

            LoggingRule consoleRule = new LoggingRule("*", LogLevel.Debug, consoleTarget);
            LoggingRule fileRule = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(consoleRule);
            config.LoggingRules.Add(fileRule);

            // Step 5. Activate the configuration
            LogManager.Configuration = config;

            TrollTyper t = new TrollTyper(args);
            if (!t.Run())
            {
                Console.Write("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
