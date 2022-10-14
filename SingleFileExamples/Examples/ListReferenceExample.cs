using SingleFileExamples.Helpers;
using SingleFileExamples.Interfaces;

namespace SingleFileExamples.Examples
{
    public class ListReferenceIssueTester : IExample
    {
        public string Name => "List Reference Issue Tester";

        public void Execute()
        {
            Console.WriteLine("Hello World!");

            var tester = new ExampleObject();

            // Show list before manupilation
            WriteListToConsole(tester.Values);

            tester.Values.Add(4);

            // Show list again and see extra value '4'
            WriteListToConsole(tester.Values);
        }

        private static void WriteListToConsole(IEnumerable<int> list)
        {
            ConsoleHelper.WriteSeparator();
            ConsoleHelper.WriteList(list);
            ConsoleHelper.WriteSeparator();
        }
    }

    public class ExampleObject
    {
        private readonly List<int> _values = new();

        public List<int> Values => _values;

        public ExampleObject()
        {
            _values.Add(2);
            _values.Add(3);
        }
    }
}