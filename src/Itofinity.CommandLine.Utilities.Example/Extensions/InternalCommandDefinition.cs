using Itofinity.CommandLine.Utilities.Commands;
using Itofinity.CommandLine.Utilities.Logging;
using System;
using System.Composition;
using Microsoft.Extensions.Logging;

namespace Itofinity.CommandLine.Utilities.Example.Extensions
{
    [Export(typeof(ICommandDefinition))]
    public class InternalCommandDefinition : BaseCommandDefinition
    {
        private static ILogger Logger { get; } = ApplicationLogging.CreateLogger<InternalCommandDefinition>();
        
        public InternalCommandDefinition() :
        base("InternalCommand", "Command defined inside the launch dll", () => 
            { 
                var message = $"Running InternalCommand ...";
                Logger.LogInformation(message);
                Console.WriteLine(message); 
            })
        {
        }
    }
}