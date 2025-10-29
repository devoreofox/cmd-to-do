var listsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Todo Lists");
var ListManager = new ListManager(listsPath);

var resolver = new CommandHandlerResolver(ListManager);

if (args.Length == 0)
{
    Console.Error.WriteLine("Usage : todo <command> [arguments]");
    Console.WriteLine("Type 'help' to see available commands.");
    return;
}
var command = args[0].ToLower();
var arguments = args.Skip(1).ToArray();

try
{
    var handler = resolver.Resolve(command);
    handler.Handle(arguments);
}
catch (Exception ex)
{
    Console.Error.WriteLine($"An error occurred: {ex.Message}");
}