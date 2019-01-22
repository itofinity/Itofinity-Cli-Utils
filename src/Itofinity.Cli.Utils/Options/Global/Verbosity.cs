using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Options.Global
{
    public class Verbosity : AbstractOptionDefinition
    {
        public override string Template => "-v|--verbosity";

        public override string Description => "log verbosity";

        public override CommandOptionType OptionType => CommandOptionType.SingleValue;
    }
}