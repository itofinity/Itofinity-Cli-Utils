using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Options.Global
{
    public class LogFilePath : AbstractOptionDefinition
    {
        public override string Template => "-l|--log";

        public override string Description => "log file path";

        public override CommandOptionType OptionType => CommandOptionType.SingleValue;
    }
}