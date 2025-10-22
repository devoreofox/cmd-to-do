public class ListsHandler : ICommandHandler
{
    private readonly ListManager _listManager;
    public ListsHandler(ListManager listManager)
    {
        _listManager = listManager;
    }
    public void Handle(string[] args)
    {
        List<string> lists = _listManager.GetLists();

        if (lists.Count == 0)
        {
            Console.WriteLine("No lists available.");
        }

        Console.WriteLine("Available lists:");
        try
        {
            var activeList = _listManager.GetActiveList();
            foreach (string list in lists)
            {
                if (list == activeList)
                {
                    Console.WriteLine($"* {list} (active)");
                }
                else
                {
                    Console.WriteLine($"  {list}");
                }
            }
        }
        catch (ListNotFoundException ex) { foreach (string list in lists)  Console.WriteLine($"  {list}"); }
    }
}