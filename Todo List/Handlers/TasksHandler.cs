public class TasksHandler : ICommandHandler
{
    private readonly ListManager _listManager;

    public TasksHandler(ListManager listManager)
    {
        _listManager = listManager;
    }

    public void Handle(string[] args)
    {
        try
        {
            string filePath = _listManager.GetFilePath();
            var activeList = _listManager.GetActiveList();
            List<string> tasks = File.ReadAllLines(filePath).ToList();
            string filter = args.Length > 0 ? args[0].ToLower() : "all";

            if (tasks.Count == 0)
            {
                Console.Error.WriteLine("No tasks in the list.");
                return;
            }

            Console.WriteLine($"Tasks in '{activeList}':");

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
                return;
            }

            Console.WriteLine("Tasks:");
            for (int i = 0; i < display.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {display[i]}");
            }
        }
        catch (ListNotFoundException ex) { Console.Error.WriteLine(ex.Message); }
    }
}