public class AddHandler : ICommandHandler
{
    private readonly ListManager _listManager;

    public AddHandler(ListManager listManager)
    {
        _listManager = listManager;
    }

    public void Handle(string[] args)
    {

        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide a task description to add.");
            return;
        }
        var task = string.Join(' ', args);

        try
        {
            var activeList = _listManager.GetActiveList();
            var tasks = _listManager.LoadTasks();
            var newTask = new TodoItem(task);

            tasks.Add(newTask);
            _listManager.SaveTasks(tasks);
            Console.WriteLine($"Task added to '{activeList}': {task}");
        }
        catch (ListNotFoundException ex) { Console.Error.WriteLine(ex.Message); }
    }
}