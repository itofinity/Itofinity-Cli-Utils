using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils.Commands
{
    public interface IPorcelainCommandFactory<T>
    {
        void AddPorcelain<T>(CommandLineApplication app, IClientFactory<T> clientFactory);
    }
}