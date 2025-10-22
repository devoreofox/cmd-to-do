public class RemoveHandler: ICommandHandler
{
    private readonly ListManager _listManager;
    public RemoveHandler(ListManager listManager)
    {
        _listManager = listManager;
    }
    public void Handle(string[] args)
    {
        if (args.Length == 0) Console.WriteLine("Specify what to remove: a number, 'all', 'complete', or 'incomplete'.");

        try
        {
            string filePath = _listManager.GetFilePath();
            var activeList = _listManager.GetActiveList();
            var tasks = File.ReadAllLines(filePath).ToList();

            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks to remove.");
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
        }
        catch (ListNotFoundException ex) { Console.WriteLine(ex.Message); }
    }
}