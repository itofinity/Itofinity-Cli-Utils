using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Options.Global
{
    public class Token : AbstractOptionDefinition
    {
        public override string Template => "-t|--token";

        public override string Description => "An authentication Token";

        public override CommandOptionType OptionType => CommandOptionType.SingleValue;
    }
}