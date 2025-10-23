public class OpenListHandler : ICommandHandler
{
    private readonly ListManager _listManager;

    public OpenListHandler(ListManager listManager)
    {
        _listManager = listManager;
    }

    public void Handle(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the name of the list to open.");
            return; 
        }

        try 
        {
            var listName = string.Join(" ", args);
            _listManager.SetActiveList(listName);
            Console.WriteLine($"List '{listName}' is now active.");
        }
        catch (ListNotFoundException ex) { Console.Error.WriteLine(ex.Message); }
    }
}