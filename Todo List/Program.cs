if (args.Length == 0)
{
    Console.Error.WriteLine("Usage : todo <command> [arguments]");
    Console.WriteLine("Type 'todo help' to see available commands.");
    return;
}

var command = args[0].ToLower();
var arguments = args.Skip(1).ToArray();

try
{
    var resolver = new CommandHandlerResolver();
    var handler = resolver.Resolve(command);
    handler.Handle(arguments);
}
catch (NotInitializedException ex)
{
    Console.Error.WriteLine(ex.Message);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"An error occurred: {ex.Message}");
}