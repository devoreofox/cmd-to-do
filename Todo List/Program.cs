if (args.Length == 0)
{
    Console.Error.WriteLine("Usage : todo <command> [arguments]");
    Console.WriteLine("Type 'todo help' to see available commands.");
    return;
}

var command = args[0].ToLower();
var resolver = new CommandHandlerResolver();
var handler = resolver.ResolveGlobalCommand(command);

if (handler is not null)
{
    handler.Handle(args[1..]);
    return;
}

try
{
    var listManager = new ListManager(new TodoProject());
    resolver = new CommandHandlerResolver(listManager);
    handler = resolver.ResolveProjectCommand(command);

    if (handler is null)
    {
        Console.Error.WriteLine($"Unknown command: {command}");
        return;
    }

    if (!TodoProject.IsInitialized())
    {
        Console.Error.WriteLine("No .todo directory found, please use `todo init` to initialize the project.");
        return;
    }

    handler.Handle(args[1..]);
}
catch (Exception e)
{
    Console.Error.WriteLine($"Error: {e.Message}");
}