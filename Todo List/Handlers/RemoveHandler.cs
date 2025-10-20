using System.Runtime.CompilerServices;

public class RemoveHandler: ICommandHandler
{
    private readonly string _listsPath;
    public RemoveHandler(string listsPath)
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
            Console.WriteLine("No tasks to remove.");
            return activeList;
        }

        if (args.Length == 0)
        {
            Console.WriteLine("Specify what to remove: a number, 'all', 'complete', or 'incomplete'.");
        }

        string target = args[0].ToLower();

        switch (target)
        {
            case "all":
                tasks.Clear();
                Console.WriteLine("All tasks removed.");
                break;
            case "complete":
                tasks = tasks.Where(t => !t.StartsWith("[X]")).ToList();
                Console.WriteLine("All completed tasks removed.");
                break;
            case "incomplete":
                tasks = tasks.Where(t => !t.StartsWith("[ ]")).ToList();
                Console.WriteLine("All incomplete tasks removed.");
                break;
            default:
                if (int.TryParse(target, out int index))
                {
                    if (index < 1 || index > tasks.Count)
                    {
                        Console.WriteLine("Invalid task number.");
                    }
                    else
                    {
                        string removedTask = tasks[index - 1];
                        string displayTask = removedTask.Length > 4 ? removedTask.Substring(4) : removedTask;
                        tasks.RemoveAt(index - 1);
                        Console.WriteLine($"Removed task: {displayTask}");
                    }
                }
                else Console.WriteLine("Invalid argument. Use an index, 'all', 'complete', or 'incomplete'.");
                break;
        }

        File.WriteAllLines(filePath, tasks);
        return activeList;
    }
}