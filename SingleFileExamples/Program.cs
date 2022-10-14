using SingleFileExamples.Helpers;
using SingleFileExamples.Interfaces;

internal class Program
{
    private const int ExitProgramCommand = 0;
    private const string Prompt = "> ";

    public static void Main(string[] args)
    {
        RunExamples();
    }

    public static void RunExamples()
    {
        var examples = CollectExamples().ToList();

        while (true)
        {
            ShowExampleMenu(examples);

            IExample? example = GetSelectedExampleFromUserInput(examples);
            if(example != null) example.Execute();
        }
    }

    private static IExample? GetSelectedExampleFromUserInput(List<IExample> examples)
    {
        int selectedIndex;
        while (!ConsoleHelper.GetNumberFromUser(out selectedIndex))
        {
            Console.WriteLine($"Please enter a valid number.");
            Console.Write(Prompt);
        }

        if (selectedIndex == 0) Environment.Exit(0);

        if (selectedIndex > examples.Count || selectedIndex < 0)
        {
            Console.WriteLine($"{Environment.NewLine}{selectedIndex} is not a valid example ID; select one from the given options!");
            Console.Write(Prompt);
        }
        else
        {
            IExample currentExample = examples[selectedIndex - 1];
            Console.Clear();

            ConsoleHelper.WriteColoredLine(ConsoleColor.Yellow, $"Started example {selectedIndex}: {currentExample.Name}{Environment.NewLine}");

            return currentExample;
        }

        return null;
    }

    private static void ShowExampleMenu(IEnumerable<IExample> examples)
    {
        ConsoleHelper.WriteSeparator();
        ConsoleHelper.WriteColoredLine(ConsoleColor.Yellow, "Which example do you want to run?");

        int index = 0;
        foreach (IExample example in examples)
        {
            Console.WriteLine($"   {++index}  :  {example.Name}");
        }

        Console.WriteLine($"{Environment.NewLine}   {ExitProgramCommand}  :  Stop program");
        
        ConsoleHelper.WriteSeparator();

        Console.Write(Prompt);
    }

    private static IEnumerable<IExample> CollectExamples()
    {
        Type convertorType = typeof(IExample);

        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => convertorType.IsAssignableFrom(p) && !p.IsInterface)
            .Select(Activator.CreateInstance).Cast<IExample>();
    }


}