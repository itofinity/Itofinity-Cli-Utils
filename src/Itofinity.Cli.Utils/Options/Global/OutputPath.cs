using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Options.Global
{
    public class OutputPath : AbstractOptionDefinition
    {
        public override string Template => "-o|--outputPath";

        public override string Description => "Redirect output to the specified file";

        public override CommandOptionType OptionType => CommandOptionType.SingleValue;
    }
}