using Itofinity.Refit.Cli.Utils.Commands;
using Itofinity.Refit.Cli.Utils.Options;
using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Itofinity.Refit.Cli.Utils
{
    public class CliBuilder<T>
    {
        public CommandLineApplication App { get; private set; }

        private static IEnumerable<IOptionDefinition> _globalOptionDefinitions = new List<IOptionDefinition>()
        {
            new Options.Global.Verbosity(),
            new Options.Global.LogFilePath(),
        };

        private static ILogger Logger { get; } = ApplicationLogging.CreateLogger<CliBuilder<T>>();
        private static ExtensionCommandFactory<T> _extensionFactory = new ExtensionCommandFactory<T>();

        private CliBuilder()
        {
        }

        public static CliBuilder<T> Build<T>(string name, string description,
            IClientFactory<T> clientFactory, IPorcelainCommandFactory<T> porcelainCommandFactory, IEnumerable<string> extensionPaths, string searchPattern)
        {
            var cliBuilder = new CliBuilder<T>();
            cliBuilder.App = new CommandLineApplication();
            cliBuilder.App.Name = name;
            cliBuilder.App.Description = description;
            cliBuilder.App.HelpOption("-?|-h|--help");

            _globalOptionDefinitions.ToList().ForEach(o => cliBuilder.App.Option(o.Template, o.Description, o.OptionType));

            cliBuilder.App.OnExecute(() =>
            {
                // minimal requirements
                cliBuilder.App.ShowHint();

                return 0;
            });

            // set top level commands
            porcelainCommandFactory.AddPorcelain(cliBuilder.App, clientFactory);
            _extensionFactory.AddExtensions(cliBuilder.App, extensionPaths, searchPattern);
            return cliBuilder;
        }

        public void Execute(string[] args)
        {
            try
            {
                App.Execute(args);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
#if DEBUG
            Console.WriteLine();
            Console.WriteLine("In DEBUG Mode, hit [ENTER] to exit.");
            Console.ReadLine();
#endif
        }
    }
}