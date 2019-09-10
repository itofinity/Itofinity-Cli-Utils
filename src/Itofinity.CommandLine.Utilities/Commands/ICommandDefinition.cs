using System;
using System.Collections.Generic;
using Itofinity.CommandLine.Utilities.Options;

namespace Itofinity.CommandLine.Utilities.Commands
{
    public interface ICommandDefinition
    {
        string Name { get; }

        string Description { get; }

        Action Action {get; }

        IEnumerable<ICommandDefinition> SubCommands { get; }

        IEnumerable<IOption> Options {get; }
    }
}