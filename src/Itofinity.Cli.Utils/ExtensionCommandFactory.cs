using Itofinity.Refit.Cli.Utils.Commands;
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Itofinity.Refit.Cli.Utils
{
    public class ExtensionCommandFactory<T>
    {
        [ImportMany]
        public IEnumerable<ICommandDefinition> CommandDefinitions { get; private set; }

        private void Compose(IEnumerable<string> extensionPaths, string searchPattern)
        {
            if (!searchPattern.EndsWith(".dll"))
            {
                searchPattern = searchPattern + ".dll";
            }

            var assemblies = new List<Assembly>() { Assembly.GetEntryAssembly() };

            var executableLocation = Assembly.GetEntryAssembly().Location;
            var rootDirectory = new FileInfo(executableLocation).DirectoryName;
            assemblies.AddRange(extensionPaths?.Where(p => Directory.Exists(Path.Combine(rootDirectory, p))).SelectMany(p =>
            {
                var files = Directory
                        .GetFiles(Path.Combine(rootDirectory, p), searchPattern, SearchOption.AllDirectories)
                        .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                        .ToList();

                return files;
            }));

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);
            using (var container = configuration.CreateContainer())
            {
                CommandDefinitions = container.GetExports<ICommandDefinition>();
            }
        }

        internal void AddExtensions(CommandLineApplication app, IEnumerable<string> extensionPaths, string searchPattern)
        {
            Compose(extensionPaths, searchPattern);

            CommandDefinitions.ToList().ForEach(c => app.Command(c.Name, c.GetConfiguration(app)));
        }
    }
}