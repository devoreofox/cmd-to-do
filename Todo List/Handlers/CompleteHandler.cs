public class CompleteHandler : ICommandHandler
{
    private readonly ListManager _listManager;

    public CompleteHandler(ListManager listManager)
    {
        _listManager = listManager;
    }

    public void Handle(string[] args)
    {

        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the task number to complete.");
            return;
        }

        if (!int.TryParse(args[0], out int taskNumber))
        {
            Console.Error.WriteLine("Invalid task number format.");
            return;
        }

        try
        {
            var tasks = _listManager.LoadTasks();

            if (taskNumber < 1 || taskNumber > tasks.Count)
            {
                Console.Error.WriteLine("Task number out of range.");
                return;
            }

            int index = taskNumber - 1;

            if (tasks[index].IsCompleted)
            {
                Console.Error.WriteLine("Task is already completed.");
                return;
            }

            tasks[index].IsCompleted = true;
            _listManager.SaveTasks(tasks);
            Console.WriteLine($"Task {taskNumber} marked as complete.");
        }
        catch (ListNotFoundException ex) { Console.Error.WriteLine(ex.Message); }
    }
}