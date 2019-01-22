using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Options.Global
{
    public class Filter : AbstractOptionDefinition
    {
        public override string Template => "-f|--filter";

        public override string Description => "Filter JSON output";

        public override CommandOptionType OptionType => CommandOptionType.SingleValue;
    }
}