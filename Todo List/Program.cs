if (args.Length == 0)
{
    Console.Error.WriteLine("Usage : todo <command> [arguments]");
    Console.WriteLine("Type 'todo help' to see available commands.");
    return;
}

ICommandHandler? handler = null;

var command = args[0].ToLower();
var arguments = args.Skip(1).ToArray();

try
{
    if (command == "init")
    {
        handler = new InitHandler();
        handler.Handle(arguments);
        return;
    }

    var directoryManager = new DirectoryManager();
    var listManager = new ListManager(directoryManager);
    var resolver = new CommandHandlerResolver(listManager);

    handler = resolver.Resolve(command);
    handler.Handle(arguments);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"An error occurred: {ex.Message}");
}