public class DeleteListHandler : ICommandHandler
{
    private readonly string _listsPath;
    public DeleteListHandler(string listsPath)
    {
        _listsPath = listsPath;
    }
    public string? Handle(string[] args, string? activeList)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the name of the list to delete.");
            return activeList;
        }

        string listName = args[0];
        string filePath = Path.Combine(_listsPath, $"{listName}.txt");

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"List '{listName}' does not exist.");
            return activeList;
        }

        File.Delete(filePath);
        Console.WriteLine($"List '{listName}' has been deleted.");

        if (activeList == listName)
        {
            return null; // Clear active list if it was deleted
        }

        return activeList;
    }
}