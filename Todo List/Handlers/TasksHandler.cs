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
            var activeList = _listManager.GetActiveList();
            var tasks = _listManager.LoadTasks();
            string filter = args.Length > 0 ? args[0].ToLower() : "all";

            if (tasks.Count == 0)
            {
                Console.Error.WriteLine("No tasks in the list.");
                return;
            }

            Console.WriteLine($"Tasks in '{activeList}':");

            IEnumerable<TodoItem> filteredTasks =
            from task in tasks
            where filter == "all" ||
                  (filter == "complete" && task.IsCompleted) ||
                  (filter == "incomplete" && !task.IsCompleted)
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
                var t = display[i];
                string checkbox = t.IsCompleted ? "[X]" : "[ ]";
                Console.WriteLine($"{i + 1}. {checkbox} {t.Description}");
            }
        }
        catch (ListNotFoundException ex) { Console.Error.WriteLine(ex.Message); }
    }
}