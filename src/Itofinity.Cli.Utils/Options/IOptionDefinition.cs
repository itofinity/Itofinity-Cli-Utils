using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Options
{
    public interface IOptionDefinition
    {
        string Template { get; }
        string Description { get; }
        CommandOptionType OptionType { get; }
    }
}