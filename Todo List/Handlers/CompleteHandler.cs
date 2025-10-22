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
            Console.WriteLine("Please provide the task number to complete.");
            return;
        }

        if (!int.TryParse(args[0], out int taskNumber))
        {
            Console.WriteLine("Invalid task number format.");
            return;
        }

        try
        {
            string filePath = _listManager.GetFilePath();
            List<string> tasks = File.ReadAllLines(filePath).ToList();

            if (taskNumber < 1 || taskNumber > tasks.Count)
            {
                Console.WriteLine("Task number out of range.");
                return;
            }

            int index = taskNumber - 1;

            if (tasks[index].StartsWith("[X]"))
            {
                Console.WriteLine("Task is already completed.");
                return;
            }

            tasks[index] = tasks[index].Replace("[ ]", "[X]");
            File.WriteAllLines(filePath, tasks);
            Console.WriteLine($"Task {taskNumber} marked as complete.");
        }
        catch (ListNotFoundException ex) { Console.WriteLine(ex.Message); }
    }
}