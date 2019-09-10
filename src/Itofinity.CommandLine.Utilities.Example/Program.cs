using System;
using Itofinity.CommandLine.Utilities.SystemCommandLine;
using Itofinity.CommandLine.Utilities.Logging;
using Itofinity.CommandLine.Utilities.Extension.Serilog;
using Serilog;
using Microsoft.Extensions.Logging;

namespace Itofinity.CommandLine.Utilities.Example
{
    class Program
    {
        private static Microsoft.Extensions.Logging.ILogger Logger { get; set;}

        static void Main(string[] args)
        {
            // configure logging early
            CliBuilderExtension.ConfigureSerilog();
            Logger = ApplicationLogging.CreateLogger<Program>();

            Logger.LogInformation($"Start?");
            Logger.LogError($"Start?");
            Logger.LogDebug($"Start?");

            var cli = CliBuilder.Application()
                // configure logging late
                //.AddSerilog()
                .SetDescription("An example application built using CliBuilder and System.CommandLine")
                // pulls in Itofinity.CommandLine.Utilities.Example.Extensions.InternalCommandDefinition
                .ImportCommands()
                // pulls in Itofinity.CommandLine.Utilities.Example.Extensions.ExternalCommandDefinition
                .ImportCommands("Itofinity*.dll")
                // locally defined command
                .DefineCommand("CommandOne", "A command to do one", () => { System.Console.WriteLine("Called Command One"); })
                .Build();

            Environment.Exit(cli.RunAsync(args).Result);
        }
    }
}
