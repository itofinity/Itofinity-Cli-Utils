using Itofinity.CommandLine.Utilities.Commands;
using Itofinity.CommandLine.Utilities.Logging;
using System;
using System.Composition;
using Microsoft.Extensions.Logging;

namespace Itofinity.CommandLine.Utilities.Example.Extension
{
    [Export(typeof(ICommandDefinition))]
    public class ExternalCommandDefinition : BaseCommandDefinition
    {
        private static ILogger Logger { get; } = ApplicationLogging.CreateLogger<ExternalCommandDefinition>();

        public ExternalCommandDefinition() :
        base("ExternalCommand", "Command defined inside an external dll", () =>
            {
                var message = $"Running ExternalCommand ...";
                Logger.LogInformation(message);
                Console.WriteLine(message);
            })
        {}
    }
}
