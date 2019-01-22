using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Itofinity.Refit.Cli.Utils
{
    public static class CommandLineApplicationExtensions
    {
        public static int Run(this CommandLineApplication command, CommandLineApplication app, Func<object> request)
        {
            return Run(command,
                        app.Options.FirstOrDefault(o => o.ShortName.Equals("l")),
                        app.Options.FirstOrDefault(o => o.ShortName.Equals("v")),
                        command.Options.FirstOrDefault(o => o.ShortName.Equals("i")),
                        command.Options.FirstOrDefault(o => o.ShortName.Equals("o")),
                        command.Options.FirstOrDefault(o => o.ShortName.Equals("f")),
                        request
                        );
        }

        public static int Run(CommandLineApplication command,
            CommandOption logLocationOption, CommandOption logVerbosityOption,
            CommandOption indentOption, CommandOption outputPathOption, CommandOption filterOption,
            Func<object> request)
        {
            ConfigureLogging(logLocationOption, logVerbosityOption);

            try
            {
                var results = request.Invoke();

                results.Output(indentOption.HasValue(), outputPathOption.Value(), filterOption.Value());
            }
            catch (Exception ex)
            {
                LogException(ex, 0);
            }

            return 0;
        }

        public static void LogException(Exception ex, int level)
        {
            Console.WriteLine($"{new String('.', level)} {ex.Message}");
            if (ex is AggregateException)
            {
                var aex = ex as AggregateException;
                foreach (var aexInnerException in aex.InnerExceptions)
                {
                    LogException(aexInnerException, level+1);
                }
            } else if (ex.InnerException != null)
            {
                LogException(ex.InnerException, level+1);
            }
        }

        private static void ConfigureLogging(CommandOption logLocationOption, CommandOption logVerbosityOption)
        {
            var logLevel = NLog.LogLevel.Off;
            if (logVerbosityOption.HasValue())
            {
                var requestedLevel = logVerbosityOption.Value();
                logLevel = NLogFactory.GetLogLevel(requestedLevel);
            }

            if (logLocationOption.HasValue())
            {
                NLogFactory.Configure(logLocationOption.Value(), logLevel);
            }
            else if (logLevel != NLog.LogLevel.Off)
            {
                // no file specified by a verbosity level so log to console
                ApplicationLogging.LoggerFactory.AddConsole(true);
            }
        }
    }
}