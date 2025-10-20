public class CompleteHandler : ICommandHandler
{
    private readonly string _listsPath;
    public CompleteHandler(string listsPath)
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
            Console.WriteLine("Please provide the task number to complete.");
            return activeList;
        }

        if (!int.TryParse(args[0], out int taskNumber))
        {
            Console.WriteLine("Invalid task number format.");
            return activeList;
        }

        string filePath = Path.Combine(_listsPath, $"{activeList}.txt");
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"List '{activeList}' does not exist.");
            return activeList;
        }

        List<string> tasks = File.ReadAllLines(filePath).ToList();

        if (taskNumber < 1 || taskNumber > tasks.Count)
        {
            Console.WriteLine("Task number out of range.");
            return activeList;
        }

        int index = taskNumber - 1;

        if (tasks[index].StartsWith("[X]"))
        {
            Console.WriteLine("Task is already completed.");
            return activeList;
        }

        tasks[index] = tasks[index].Replace("[ ]", "[X]");
        File.WriteAllLines(filePath, tasks);
        Console.WriteLine($"Task {taskNumber} marked as complete.");
        return activeList;
    }
}