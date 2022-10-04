using SingleFileExamples.Interfaces;

internal class Program
{
    private const int SeparatorWidth = 70;
    private const char ExitProgramCommand = 'q';
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
            ShowMenu(examples);

            int selectedIndex = GetUserInput();

            if (selectedIndex > examples.Count || selectedIndex <= 0)
            {
                Console.WriteLine();
                Console.WriteLine($"{selectedIndex} is not a valid example ID; select one from the given options!");
            }
            else
            {
                IExample currentExample = examples[selectedIndex - 1];
                Console.Clear();
                Console.WriteLine($"Started example {selectedIndex}: {currentExample.GetType().Name}");
                Console.WriteLine();

                currentExample.Execute();
            }
        }
    }

    /// <summary>
    /// Current way of getting input makes it a max of 9 examples ... for now good enough
    /// </summary>
    /// <returns></returns>
    private static int GetUserInput()
    {
        char input = Console.ReadKey().KeyChar;
        if (input == ExitProgramCommand) Environment.Exit(0);

        return input - (int)'0'; // Question for the students: What happens here?!

    }

    private static void ShowMenu(IEnumerable<IExample> examples)
    {
        Console.WriteLine(Environment.NewLine + new string('_', SeparatorWidth) + Environment.NewLine);
        Console.WriteLine("Which example do you want to run?");

        int index = 0;
        foreach (IExample example in examples)
        {
            Console.WriteLine($"   {++index}  :  {example.GetType().Name}");
        }

        Console.WriteLine();
        Console.WriteLine($"   {ExitProgramCommand}  :  Stop program");
        Console.WriteLine(new string('_', SeparatorWidth) + Environment.NewLine);

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