using System;
using Itofinity.CommandLine.Utilities.SystemCommandLine;
using Microsoft.Extensions.Logging;
using Itofinity.CommandLine.Utilities.Logging;
using Serilog;

namespace Itofinity.CommandLine.Utilities.Extension.Serilog
{
    public static class CliBuilderExtension
    {
        public static CliBuilder AddSerilog(
            this CliBuilder builder)
        {
            ConfigureSerilog();

            return builder;
        }

        public static void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

            ApplicationLogging.LoggerFactory.AddSerilog();
        }
    }
}
