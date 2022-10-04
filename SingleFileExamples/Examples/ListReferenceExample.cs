using SingleFileExamples.Interfaces;
using System;
using System.Collections.Generic;

namespace SingleFileExamples.Examples
{
    public class ListReferenceIssueTester : IExample
    {
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

        private void WriteListToConsole(IEnumerable<int> list)
        {
            Console.WriteLine("------");

            foreach (int value in list)
            {
                Console.WriteLine(value);
            }

            Console.WriteLine("------");
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