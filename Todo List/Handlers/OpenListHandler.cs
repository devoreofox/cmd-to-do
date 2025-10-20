public class OpenListHandler : ICommandHandler
{
    private readonly string _listsPath;
    public OpenListHandler(string listsPath)
    {
        _listsPath = listsPath;
    }
    public string? Handle(string[] args, string? activeList)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the name of the list to open.");
            return activeList;
        }
        var listName = args[0];
        string filePath = Path.Combine(_listsPath, $"{listName}.txt");

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"List '{listName}' does not exist.");
            return activeList;
        }

        Console.WriteLine($"List '{listName}' is now active.");
        return listName;
    }
}