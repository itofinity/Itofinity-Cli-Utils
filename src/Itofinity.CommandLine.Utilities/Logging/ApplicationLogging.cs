using Microsoft.Extensions.Logging;

namespace Itofinity.CommandLine.Utilities.Logging
{
    /// <summary>
    /// <see cref="https://msdn.microsoft.com/en-us/magazine/mt694089.aspx"/>
    /// </summary>
    public static class ApplicationLogging
    {
        public static ILoggerFactory LoggerFactory { get; } = new LoggerFactory();

        public static ILogger CreateLogger<T>() =>
          LoggerFactory.CreateLogger<T>();
    }
}