using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Options.Global
{
    public class Scheme : AbstractOptionDefinition
    {
        public override string Template => "-s|--scheme";

        public override string Description => "An authentication scheme. Defaults to Bearer";

        public override CommandOptionType OptionType => CommandOptionType.SingleValue;
    }
}