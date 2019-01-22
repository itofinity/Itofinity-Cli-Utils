using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Options.Global
{
    public class Url : AbstractOptionDefinition
    {
        public override string Template => "-u|--url";

        public override string Description => "Root API Url";

        public override CommandOptionType OptionType => CommandOptionType.SingleValue;
    }
}