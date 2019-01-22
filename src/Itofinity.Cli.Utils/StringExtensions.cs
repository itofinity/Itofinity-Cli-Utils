using System.Text.RegularExpressions;

namespace Itofinity.Refit.Cli.Utils
{
    /// <summary>
    /// <see cref="https://stackoverflow.com/a/37301354"/>
    /// </summary>
    public static class StringExtensions
    {
        public static string ToKebabCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Regex.Replace(
                value,
                "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                "-$1",
                RegexOptions.Compiled)
                .Trim()
                .ToLower();
        }

        public static string ToText(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return Regex.Replace(
                value,
                "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
                " $1",
                RegexOptions.Compiled)
                .Trim();
        }
    }
}