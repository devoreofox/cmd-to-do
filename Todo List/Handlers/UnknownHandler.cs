public class UnknownHandler : ICommandHandler
{
    public string? Handle(string[] args, string? activeList)
    {
        Console.WriteLine("Unknown command. Type 'help' to see the list of available commands.");
        return activeList;
    }
}