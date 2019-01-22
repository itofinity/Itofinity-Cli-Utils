using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Commands
{
    public interface ICommandDefinition
    {
        string Name { get; }

        Action<CommandLineApplication> GetConfiguration(CommandLineApplication app);
    }
}