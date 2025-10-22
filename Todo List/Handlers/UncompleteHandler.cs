public class UncompleteHandler : ICommandHandler
{
    private readonly ListManager _listManager;
    public UncompleteHandler(ListManager listManager)
    {
        _listManager = listManager;
    }
    public void Handle(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide the task number to uncomplete.");
            return;
        }
        try
        {
            string filePath = _listManager.GetFilePath();
            List<string> tasks = File.ReadAllLines(filePath).ToList();

            if (!int.TryParse(args[0], out int taskNumber))
            {
                Console.WriteLine("Invalid task number format.");
            }

            if (taskNumber < 1 || taskNumber > tasks.Count)
            {
                Console.WriteLine("Task number out of range.");
                return;
            }

            int index = taskNumber - 1;

            if (tasks[index].StartsWith("[ ]"))
            {
                Console.WriteLine("Task is already incomplete.");
                return;
            }

            tasks[index] = tasks[index].Replace("[X]", "[ ]");
            File.WriteAllLines(filePath, tasks);
            Console.WriteLine($"Task {taskNumber} marked as incomplete.");
        }
        catch (ListNotFoundException ex) { Console.WriteLine(ex.Message); }
    }
}