using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleFileExamples.Helpers
{
    internal static class ExtensionsMethods
    {
        public static string GetFormatInSecondsString(this TimeSpan elapsedTime)
        {
            return $"{elapsedTime:ss},{elapsedTime:fffffff}";
        }
    }
}
