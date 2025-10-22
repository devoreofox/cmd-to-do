public class UnknownHandler : ICommandHandler
{
    public void Handle(string[] args)
    {
        Console.WriteLine("Unknown command. Type 'help' to see the list of available commands.");
    }
}