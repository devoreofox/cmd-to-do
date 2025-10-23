public class CreateListHandler : ICommandHandler
{
    private readonly ListManager _listManager;

    public CreateListHandler(ListManager listManager)
    {
        _listManager = listManager;
    }

    public void Handle(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide a name for the new list.");
            return;
        }

        try
        {
            var listName = string.Join(" ", args);
            _listManager.CreateList(listName);
            Console.WriteLine($"List '{listName}' has been created.");
        }
        catch (ListAlreadyExistsException ex) { Console.Error.WriteLine(ex.Message); }
    }
}