using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Options
{
    public abstract class AbstractOptionDefinition : IOptionDefinition
    {
        public abstract string Template { get; }
        public abstract string Description { get; }
        public abstract CommandOptionType OptionType { get; }
    }
}