
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace Itofinity.CommandLine.Utilities.SystemCommandLine
{
    public class Cli : ICli
    {
        public Cli(RootCommand command)
        {
            RootCommand = command;
        }

        public RootCommand RootCommand { get; }

        public int Run(IEnumerable<string> args)
        {
            return 0;
        }
        public Task<int> RunAsync(IEnumerable<string> args)
        {
            return RootCommand.InvokeAsync(args.ToArray());
        }
    }
}