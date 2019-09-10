using System.Threading.Tasks;
using System.Collections.Generic;

namespace Itofinity.CommandLine.Utilities {
    public interface ICli {
        int Run (IEnumerable<string> args);
        Task<int> RunAsync (IEnumerable<string> args);
    }
}