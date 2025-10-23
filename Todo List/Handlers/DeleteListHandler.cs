public class DeleteListHandler : ICommandHandler
{
    private readonly ListManager _listManager;

    public DeleteListHandler(ListManager listManager)
    {
        _listManager = listManager;
    }

    public void Handle(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the name of the list to delete.");
            return;
        }

        try
        {
            string listName = args[0];
            _listManager.DeleteList(listName);
            Console.WriteLine($"List '{listName}' has been deleted.");
        }
        catch (ListNotFoundException ex) { Console.Error.WriteLine(ex.Message); }
    }
}