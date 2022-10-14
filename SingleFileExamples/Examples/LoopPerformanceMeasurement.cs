using System.Diagnostics;
using SingleFileExamples.Interfaces;
using SingleFileExamples.Helpers;

namespace SingleFileExamples.Examples;

public class LoopPerformanceMeasurement : IExample
{
    private const int MaxValue = 10_000_000;
    private const int ValueToSearch = MaxValue - 1;

    private readonly List<int> _data = new();

    public void Execute()
    {
        InitDataCollection();

        string genericMessage = $"Find value '{ValueToSearch}' using ";

        TestMethodDefinition loopTester = new(_data, ValueToSearch);

        Console.WriteLine("Testing started: ");
        ExecuteTestMethod($"{genericMessage} foreach", loopTester.TestFindWithForeach);
        ExecuteTestMethod($"{genericMessage} for", loopTester.TestFindWithFor);
        ExecuteTestMethod($"{genericMessage} while", loopTester.TestFindWithWhile);
        ExecuteTestMethod($"{genericMessage} linq", loopTester.TestFindWithLinq);
    }

    private void ExecuteTestMethod(string message, Func<bool> testMethod)
    {
        Stopwatch stopwatch = new();

        Console.WriteLine();
        Console.WriteLine(message);

        stopwatch.Start();
        bool isFound = testMethod();
        stopwatch.Stop();

        PrintTimerResult(isFound, stopwatch);
        stopwatch.Reset();
    }

    private void InitDataCollection()
    {
        for (int i = 0; i < MaxValue; i++)
        {
            _data.Add(i);
        }
    }

    private static void PrintTimerResult(bool isFound, Stopwatch stopwatch)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;

        if (isFound)
        {
            Console.WriteLine($"Found value in {ConsoleHelper.GetFormattedTimeSpan(stopwatch.Elapsed)} seconds");
        }
        else
        {
            Console.WriteLine($"Not Found! And search duration is {ConsoleHelper.GetFormattedTimeSpan(stopwatch.Elapsed)} seconds");
        }

        Console.ResetColor();
    }
}

public class TestMethodDefinition
{
    private readonly int _searchedValue;

    private readonly List<int> _dataCollection;

    public TestMethodDefinition(IEnumerable<int> dataCollection, int searchedValue)
    {
        _dataCollection = dataCollection.ToList();
        _searchedValue = searchedValue;
    }

    private bool IsSearchedValue(int element)
    {
        return element == _searchedValue;
    }

    public bool TestFindWithFor()
    {
        for (int i = 0; i < _dataCollection.Count; i++)
        {
            if (IsSearchedValue(i)) return true;
        }

        return false;
    }

    public bool TestFindWithForeach()
    {
        foreach (int element in _dataCollection)
        {
            if (IsSearchedValue(element)) return true;
        }

        return false;
    }

    public bool TestFindWithLinq()
    {
        int? result = _dataCollection.FirstOrDefault(element => element == _searchedValue);
        return result != null;
    }

    public bool TestFindWithWhile()
    {
        bool isFound = false;
        int index = 0;
        int element;

        while (!isFound && index < _dataCollection.Count)
        {
            element = _dataCollection[index];
            isFound = IsSearchedValue(element);

            index++;
        }

        return isFound;
    }
}