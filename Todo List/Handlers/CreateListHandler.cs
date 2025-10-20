public class CreateListHandler : ICommandHandler
{
    private readonly string _listsPath;
    public CreateListHandler(string listsPath)
    {
        _listsPath = listsPath;
    }
    public string? Handle(string[] args, string? activeList)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a name for the new list.");
            return activeList;
        }

        var listName = args[0];
        string filePath = Path.Combine(_listsPath, $"{listName}.txt");

        if (File.Exists(filePath))
        {
            Console.WriteLine($"List '{listName}' already exists.");
            return activeList;
        }

        File.Create(filePath).Dispose();
        Console.WriteLine($"List '{listName}' has been created.");

        return activeList;
    }
}