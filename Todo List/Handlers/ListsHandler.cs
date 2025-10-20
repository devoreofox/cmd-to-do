public class ListsHandler : ICommandHandler
{
    private readonly string _listsPath;
    public ListsHandler(string listsPath)
    {
        _listsPath = listsPath;
    }
    public string? Handle(string[] args, string? activeList)
    {
        string[] files = Directory.GetFiles(_listsPath, "*.txt");

        if (files.Length == 0)
        {
            Console.WriteLine("No lists available.");
            return activeList;
        }

        Console.WriteLine("Available lists:");

        foreach (string file in files)
        {
            string listName = Path.GetFileNameWithoutExtension(file);
            if (listName == activeList)
            {
                Console.WriteLine($"* {listName} (active)");
            }
            else
            {
                Console.WriteLine($"  {listName}");
            }
        }

        return activeList;
    }
}