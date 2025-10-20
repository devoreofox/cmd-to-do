public class TasksHandler : ICommandHandler
{
    private readonly string _listsPath;
    public TasksHandler(string listsPath)
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

        string filePath = Path.Combine(_listsPath, $"{activeList}.txt");

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"List '{activeList}' does not exist.");
            return activeList;
        }

        List<string> tasks = File.ReadAllLines(filePath).ToList();

        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks in the list.");
            return activeList;
        }

        Console.WriteLine($"Tasks in '{activeList}':");

        string filter = args.Length > 0 ? args[0].ToLower() : "all";

        IEnumerable<string> filteredTasks =
            from task in tasks
            where filter == "all" ||
                  (filter == "complete" && task.StartsWith("[X]")) ||
                  (filter == "incomplete" && task.StartsWith("[ ]"))
            select task;

        var display = filteredTasks.ToList();
        if (display.Count == 0)
        {
            Console.WriteLine("No tasks match the filter.");
            return activeList;
        }

        Console.WriteLine("Tasks:");
        for (int i = 0; i < display.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {display[i]}");
        }

        return activeList;
    }
}