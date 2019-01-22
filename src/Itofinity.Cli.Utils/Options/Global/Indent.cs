using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Options.Global
{
    public class Indent : AbstractOptionDefinition
    {
        public override string Template => "-i|--indent";

        public override string Description => "Format JSON output";

        public override CommandOptionType OptionType => CommandOptionType.NoValue;
    }
}