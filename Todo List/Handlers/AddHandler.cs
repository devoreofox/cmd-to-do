public class AddHandler : ICommandHandler
{
    private readonly string _listsPath;

    public AddHandler(string listsPath)
    {
        _listsPath = listsPath;
    }
    public string? Handle(string[] args, string? activeList)
    {
        if (activeList == null)
        {
            Console.WriteLine("No active list. Please open a list first.");
            return activeList;
        }

        if (args.Length == 0)
        {
            Console.WriteLine("Please provide a task description to add.");
            return activeList;
        }

        var task = string.Join(' ', args);
        string filePath = Path.Combine(_listsPath, $"{activeList}.txt");

        File.AppendAllText(filePath, $"[ ] {task}{Environment.NewLine}");
        Console.WriteLine($"Task added to '{activeList}': {task}");

        return activeList;
    }
}