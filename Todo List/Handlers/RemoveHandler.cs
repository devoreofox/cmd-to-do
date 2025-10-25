public class RemoveHandler: ICommandHandler
{
    private readonly ListManager _listManager;

    public RemoveHandler(ListManager listManager)
    {
        _listManager = listManager;
    }

    public void Handle(string[] args)
    {
        if (args.Length == 0) Console.Error.WriteLine("Specify what to remove: a number, 'all', 'complete', or 'incomplete'.");

        try
        {
            var tasks = _listManager.LoadTasks();

            if (tasks.Count == 0)
            {
                Console.Error.WriteLine("No tasks to remove.");
                return;
            }
            string target = args[0].ToLower();

            switch (target)
            {
                case "all":
                    tasks.Clear();
                    Console.WriteLine("All tasks removed.");
                    break;
                case "complete":
                    tasks = tasks.Where(t => !t.IsCompleted).ToList();
                    Console.WriteLine("All completed tasks removed.");
                    break;
                case "incomplete":
                    tasks = tasks.Where(t => t.IsCompleted).ToList();
                    Console.WriteLine("All incomplete tasks removed.");
                    break;
                default:
                    if (int.TryParse(target, out int index))
                    {
                        if (index < 1 || index > tasks.Count)
                        {
                            Console.Error.WriteLine("Invalid task number.");
                        }
                        else
                        {
                            var displayTask = tasks[index - 1].Description;
                            tasks.RemoveAt(index - 1);
                            Console.WriteLine($"Removed task: {displayTask}");
                        }
                    }
                    else Console.Error.WriteLine("Invalid argument. Use an index, 'all', 'complete', or 'incomplete'.");
                    break;
            }
            _listManager.SaveTasks(tasks);
        }
        catch (ListNotFoundException ex) { Console.Error.WriteLine(ex.Message); }
    }
}