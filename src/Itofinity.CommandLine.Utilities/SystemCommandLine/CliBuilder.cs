using System;
using System.CommandLine;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Composition.Hosting;
using System.Runtime.Loader;
using Itofinity.CommandLine.Utilities.Commands;
using GlobExpressions;

namespace Itofinity.CommandLine.Utilities.SystemCommandLine
{
    public class CliBuilder
    {
        public string Description { get; private set; }
        public List<ICommandDefinition> CommandDefinitions { get; private set; } = new List<ICommandDefinition>();
        public bool ImportInternalCommandDefinitions { get; private set; }
        public string ImportCommandDefinitionGlob { get; private set; }
        public static CliBuilder Application()
        {
            var builder = new CliBuilder();
            return builder;
        }

        public CliBuilder ImportCommands()
        {
            ImportInternalCommandDefinitions = true;
            return this;
        }

        public CliBuilder ImportCommands(string searchGlob)
        {
            ImportCommandDefinitionGlob = searchGlob;
            return this;
        }

        public CliBuilder SetDescription(string description)
        {
            Description = description;
            return this;
        }

        public CliBuilder DefineCommand(ICommandDefinition commandDefinition)
        {
            CommandDefinitions.Add(commandDefinition);
            return this;
        }

        public CliBuilder DefineCommand(string name, string description, Action action)
        {
            CommandDefinitions.Add(new BaseCommandDefinition(name, description, action));
            return this;
        }

        public ICli Build()
        {
            var rootCommand = new RootCommand();

            if (Description != null)
            {
                rootCommand.Description = Description;
            }

            CommandDefinitions.ForEach(cd =>
            {
                rootCommand.AddCommand(new Command(cd.Name, cd.Description));
            });

            var importedCommandDefinitions = ComposeCommandDefinitions(ImportInternalCommandDefinitions, ImportCommandDefinitionGlob);

            importedCommandDefinitions.ToList().ForEach(cd =>
            {
                rootCommand.AddCommand(new Command(cd.Name, cd.Description));
            });

            return new Cli(rootCommand);
        }


        private IEnumerable<ICommandDefinition> ComposeCommandDefinitions(bool importInternal, string importGlob)
        {
            //var assemblies = importInternal ? new List<Assembly>() { Assembly.GetEntryAssembly() } : new List<Assembly>();

            var executableLocation = Assembly.GetEntryAssembly().Location;
            var rootDirectory = new FileInfo(executableLocation).DirectoryName;

            var root = new DirectoryInfo(rootDirectory);
            var assemblies = root.GlobFiles(importGlob)
                        .Select(f => AssemblyLoadContext.Default.LoadFromAssemblyPath(f.FullName))
                        .ToList();
            var entryAssembly = Assembly.GetEntryAssembly();
            if (importInternal && !assemblies.Contains(entryAssembly))
            {
                assemblies.Add(entryAssembly);
            }
            else if (!importInternal && assemblies.Contains(entryAssembly))
            {
                assemblies.Remove(entryAssembly);
            }

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);
            using (var container = configuration.CreateContainer())
            {
                return container.GetExports<ICommandDefinition>();
            }
        }
    }
}