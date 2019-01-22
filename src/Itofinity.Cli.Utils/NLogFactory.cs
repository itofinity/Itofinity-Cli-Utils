using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Itofinity.Refit.Cli.Utils
{
    public class NLogFactory
    {
        private static Microsoft.Extensions.Logging.ILogger Logger { get; } = ApplicationLogging.CreateLogger<NLogFactory>();

        public static NLog.LogLevel GetLogLevel(string requestedLevel)
        {
            switch (requestedLevel.ToLower())
            {
                case "info":
                    return NLog.LogLevel.Info;

                case "debug":
                    return NLog.LogLevel.Debug;

                case "error":
                    return NLog.LogLevel.Error;

                case "off":
                default:
                    return NLog.LogLevel.Off;
            }
        }

        public static void Configure(string requestedLogFilePath, NLog.LogLevel requestedLogLevel)
        {
            // Add NLog provider
            ApplicationLogging.LoggerFactory.AddNLog(new NLogProviderOptions());

            //target.Layout = "${message}";
            NLog.Config.SimpleConfigurator.ConfigureForFileLogging(
              requestedLogFilePath, requestedLogLevel);

            Logger.LogInformation($"Logging configured to [{requestedLogFilePath}]");
        }
    }
}