using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;

namespace Itofinity.Refit.Cli.Utils
{
    public interface IClientFactory<T>
    {
        Type ClientType { get; }

        IEnumerable<PropertyInfo> GetProperties();

        T GetClient();

        T GetClient(string scheme, string securityToken);

        T GetClient(string apiUrl, string scheme, string securityToken);

        T GetClient(string apiUrl, Func<Task<Tuple<string, string>>> getSchemeAndToken);

        string GetUserAgent(CommandLineApplication app);
    }
}