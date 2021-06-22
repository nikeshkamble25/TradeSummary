using System;

namespace TradeSummaryReport.Extensions
{
    public static class Extensions
    {
        public static String ToCamelCase(this string inputValue)
        {
            return inputValue.Length > 0 ? inputValue[0].ToString().ToLowerInvariant() + inputValue.Substring(1): inputValue;
        }
    }
}
