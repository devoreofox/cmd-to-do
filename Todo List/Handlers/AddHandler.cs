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
            Console.WriteLine("Please provide a task description to add.");
            return;
        }
        var task = string.Join(' ', args);

        try
        {
            string filePath = _listManager.GetFilePath();
            var activeList = _listManager.GetActiveList();
            File.AppendAllText(filePath, $"[ ] {task}{Environment.NewLine}");
            Console.WriteLine($"Task added to '{activeList}': {task}");
        }
        catch (ListNotFoundException ex) { Console.WriteLine(ex.Message); }
    }
}