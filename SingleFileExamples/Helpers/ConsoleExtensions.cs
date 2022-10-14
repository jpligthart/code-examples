using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleFileExamples.Helpers
{
    internal static class ConsoleHelper
    {
        private const int DefaultSeparatorWidth = 70;

        public static void WriteList(IEnumerable<int> list)
        {
            foreach (int value in list)
            {
                Console.WriteLine(value);
            }
        }

        public static void WriteSeparator(int width = DefaultSeparatorWidth)
        {
            Console.WriteLine(new string('_', width) + Environment.NewLine);
        }

        public static string GetFormattedTimeSpan(TimeSpan elapsedTime)
        {
            return $"{elapsedTime:ss},{elapsedTime:fffffff}";
        }
    }
}
